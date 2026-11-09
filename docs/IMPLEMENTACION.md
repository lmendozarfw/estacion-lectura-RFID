# Implementación del backend RFID Traceability

Documento de referencia con todo lo implementado hasta el momento: arquitectura, decisiones, comandos, archivos y estado actual.

## 1. Resumen

El proyecto es una API ASP.NET Core con arquitectura Clean Architecture para la trazabilidad de piezas RFID en producción de calzado.

El alcance implementado hasta ahora incluye:

- Solución con cuatro capas: Domain, Application, Infrastructure y Api.
- Entidades de dominio y mapeo a PostgreSQL con EF Core.
- Migración inicial aplicada sobre una base de datos en Docker.
- Interfaces de repositorio por entidad.
- Recepción de lecturas EPC desde un hub SignalR externo.
- Punto de extensión para procesar el EPC en el futuro.
- Verificación del flujo de recepción con un hub SignalR de prueba.

Todavía no se implementó:

- Parser del EPC.
- Persistencia de lecturas.
- Implementaciones concretas de repositorios.
- Casos de uso de negocio.
- Endpoints HTTP.
- Hub SignalR para Angular.
- Autenticación contra el hub del worker.
- Pruebas automatizadas.

## 2. Arquitectura

La solución sigue el patrón de Clean Architecture con dependencias hacia adentro:

```text
Api
  -> Application
  -> Infrastructure
       -> Application
       -> Domain

Application
  -> Domain

Domain
  -> sin dependencias
```

### Responsabilidad de cada capa

| Capa | Responsabilidad |
|---|---|
| `Domain` | Entidades e interfaces de repositorio. No conoce EF Core, SignalR ni ASP.NET. |
| `Application` | Modelos de entrada, puertos y futuros casos de uso. |
| `Infrastructure` | EF Core, PostgreSQL, SignalR Client, configuraciones y adaptadores. |
| `Api` | Punto de composición y host de la aplicación. |

La regla principal es que `Domain` y `Application` no dependen de detalles de infraestructura.

## 3. Estructura del repositorio

```text
.
├── compose.yaml
├── dotnet-tools.json
├── RfidTraceability.sln
├── docs/
│   └── IMPLEMENTACION.md
└── src/
    ├── RfidTraceability.Api/
    │   ├── Program.cs
    │   ├── appsettings.json
    │   └── RfidTraceability.Api.csproj
    ├── RfidTraceability.Application/
    │   ├── Readings/
    │   │   ├── EpcReading.cs
    │   │   └── IEpcReadingProcessor.cs
    │   └── RfidTraceability.Application.csproj
    ├── RfidTraceability.Domain/
    │   ├── Entities/
    │   ├── Repositories/
    │   └── RfidTraceability.Domain.csproj
    └── RfidTraceability.Infrastructure/
        ├── DependencyInjection.cs
        ├── Persistence/
        │   ├── AppDbContext.cs
        │   ├── AppDbContextFactory.cs
        │   ├── Configurations/
        │   └── Migrations/
        ├── SignalR/
        │   ├── EpcReaderMessage.cs
        │   ├── LoggingEpcReadingProcessor.cs
        │   ├── RfidReaderHubOptions.cs
        │   └── RfidReaderSignalRListener.cs
        └── RfidTraceability.Infrastructure.csproj
```

## 4. Creación de la solución

Se creó una solución con cuatro proyectos:

```bash
dotnet new sln -n RfidTraceability

dotnet new classlib -n RfidTraceability.Domain -o src/RfidTraceability.Domain -f net10.0
dotnet new classlib -n RfidTraceability.Application -o src/RfidTraceability.Application -f net10.0
dotnet new classlib -n RfidTraceability.Infrastructure -o src/RfidTraceability.Infrastructure -f net10.0
dotnet new web -n RfidTraceability.Api -o src/RfidTraceability.Api -f net10.0

dotnet sln add src/RfidTraceability.Domain
dotnet sln add src/RfidTraceability.Application
dotnet sln add src/RfidTraceability.Infrastructure
dotnet sln add src/RfidTraceability.Api
```

Luego se configuraron las referencias:

```bash
dotnet add src/RfidTraceability.Application reference src/RfidTraceability.Domain
dotnet add src/RfidTraceability.Infrastructure reference src/RfidTraceability.Application
dotnet add src/RfidTraceability.Infrastructure reference src/RfidTraceability.Domain
dotnet add src/RfidTraceability.Api reference src/RfidTraceability.Application
dotnet add src/RfidTraceability.Api reference src/RfidTraceability.Infrastructure
```

### Por qué

- `Domain` no depende de nadie.
- `Application` depende solo de `Domain`.
- `Infrastructure` depende de `Application` y `Domain`.
- `Api` depende de `Application` e `Infrastructure`.

Esto evita que el dominio conozca EF Core, SignalR o ASP.NET.

## 5. Entidades de dominio

Se crearon 8 entidades en `src/RfidTraceability.Domain/Entities/`:

| Entidad | Tabla |
|---|---|
| `PurchaseOrder` | `ordenes_compra` |
| `Program` | `programas` |
| `Lot` | `lotes` |
| `Sku` | `skus` |
| `ControlPoint` | `puntos_control` |
| `RfidPiece` | `piezas_rfid` |
| `PieceControlPointAdvanceReading` | `lecturas_avance_pieza_punto_control` |
| `ProgramGoal` | `meta_programa` |

### Relaciones principales

```text
PurchaseOrder -> Programs -> Lots -> RfidPieces
Sku -> RfidPieces
ControlPoint -> PieceControlPointAdvanceReadings
Program -> ProgramGoals
```

### Decisiones importantes

- Los nombres de clases y propiedades están en inglés.
- Las tablas y columnas se mapean al español en Infrastructure.
- Los IDs son `string` y se generan con `Guid.NewGuid().ToString()` porque el esquema de base de datos usa texto.
- El EPC y los códigos también son `string` para no perder ceros a la izquierda.
- Las fechas opcionales usan `DateTime?`.
- `ProgramGoal.Date` usa `DateOnly` porque representa un día, no una fecha con hora.

Ejemplo en `PurchaseOrder.cs`:

```csharp
public string Id { get; set; } = Guid.NewGuid().ToString();
public string Code { get; set; } = string.Empty;
public DateTime? Date { get; set; }
```

## 6. Configuración de EF Core

Se creó una configuración por entidad en:

```text
src/RfidTraceability.Infrastructure/Persistence/Configurations/
```

Cada clase implementa `IEntityTypeConfiguration<T>` y define:

- Nombre de tabla en español con `ToTable(...)`.
- Nombre de columna con `HasColumnName(...)`.
- Longitudes máximas.
- Campos requeridos.
- Índices únicos.
- Relaciones y comportamiento de borrado.
- Tipos de fecha de PostgreSQL.

Ejemplo en `PurchaseOrderConfiguration.cs`:

```csharp
builder.ToTable("ordenes_compra");

builder.Property(x => x.Code)
    .HasColumnName("codigo")
    .HasMaxLength(50)
    .IsRequired();

builder.HasIndex(x => x.Code)
    .IsUnique();
```

### Por qué usar configuraciones separadas

Porque el dominio queda limpio, sin atributos de EF Core. Toda la persistencia vive en Infrastructure.

### Borrado restringido

Se usó `DeleteBehavior.Restrict` en las relaciones. Eso evita borrados en cascada accidentales. Por ejemplo, no se puede borrar un lote si tiene piezas asociadas.

### Índices únicos

- `ordenes_compra.codigo`
- `skus.codigo`
- `piezas_rfid.epc`
- `(programas.id_orden_compra, programas.codigo)`
- `(lotes.id_programa, lotes.codigo)`
- `(lecturas_avance_pieza_punto_control.id_pieza_rfid, id_punto_control)`
- `(meta_programa.id_programa, id_punto_control, fecha)`

Esto protege la integridad desde la base de datos, no solo desde el código.

## 7. `AppDbContext` y fábrica de diseño

Archivo: `src/RfidTraceability.Infrastructure/Persistence/AppDbContext.cs`

El contexto expone los `DbSet` y aplica automáticamente todas las configuraciones:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

### Por qué `ApplyConfigurationsFromAssembly`

Para no tener que registrar manualmente cada configuración. Si mañana se agrega otra entidad, basta con crear su configuración.

### Fábrica de diseño

Archivo: `src/RfidTraceability.Infrastructure/Persistence/AppDbContextFactory.cs`

Implementa `IDesignTimeDbContextFactory<AppDbContext>`.

```csharp
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Host=localhost;Port=5433;Database=rfid_traceability;Username=postgres;Password=postgres";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
```

### Por qué

Porque `dotnet ef` necesita crear el `DbContext` sin levantar la API ni depender del contenedor de dependencias.

## 8. PostgreSQL con Docker Compose

Archivo: `compose.yaml`

Se configuró PostgreSQL 17 Alpine con:

- Contenedor `rfid-traceability-db`.
- Base `rfid_traceability`.
- Usuario `postgres`.
- Volumen persistente `postgres_data`.
- Healthcheck con `pg_isready`.
- Puerto host `5433`.

```yaml
services:
  postgres:
    image: postgres:17-alpine
    container_name: rfid-traceability-db
    restart: unless-stopped
    environment:
      POSTGRES_DB: ${POSTGRES_DB:-rfid_traceability}
      POSTGRES_USER: ${POSTGRES_USER:-postgres}
      POSTGRES_PASSWORD: ${POSTGRES_PASSWORD:-postgres}
    ports:
      - "${POSTGRES_PORT:-5433}:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test:
        [
          "CMD-SHELL",
          "pg_isready -U ${POSTGRES_USER:-postgres} -d ${POSTGRES_DB:-rfid_traceability}",
        ]
      interval: 5s
      timeout: 5s
      retries: 10

volumes:
  postgres_data:
```

### Por qué el puerto 5433

Porque el puerto 5432 ya estaba ocupado por otro contenedor llamado `registro-activo-fijo-postgres`. Usar 5433 evita conflictos.

Comando usado:

```bash
docker compose up -d
```

La cadena de conexión quedó en `appsettings.json`:

```json
"DefaultConnection": "Host=localhost;Port=5433;Database=rfid_traceability;Username=postgres;Password=postgres"
```

> Nota: estas credenciales son solo para desarrollo local. No deben usarse en producción ni commitearse secretos reales.

## 9. Herramienta local de EF Core

Se creó un manifiesto de herramientas:

```bash
dotnet new tool-manifest
dotnet tool install dotnet-ef --version 10.0.4
```

Archivo: `dotnet-tools.json`

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "dotnet-ef": {
      "version": "10.0.4",
      "commands": [
        "dotnet-ef"
      ],
      "rollForward": false
    }
  }
}
```

### Por qué local y no global

Para que la versión de `dotnet-ef` quede fijada en el repositorio y todos usen la misma. Así se evita el problema de “en mi máquina funciona con otra versión”.

## 10. Migración inicial

Se generó la migración:

```bash
dotnet tool run dotnet-ef migrations add InitialCreate \
  --project src/RfidTraceability.Infrastructure \
  --startup-project src/RfidTraceability.Infrastructure
```

### Por qué el proyecto de infraestructura como startup

Porque ahí está `AppDbContextFactory`. Así EF no necesita arrancar la API.

Luego se aplicó:

```bash
dotnet tool run dotnet-ef database update \
  --project src/RfidTraceability.Infrastructure \
  --startup-project src/RfidTraceability.Infrastructure
```

La migración se llama `20261108200010_InitialCreate` y creó las tablas:

- `ordenes_compra`
- `programas`
- `lotes`
- `skus`
- `puntos_control`
- `piezas_rfid`
- `lecturas_avance_pieza_punto_control`
- `meta_programa`
- `__EFMigrationsHistory`

Se verificó con `psql` que las tablas existieran.

## 11. Interfaces de repositorio

Se crearon interfaces en `src/RfidTraceability.Domain/Repositories/`.

Primero una interfaz base en `IRepository.cs`:

```csharp
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

Después se crearon interfaces específicas:

- `IPurchaseOrderRepository`
- `IProgramRepository`
- `ILotRepository`
- `ISkuRepository`
- `IControlPointRepository`
- `IRfidPieceRepository`
- `IPieceControlPointAdvanceReadingRepository`
- `IProgramGoalRepository`

Cada una agrega consultas propias. Por ejemplo, `IRfidPieceRepository.cs`:

```csharp
Task<RfidPiece?> GetByEpcAsync(string epc, CancellationToken cancellationToken = default);
Task<bool> ExistsByEpcAsync(string epc, CancellationToken cancellationToken = default);
Task<RfidPiece?> GetWithTraceabilityAsync(string epc, CancellationToken cancellationToken = default);
```

### Por qué interfaces y no implementaciones todavía

Porque primero se definió el contrato. Las implementaciones concretas usarán EF Core y vivirán en Infrastructure. Application podrá depender de las interfaces sin conocer la base de datos.

> Importante: actualmente no hay implementaciones de repositorios. Solo existen las interfaces.

## 12. Modelo de entrada en Application

Se creó `src/RfidTraceability.Application/Readings/EpcReading.cs`:

```csharp
public sealed record EpcReading(string Epc, DateTimeOffset ReceivedAt);
```

### Por qué

Porque es el dato que Application recibe cuando llega una lectura. No es una entidad de base de datos. Es un modelo inmutable de entrada.

`ReceivedAt` se toma al momento de recibir porque el worker solo envía `epc`.

## 13. Punto de extensión `IEpcReadingProcessor`

Archivo: `src/RfidTraceability.Application/Readings/IEpcReadingProcessor.cs`

```csharp
public interface IEpcReadingProcessor
{
    Task ProcessAsync(EpcReading reading, CancellationToken cancellationToken = default);
}
```

Esta es la pieza clave para el futuro.

Hoy solo existe una implementación que escribe en el log. Mañana se implementará aquí:

- Validación del EPC.
- Descomposición del EPC.
- Búsqueda de la pieza.
- Determinación del punto de control.
- Registro de avance.
- Notificación a Angular.

El listener de SignalR no necesita saber nada de eso. Solo llama a `ProcessAsync`.

## 14. Configuración tipada de SignalR

Archivo: `src/RfidTraceability.Infrastructure/SignalR/RfidReaderHubOptions.cs`

```csharp
public sealed class RfidReaderHubOptions
{
    public const string SectionName = "RfidReader";

    public string HubUrl { get; set; } = string.Empty;
    public string ReadingEvent { get; set; } = "epc-reader";
    public int ReconnectDelaySeconds { get; set; } = 5;
}
```

### Por qué

Para no hardcodear la URL ni el nombre del evento. Se puede cambiar desde `appsettings.json` o con variables de entorno.

La sección quedó en `appsettings.json`:

```json
"RfidReader": {
  "HubUrl": "http://localhost:5000/hubs/rfid-reader",
  "ReadingEvent": "epc-reader",
  "ReconnectDelaySeconds": 5
}
```

La URL es un placeholder. Debe reemplazarse por la del worker real.

También puede sobrescribirse con una variable de entorno:

```bash
RfidReader__HubUrl=http://mi-worker:5000/hubs/rfid-reader
```

## 15. DTO del mensaje SignalR

Archivo: `src/RfidTraceability.Infrastructure/SignalR/EpcReaderMessage.cs`

```csharp
public sealed class EpcReaderMessage
{
    [JsonPropertyName("epc")]
    public string Epc { get; set; } = string.Empty;
}
```

### Por qué no usar directamente `EpcReading`

Porque este DTO representa el contrato externo:

```json
{ "epc": "53434634457457457453434634457457" }
```

Si el worker agrega campos como `readerId` o `timestamp`, se modifica este DTO sin contaminar Application.

## 16. Listener de SignalR

Archivo: `src/RfidTraceability.Infrastructure/SignalR/RfidReaderSignalRListener.cs`

Hereda de `BackgroundService`.

### Por qué

Porque no depende de una petición HTTP. Es un proceso continuo que arranca y se detiene con la aplicación.

El flujo es:

```text
Worker -> Hub SignalR -> RfidReaderSignalRListener -> EpcReading -> IEpcReadingProcessor
```

### 16.1. Validación de configuración

```csharp
if (string.IsNullOrWhiteSpace(_options.HubUrl))
{
    _logger.LogWarning("RfidReader HubUrl is not configured. SignalR listener is disabled.");
    return;
}
```

Si no hay URL, el listener se desactiva y la API sigue funcionando.

### 16.2. Bucle de conexión

El listener ejecuta un bucle que:

1. Construye una conexión.
2. Intenta conectarse.
3. Espera hasta que se desconecte.
4. Si falla, espera y reintenta.

### Por qué un bucle si SignalR tiene reconexión automática

Porque `WithAutomaticReconnect()` maneja cortes temporales, pero no la falla inicial ni el caso en que se agotan los reintentos. El bucle externo cubre esos casos.

### 16.3. Construcción de la conexión

```csharp
var connection = new HubConnectionBuilder()
    .WithUrl(_options.HubUrl)
    .WithAutomaticReconnect()
    .Build();
```

Luego se suscribe al evento:

```csharp
connection.On<EpcReaderMessage>(_options.ReadingEvent, HandleReadingAsync);
```

Esto conecta el evento `epc-reader` con el método que procesa el mensaje.

### 16.4. Manejo de la lectura

```csharp
if (string.IsNullOrWhiteSpace(message.Epc))
{
    _logger.LogWarning("Received an empty EPC from RFID reader hub.");
    return;
}

var reading = new EpcReading(message.Epc, DateTimeOffset.UtcNow);
```

Se valida el EPC y se transforma el DTO externo en el modelo de Application.

Luego:

```csharp
using var scope = _scopeFactory.CreateScope();
var processor = scope.ServiceProvider.GetRequiredService<IEpcReadingProcessor>();
await processor.ProcessAsync(reading, CancellationToken.None);
```

### Por qué se crea un scope

Porque el listener es singleton, pero `IEpcReadingProcessor` es scoped. En el futuro usará `DbContext` y repositorios, que también son scoped. Un singleton no puede consumir directamente un scoped.

### Por qué se captura la excepción

Para que un EPC inválido o un error de base de datos no tumbe el listener.

### 16.5. Espera de desconexión

El listener usa `TaskCompletionSource` para esperar de forma asíncrona el evento `Closed` de la conexión:

```csharp
var completionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

Task OnClosed(Exception? exception)
{
    completionSource.TrySetResult();
    return Task.CompletedTask;
}

connection.Closed += OnClosed;

try
{
    await completionSource.Task.WaitAsync(cancellationToken);
}
finally
{
    connection.Closed -= OnClosed;
}
```

Esto permite esperar la desconexión sin bloquear el hilo y respetando la cancelación de la aplicación.

## 17. Procesador temporal

Archivo: `src/RfidTraceability.Infrastructure/SignalR/LoggingEpcReadingProcessor.cs`

```csharp
public sealed class LoggingEpcReadingProcessor : IEpcReadingProcessor
```

Hoy solo hace:

```csharp
_logger.LogInformation("EPC received: {Epc} at {ReceivedAt}", reading.Epc, reading.ReceivedAt);
```

### Por qué existe

Porque necesitábamos una implementación concreta para probar el flujo sin implementar todavía el parser ni los casos de uso. Es temporal.

## 18. Registro en Dependency Injection

Archivo: `src/RfidTraceability.Infrastructure/DependencyInjection.cs`

```csharp
services.Configure<RfidReaderHubOptions>(
    configuration.GetSection(RfidReaderHubOptions.SectionName));

services.AddScoped<IEpcReadingProcessor, LoggingEpcReadingProcessor>();
services.AddHostedService<RfidReaderSignalRListener>();
```

### Qué hace cada línea

- `Configure`: enlaza la sección `RfidReader` con la clase de opciones.
- `AddScoped`: registra el processor temporal.
- `AddHostedService`: registra el listener para que arranque con la aplicación.

## 19. API mínima

Archivo: `src/RfidTraceability.Api/Program.cs`

```csharp
using RfidTraceability.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Run();
```

La API no tiene controladores todavía. Su única responsabilidad actual es hospedar la infraestructura y el listener. Más adelante aquí se agregarán endpoints y el hub para Angular.

## 20. Paquetes de SignalR

En `src/RfidTraceability.Infrastructure/RfidTraceability.Infrastructure.csproj`:

| Paquete | Uso |
|---|---|
| `Microsoft.AspNetCore.SignalR.Client` | Cliente para conectarse al hub del worker. |
| `Microsoft.Extensions.Hosting.Abstractions` | Aporta `BackgroundService`. |
| `Microsoft.Extensions.Options.ConfigurationExtensions` | Permite `Configure<T>(IConfiguration)`. |
| `Microsoft.Extensions.Logging.Abstractions` | Permite usar `ILogger<T>`. |

Se usó `Microsoft.AspNetCore.SignalR.Client` versión `10.0.13` porque la `10.0.4` tenía una vulnerabilidad conocida de severidad alta, corregida en `10.0.13`.

No se agregó `AddSignalR()` porque eso es para exponer un hub. Este proyecto actualmente solo consume el hub del worker.

## 21. Verificación

Se compiló la solución:

```bash
dotnet build RfidTraceability.sln
```

Resultado: 0 errores y 0 advertencias.

Después se levantó un hub SignalR temporal en otro puerto que emitía:

```json
{ "epc": "53434634457457457453434634457457" }
```

La API se ejecutó apuntando a ese hub:

```bash
ASPNETCORE_URLS=http://localhost:5099 \
RfidReader__HubUrl=http://localhost:5180/hubs/rfid-reader \
dotnet run --project src/RfidTraceability.Api --no-build --no-launch-profile
```

Los logs mostraron:

```text
Connected to RFID reader hub at http://localhost:5180/hubs/rfid-reader
EPC received: 53434634457457457453434634457457
```

Eso confirmó que:

- La URL configurable funciona.
- El evento `epc-reader` coincide.
- El JSON se deserializa correctamente.
- El processor temporal recibe el EPC.
- La aplicación no se cae si el hub no está disponible.

También se detectó que `http://localhost:5000/hubs/rfid-reader` responde `403 Forbidden`. Eso indica que hay algo escuchando en ese puerto, pero no es el hub esperado o requiere autenticación. Por eso se usó un puerto temporal para la prueba.

## 22. Cómo ejecutar el proyecto

```bash
# 1. Levantar PostgreSQL
docker compose up -d

# 2. Restaurar herramientas locales
dotnet tool restore

# 3. Aplicar migraciones
dotnet tool run dotnet-ef database update \
  --project src/RfidTraceability.Infrastructure \
  --startup-project src/RfidTraceability.Infrastructure

# 4. Compilar
dotnet build RfidTraceability.sln

# 5. Ejecutar la API
dotnet run --project src/RfidTraceability.Api
```

La API usa el puerto `5011` por `launchSettings.json` cuando se ejecuta con el perfil de desarrollo.

## 23. Estado actual

### Implementado

- Solución y referencias entre capas.
- Entidades de dominio.
- Configuraciones Fluent API.
- `AppDbContext` y fábrica de diseño.
- Docker Compose para PostgreSQL.
- Migración inicial aplicada.
- Interfaces de repositorio.
- Modelo `EpcReading`.
- Puerto `IEpcReadingProcessor`.
- Cliente SignalR con reconexión y manejo de errores.
- DTO del evento `epc-reader`.
- Processor temporal que loggea el EPC.
- Registro en Dependency Injection.
- Configuración por `appsettings.json`.
- Verificación con hub de prueba.

### Pendiente

- Parser del EPC de 32 caracteres.
- Validación de formato del EPC.
- Persistencia de lecturas.
- Implementaciones de repositorios con EF Core.
- Casos de uso de orden, programa, lote, SKU y meta.
- Determinación del punto de control.
- Endpoints HTTP.
- Hub SignalR para Angular.
- Autenticación contra el hub del worker.
- Pruebas automatizadas.
- Manejo de idempotencia y lecturas duplicadas.

## 24. Próximos pasos sugeridos

1. Configurar la URL real del worker en `RfidReader:HubUrl`.
2. Validar la integración contra el worker real.
3. Implementar el parser del EPC y las reglas de negocio en `IEpcReadingProcessor`.
4. Implementar los repositorios con EF Core.
5. Agregar persistencia de lecturas y avances.
6. Exponer endpoints HTTP para consultas.
7. Agregar un hub SignalR para notificar a Angular en tiempo real.
8. Agregar pruebas unitarias y de integración.

## 25. Idea clave

El patrón usado es puertos y adaptadores:

- `IEpcReadingProcessor` es el puerto.
- `RfidReaderSignalRListener` es el adaptador de entrada.
- `LoggingEpcReadingProcessor` es una implementación temporal.

Cuando se implemente el parser y los casos de uso, no será necesario tocar SignalR. Solo se reemplazará la implementación de `IEpcReadingProcessor` y se agregarán repositorios y casos de uso. Esa separación es la principal ventaja del diseño actual.

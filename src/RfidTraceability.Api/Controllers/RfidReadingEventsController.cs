using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Api.Extensions;
using RfidTraceability.Application.Readings;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RfidReadingEventsController(
    ISender sender, 
    ILogger<RfidReadingEventsController> logger,
    ProductionAdvanceService productionAdvanceService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductionAdvanceResponse>> Register([FromBody] List<RfidWorkerEvent> events, CancellationToken cancellationToken)
    {
        logger.LogInformation("Eventos recibidos desde el worker:");
        foreach (var eventW in events)
        {
            logger.LogInformation($"EventId: {eventW.EventId}, Epc: {eventW.Epc}, ReaderDate: {eventW.ReaderDate}");
        }

        var command = new RegisterRfidReadingEventsCommand(
            events.Select(eventW => new RfidReadingEventInput(
                eventW.EventId,
                eventW.Epc,
                ToUtc(eventW.ReaderDate),
                eventW.LectorRfidId)).ToList(),
            User.GetOrganizationId());

        var processed = await sender.Send(command, cancellationToken);
        logger.LogInformation("Eventos procesados: {Processed}", processed);

        // METRICAS GLOBALES
        // - Meta diaria total (pares de todos los programas), es la suma de las metas de todos los programas que deben pasar por ese punto de control
        // - pares completos (en este punto de control) -> en una lectura debe haber el mismo numero de lados izquierdos que derechos, considerando todos los programas de ese punto de control
        // - Cumplimiento global (73% . n pares para cumplir) calculo tomando en cuenta los pares que han sido detectados y  la meta diaria total
        // - pares incompletos (6 . falta 1 pieza del par) considerando las lecturas de peizas de todos los programas

        // PROGRESO POR PROGRAMA
        // por cada programa se debe teners:
        // - codigo del programa
        // - codigo de la orden de compra
        // - calculo de avance considerando su meta diaria
        //   - cuantos pares hay
        //   - cuantos son los pares objetivos
        //   - cuantos lotes ya se cerraron (10 pares por lote)

        if (events.Count == 0)
        {
            return Ok(new ProductionAdvanceResponse([], [], []));
        }

        var date = DateOnly.FromDateTime(ToUtc(events[0].ReaderDate));
        var response = await sender.Send(new GetProductionAdvanceQuery(events[0].LectorRfidId, date), cancellationToken);
        await productionAdvanceService.AdvanceNotification(response);
        return Ok(response);
    }

    private static DateTime ToUtc(DateTime value) => value.Kind switch
    {
        DateTimeKind.Local => value.ToUniversalTime(),
        DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Utc),
        _ => value
    };
}

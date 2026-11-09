using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Infrastructure.Persistence.Converters;

public sealed class ProgramStatusConverter : ValueConverter<ProgramStatus, string>
{
    public ProgramStatusConverter()
        : base(
            status => ToProvider(status),
            value => FromProvider(value))
    {
    }

    private static string ToProvider(ProgramStatus status) => status switch
    {
        ProgramStatus.Planned => "PLANIFICADO",
        ProgramStatus.InProduction => "EN_PRODUCCION",
        ProgramStatus.Completed => "COMPLETADO",
        ProgramStatus.Cancelled => "CANCELADO",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };

    private static ProgramStatus FromProvider(string value) => value switch
    {
        "PLANIFICADO" => ProgramStatus.Planned,
        "EN_PRODUCCION" => ProgramStatus.InProduction,
        "COMPLETADO" => ProgramStatus.Completed,
        "CANCELADO" => ProgramStatus.Cancelled,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };
}

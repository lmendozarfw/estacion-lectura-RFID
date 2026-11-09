using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Infrastructure.Persistence.Converters;

public sealed class ReaderStatusConverter : ValueConverter<ReaderStatus, string>
{
    public ReaderStatusConverter()
        : base(
            status => ToProvider(status),
            value => FromProvider(value))
    {
    }

    private static string ToProvider(ReaderStatus status) => status switch
    {
        ReaderStatus.Active => "ACTIVO",
        ReaderStatus.Inactive => "INACTIVO",
        ReaderStatus.Maintenance => "MANTENIMIENTO",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };

    private static ReaderStatus FromProvider(string value) => value switch
    {
        "ACTIVO" => ReaderStatus.Active,
        "INACTIVO" => ReaderStatus.Inactive,
        "MANTENIMIENTO" => ReaderStatus.Maintenance,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RfidTraceability.Domain.Enums;

namespace RfidTraceability.Infrastructure.Persistence.Converters;

public sealed class DataOriginConverter : ValueConverter<DataOrigin, string>
{
    public DataOriginConverter()
        : base(
            origin => ToProvider(origin),
            value => FromProvider(value))
    {
    }

    private static string ToProvider(DataOrigin origin) => origin switch
    {
        DataOrigin.Local => "LOCAL",
        DataOrigin.Erp => "ERP",
        _ => throw new ArgumentOutOfRangeException(nameof(origin), origin, null)
    };

    private static DataOrigin FromProvider(string value) => value switch
    {
        "LOCAL" => DataOrigin.Local,
        "ERP" => DataOrigin.Erp,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };
}

public static class DomainGuard
{
    public static string NotNullOrWhiteSpace(string value, string paramName)
    {
        value = value.Trim();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"La propiedad {paramName} es requerida.", paramName);
        }
        return value;
    }

    public static string? NormalizeString(string? value, string paramName)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            value = value.Trim();
        }
        return value ?? null;
    }

    public static DateOnly? NormalizedDateOnly(DateOnly? value, string paramName)
    {
        if (value is null)
        {
            return null;
        }
        if (value.Value == DateOnly.MinValue)
        {
            throw new ArgumentException("The date is not valid.", paramName);
        }

        return value.Value;
    }

    public static Guid GuidNotEmpty(Guid value, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException($"La propiedad {paramName} es requerida.", paramName);
        }
        return value;
    }

    public static int GreaterThanZero(int value, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentException($"La pripiedad {paramName} debe ser mayor a cero.", paramName);
        }
        return value;
    }
}
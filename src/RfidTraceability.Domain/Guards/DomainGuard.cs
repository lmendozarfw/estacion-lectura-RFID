public static class DomainGuard {
    public static string NotNullOrWhiteSpace(string value, string paramName)
    {
        value = value.Trim();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"La propiedad {paramName} es requerida.", paramName);
        }
        return value;
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
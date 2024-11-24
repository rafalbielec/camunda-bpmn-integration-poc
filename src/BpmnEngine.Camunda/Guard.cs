using System.Diagnostics.CodeAnalysis;

namespace BpmnEngine.Camunda;

public static class Guard
{
    [ExcludeFromCodeCoverage]
    public static T NotNull<T>(T parameterValue, string parameterName) where T : class
    {
        if (parameterValue == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return parameterValue;
    }

    [ExcludeFromCodeCoverage]
    public static int GreaterThanOrEqual(int value, int minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentException($"Must be greater than or equal to {minValue}", parameterName);
        }

        return value;
    }

    [ExcludeFromCodeCoverage]
    public static string NotEmptyAndNotNull(string? value, string parameterName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"Mustn't be null or empty string", parameterName);
        }

        return value;
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TwitchLib.Api.Core.Exceptions;

/// <inheritdoc />
/// <summary>Exception representing an invalid resource</summary>
public class BadParameterException : Exception
{
    /// <inheritdoc />
    /// <summary>Exception constructor</summary>
    public BadParameterException(string badParamData)
        : base(badParamData)
    {
    }

    public static void ThrowIfNull(object value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is null)
        {
            throw new BadParameterException($"Parameter '{paramName}' cannot be null.");
        }
    }

    public static void ThrowIfNullOrEmpty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new BadParameterException($"Parameter '{paramName}' cannot be null or empty.");
        }
    }

    public static void ThrowIfNotBetween(int value, int min, int max, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < min || value > max)
        {
            throw new BadParameterException($"Parameter '{paramName}' cannot be less than {min}(inclusive) or greater than {max}(inclusive).");
        }
    }

    public static void ThrowIfCollectionNullOrEmptyOrGreaterThan<T>(List<T> value, int max, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is null || value.Count is 0 || value.Count > max)
        {
            throw new BadParameterException($"Parameter '{paramName}' must contains at least 1 item and cannot exceed {max} items.");
        }
    }

    public static void ThrowIfCollectioGreaterThan<T>(List<T> value, int max, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.Count > max)
        {
            throw new BadParameterException($"Parameter '{paramName}' cannot exceed {max} items.");
        }
    }
}

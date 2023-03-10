using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Services.Utils;

/// <summary>
/// This class represents a safeguard util to ensure parameters are in compliance.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Guard
{
    /// <summary>
    /// Throws ArgumentNullException if the string value is empty, null or whitespace.
    /// </summary>
    /// <param name="key">The key related.</param>
    /// <param name="value">The string value to check.</param>
    public static void ThrowIfNull(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(key, "Key is required but missing.");
        }
    }
}
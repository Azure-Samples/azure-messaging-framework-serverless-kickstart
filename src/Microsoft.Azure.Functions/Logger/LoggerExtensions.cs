using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.Logger;

/// <summary>
/// All logs should be defined here for a structured logging.
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 200,
        Level = LogLevel.Error,
        EventName = "FailedToProcessFunction",
        Message = "Failed to process a function")]
    public static partial void FailedToProcessFunction(this ILogger logger, Exception ex);
}

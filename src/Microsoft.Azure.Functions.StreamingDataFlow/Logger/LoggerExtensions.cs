using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Logger;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Logger;

/// <summary>
/// Containing all the logger extensions for the Microsoft streaming data flow. It is recommended to add
/// EventName ald EventId to the logger extension method. EventName is used to identify the log message.
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessageAttribute(
    EventId = 2000,
    Level = LogLevel.Warning,
    EventName = "MicrosoftStreamingDataEmpty",
    Message = "There is no data field or cycle information in Microsoft streaming data payload {streamingDataId}")]
    public static partial void MicrosoftStreamingInvalid(this ILogger logger, string streamingDataId);
}
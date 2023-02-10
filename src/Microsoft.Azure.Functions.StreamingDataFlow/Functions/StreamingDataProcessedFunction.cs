using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow;

/// <summary>
/// Azure function to process streaming data processed event.
/// </summary>
[ExcludeFromCodeCoverage]
public class StreamingDataProcessedFunction : BasicFunctionAsync
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataProcessedFunction"/> class.
    /// </summary>
    /// <param name="serviceProvider">A service provider.</param>
    /// <param name="loggerFactory">A logger factory.</param>
    public StreamingDataProcessedFunction(
        IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
        : base(serviceProvider, loggerFactory.CreateLogger<StreamingDataProcessedFunction>())
    {
    }

    /// <summary>
    /// Run a function to process AAS streaming data changed event.
    /// </summary>
    /// <param name="eventData">The input EventHub events.</param>
    /// <returns></returns>
    [FunctionName(nameof(StreamingDataProcessedFunction))]
    public async Task Run([EventHubTrigger("%EVENT_HUB_NAME2%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData)
    {
        await this.RunFunctionAsync(eventData);
    }

    /// <summary>
    /// Providing default event type for incoming payloads if not specified in the payload.
    /// </summary>
    /// <returns></returns>
    protected override Type? GetDefaultIntegrationEvent()
    {
        return typeof(Models.EventHubs.Events.V1.StreamingDataProcessed);
    }
}
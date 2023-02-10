using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow;

/// <summary>
/// Function to process StreamingDataChanged event.
/// </summary>
[ExcludeFromCodeCoverage]
public class StreamingDataChangedFunction : BasicFunctionWithReturnAsync<EventData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataChangedFunction"/> class.
    /// </summary>
    /// <param name="serviceProvider">A service provider.</param>
    /// <param name="logger">A category logger.</param>
    public StreamingDataChangedFunction(
        IServiceProvider serviceProvider, ILogger<StreamingDataChangedFunction> logger)
        : base(serviceProvider, logger)
    {
    }

    /// <summary>
    /// Run the function and then return the result to another event hub.
    /// </summary>
    /// <param name="eventData">Event data</param>
    /// <param name="collector">Collector to send the event</param>
    /// <returns></returns>
    [FunctionName(nameof(StreamingDataChangedFunction))]
    public async Task Run(
                    [EventHubTrigger("%EVENT_HUB_NAME1%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData,
                    [EventHub("%EVENT_HUB_NAME2%", Connection = "EVENT_HUB_CONNECTION_STRING")] IAsyncCollector<EventData> collector)
    {
        var outputEventData = await this.RunFunctionWithReturnAsync(eventData);

        if (outputEventData == null)
        {
            return;
        }

        await collector.AddAsync(outputEventData);
    }

    /// <summary>
    /// Default event type for incoming payloads if not specified in the payload.
    /// </summary>
    /// <returns>default version for incoming payloads</returns>
    protected override Type? GetDefaultIntegrationEvent()
    {
        return typeof(Models.EventHubs.Events.V1.StreamingDataChanged);
    }
}
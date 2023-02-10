using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Attributes;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Functions.EventHubs.Extensions;
using Microsoft.Azure.Functions.Interfaces;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microsoft.Azure.Functions.StreamingDataFlow.EventHandler;

/// <summary>
/// A sample of handler to processing a StreamingDataChanged event and return an EventData.
/// </summary>
[DependencyInjection(Extends = typeof(IIntegrationEventHandlerWithReturnAsync<StreamingDataChanged, EventData>), ServiceType = ServiceLifetime.Scoped)]
public class StreamingDataChangedHandlerAsync : IIntegrationEventHandlerWithReturnAsync<StreamingDataChanged, EventData>
{
    private readonly ILogger<StreamingDataChangedHandlerAsync> logger;
    private readonly IStreamingDataFlowSettings settings;

    private readonly IAdapter<StreamingDataChanged, StreamingDataProcessed> adapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataChangedHandlerAsync"/> class.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="logger"></param>
    public StreamingDataChangedHandlerAsync(
        IStreamingDataFlowSettings settings,
        IAdapter<StreamingDataChanged, StreamingDataProcessed> adapter,
        ILogger<StreamingDataChangedHandlerAsync> logger)
    {
        this.logger = logger;
        this.settings = settings;
        this.adapter = adapter;
    }

    /// <summary>
    /// Process the event.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public async Task<EventData> Handle(StreamingDataChanged eventData)
    {
        // Whole logic to handle the event goes here.

        // Convert the event to StreamingDataProcessed.
        var streamingDataProcessed = this.adapter.Convert(eventData);

        // Create a new EventData.
        var newEventData = new EventData(JsonConvert.SerializeObject(streamingDataProcessed));
        newEventData.SetEventType(typeof(StreamingDataProcessed).FullName!);
        newEventData.CorrelationId = eventData.MetaData.TraceId;

        return await Task.FromResult<EventData>(newEventData);
    }
}
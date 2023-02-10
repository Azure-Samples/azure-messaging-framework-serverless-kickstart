using Microsoft.Azure.Functions.Attributes;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow.EventHandler;

/// <summary>
/// Sample of a handler for processing StreamingDataProcessed event.
/// </summary>
[DependencyInjection(Extends = typeof(IIntegrationEventHandlerAsync<StreamingDataProcessed>), ServiceType = ServiceLifetime.Scoped)]
public class StreamingDataProcessedHandlerAsync : IIntegrationEventHandlerAsync<StreamingDataProcessed>
{
    private readonly ILogger<StreamingDataProcessedHandlerAsync> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataProcessedHandlerAsync"/> class.
    /// </summary>
    /// <param name="logger"></param>
    public StreamingDataProcessedHandlerAsync(ILogger<StreamingDataProcessedHandlerAsync> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Handle the event.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public async Task Handle(StreamingDataProcessed eventData)
    {
        // Whole logic to handle the event goes here.
        await Task.CompletedTask;
    }
}
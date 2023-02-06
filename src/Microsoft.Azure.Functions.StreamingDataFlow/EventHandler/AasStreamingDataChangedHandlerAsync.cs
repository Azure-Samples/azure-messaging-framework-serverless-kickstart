using Microsoft.Azure.Functions.Attributes;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow.EventHandler;

/// <summary>
/// Sample of a handler for processing AasStreamingDataChanged event.
/// </summary>
[DependencyInjection(Extends = typeof(IIntegrationEventHandlerAsync<AasStreamingDataChanged>), ServiceType = ServiceLifetime.Scoped)]
public class AasStreamingDataChangedHandlerAsync : IIntegrationEventHandlerAsync<AasStreamingDataChanged>
{
    private readonly ILogger<AasStreamingDataChangedHandlerAsync> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AasStreamingDataChangedHandlerAsync"/> class.
    /// </summary>
    /// <param name="logger"></param>
    public AasStreamingDataChangedHandlerAsync(ILogger<AasStreamingDataChangedHandlerAsync> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Handle the event.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public async Task Handle(AasStreamingDataChanged eventData)
    {
        // Whole logic to handle the event goes here.
        await Task.CompletedTask;
    }
}
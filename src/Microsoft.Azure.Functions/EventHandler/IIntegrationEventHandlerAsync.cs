using Microsoft.Azure.Functions.EventHubs;

namespace Microsoft.Azure.Functions.EventHandler;

/// <summary>
/// An interface for an integration event handler which can handle an integration event asynchronously.
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
public interface IIntegrationEventHandlerAsync<in TIntegrationEvent> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// Handle method to handle the integration event.
    /// </summary>
    Task Handle(TIntegrationEvent @event);
}
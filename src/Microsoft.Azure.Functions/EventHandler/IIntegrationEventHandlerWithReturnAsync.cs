using Microsoft.Azure.Functions.EventHubs;

namespace Microsoft.Azure.Functions.EventHandler;

/// <summary>
/// An integration event handle which can handle an integration event asynchronously and return a value.
/// </summary>
/// <typeparam name="TIntegrationEvent">Acceptable event type to handle</typeparam>
/// <typeparam name="TReturn">Return type</typeparam>
public interface IIntegrationEventHandlerWithReturnAsync<in TIntegrationEvent, TReturn> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// Handle method to handle the integration event asynchronously and return a value.
    /// </summary>
    Task<TReturn> Handle(TIntegrationEvent @event);
}
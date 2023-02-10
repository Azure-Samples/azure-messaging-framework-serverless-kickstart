using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Functions.EventHubs;
using Microsoft.Azure.Functions.EventHubs.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.Functions
{
    /// <summary>
    /// An abstract class for basic Azure function.
    /// </summary>
    public abstract class BasicFunction
    {
        /// <summary>
        /// Constructor to initialize the basic function.
        /// </summary>
        /// <param name="serviceProvider">Service provider for accessing to the dependency framework</param>
        /// <param name="logger">Logger for logging</param>
        public BasicFunction(IServiceProvider serviceProvider, ILogger logger)
        {
            this.Logger = logger;
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Logger for logging.
        /// </summary>
        /// <value></value>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Service provider for accessing to the dependency framework.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Adding trace id and event type to all logs.
        /// </summary>
        /// <param name="Handler"></param>
        /// <param name="handlerInfo"></param>
        /// <returns></returns>
        protected static Dictionary<string, object> GetState((IIntegrationEventBasicHandler Handler, IntegrationEvent IntegrationEvent) handlerInfo)
        {
            return new Dictionary<string, object>
            {
                ["trace_id"] = handlerInfo.IntegrationEvent.MetaData.TraceId,
                ["event_type"] = handlerInfo.IntegrationEvent.GetType(),
            };
        }

        /// <summary>
        /// Get the handler info from the event data.
        /// </summary>
        /// <param name="eventData">Event information extracted from eventData</param>
        /// <returns>Handle which is responsible to process the integration event</returns>
        protected (IIntegrationEventBasicHandler Handler, IntegrationEvent IntegrationEvent) GetHandlerInfo(EventData eventData)
        {
            var eventTypeValue = eventData.TryGetEvent();

            if (eventTypeValue == null)
            {
                throw new Exception($"There is no event with the type of {eventData.GetEventType()}");
            }

            IIntegrationEventBasicHandler? eventHandler = this.GetInterfaceHandler(eventData, eventTypeValue);

            return (eventHandler, eventTypeValue);
        }

        /// <summary>
        /// Abstract method to extract the handler type from the event data.
        /// </summary>
        /// <param name="eventTypeValue">Integration event</param>
        /// <returns></returns>
        protected abstract Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue);

        /// <summary>
        /// Default integration event type which can be processed by the function if no event type is provided.
        /// </summary>
        /// <returns>Default event type</returns>
        protected virtual Type? GetDefaultIntegrationEvent()
        {
            return null;
        }

        /// <summary>
        /// Populate event type to the event data if it is not provided as per default event type.
        /// </summary>
        /// <param name="eventData"></param>
        protected void PopulateEventType(EventData eventData)
        {
            if (eventData.Properties.ContainsKey("EventType"))
            {
                return;
            }

            var defaultType = this.GetDefaultIntegrationEvent();

            if (defaultType is null)
            {
                return;
            }

            eventData.Properties["EventType"] = defaultType.ToString();
        }

        /// <summary>
        /// Get the handler for the event data.
        /// </summary>
        /// <param name="eventData">Event data received from the event hub</param>
        /// <param name="eventTypeValue">The integration event</param>
        /// <returns></returns>
        private IIntegrationEventBasicHandler GetInterfaceHandler(EventData eventData, IntegrationEvent? eventTypeValue)
        {
            var integrationEventHandler = this.GetInterfaceHandlerType(eventTypeValue!);
            var eventHandler = this.ServiceProvider.GetService(integrationEventHandler)! as IIntegrationEventBasicHandler;

            if (eventHandler == null)
            {
                throw new Exception($"There is no registered handler to process {integrationEventHandler}");
            }

            return eventHandler;
        }
    }
}

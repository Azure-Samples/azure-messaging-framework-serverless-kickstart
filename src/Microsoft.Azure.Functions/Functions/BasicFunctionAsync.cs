using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Functions.EventHubs;
using Microsoft.Azure.Functions.Logger;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.Functions
{
    /// <summary>
    /// Basic function to handle integration events asynchronously.
    /// </summary>
    public class BasicFunctionAsync : BasicFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicFunctionAsync"/> class.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public BasicFunctionAsync(IServiceProvider serviceProvider, ILogger logger)
            : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// Running function to handle integration events asynchronously.
        /// </summary>
        /// <param name="eventData">Event data</param>
        /// <returns>Task</returns>
        public async Task RunFunctionAsync(EventData eventData)
        {
            try
            {
                this.PopulateEventType(eventData);
                var handlerInfo = this.GetHandlerInfo(eventData);
                Dictionary<string, object> state = GetState(handlerInfo);

                using (this.Logger.BeginScope(state))
                {
                    var methodInfo = handlerInfo.Handler.GetType().GetMethod("Handle")!;
                    var task = (Task)methodInfo.Invoke(handlerInfo.Handler, new object[] { handlerInfo.IntegrationEvent })!;
                    await task;
                }
            }
            catch (Exception e)
            {
                // We need to keep processing the rest of the batch - capture this exception and continue.
                // Also, consider capturing details of the message that failed processing so it can be processed again later.
                this.Logger.FailedToProcessFunction(e);
                throw;
            }
        }

        /// <summary>
        /// Getting the handle info
        /// </summary>
        /// <param name="eventTypeValue">Integration event</param>
        /// <returns>Event handler to process the integration event</returns>
        protected override Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue)
        {
            var typeOfHandler = typeof(IIntegrationEventHandlerAsync<>);
            var integrationEventHandler = typeOfHandler.MakeGenericType(eventTypeValue.GetType());
            return integrationEventHandler;
        }
    }
}

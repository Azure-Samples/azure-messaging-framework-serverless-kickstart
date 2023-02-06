using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.EventHandler;
using Microsoft.Azure.Functions.EventHubs;
using Microsoft.Azure.Functions.Logger;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.Functions
{
    /// <summary>
    /// Basic function to process a function with return value.
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public abstract class BasicFunctionWithReturnAsync<TReturn> : BasicFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicFunctionWithReturnAsync{TReturn}"/> class.
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="logger">Logger</param>
        public BasicFunctionWithReturnAsync(IServiceProvider serviceProvider, ILogger logger)
            : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// Run the function with return value.
        /// </summary>
        /// <param name="eventData">Raw event data</param>
        /// <returns></returns>
        public virtual async Task<TReturn> RunFunctionWithReturnAsync(EventData eventData)
        {
            try
            {
                this.PopulateEventType(eventData);
                var handlerInfo = this.GetHandlerInfo(eventData);
                var state = BasicFunction.GetState(handlerInfo);

                using (this.Logger.BeginScope(state))
                {
                    var methodInfo = handlerInfo.Handler.GetType().GetMethod("Handle")!;
                    var task = (Task<TReturn>)methodInfo.Invoke(handlerInfo.Handler, new object[] { handlerInfo.IntegrationEvent })!;
                    return await task;
                }
            }
            catch (Exception e)
            {
                this.Logger.FailedToProcessFunction(e);
                throw;
            }
        }

        /// <summary>
        /// Overriding the type of interface handler to handle the integration event with return value.
        /// </summary>
        /// <param name="eventTypeValue"></param>
        /// <returns></returns>
        protected override Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue)
        {
            var typeOfHandler = typeof(IIntegrationEventHandlerWithReturnAsync<,>);
            var integrationEventHandler = typeOfHandler.MakeGenericType(eventTypeValue.GetType(), typeof(TReturn));
            return integrationEventHandler;
        }
    }
}

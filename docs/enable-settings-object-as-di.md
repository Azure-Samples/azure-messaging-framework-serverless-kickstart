# Enable Settings object as Dependency Injection

## Overview

If the business logic of your application relies heavily on the function app settings,
it is recommended to inject the settings object as part of the Microsoft IoC container.

## Register Settings object in StartUp class.

1. Make sure to add your [local settings](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs?tabs=in-process#local-settings)
or [function app settings](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs?tabs=in-process#function-app-settings)
as you would regularly do.

1. Create the settings class and add `IConfiguration` param into its constructor.

    ```c#
    using Microsoft.Extensions.Configuration;

    public class StreamingDataFlowSettings : `IStreamingDataFlowSettings`
    {
        public StreamingDataFlowSettings(IConfiguration config)
        {
            // extract setting values
            this.EventHubName = config.GetValue<string>("EVENT_HUB_NAME");
            this.StorageAccountContainer = config.GetValue<string>("STORAGE_ACCOUNT_CONTAINER");
            this.Timeout = config.GetValue<int>("TIMEOUT", 60);

            // throw exceptions if required fields are not met
            Guard.ThrowIfNull("EVENT_HUB_NAME", this.EventHubName);
            Guard.ThrowIfNull("STORAGE_ACCOUNT_CONTAINER", this.StorageAccountContainer);
        }

        public string EventHubName { get; private set; }

        public string StorageAccountContainer { get; private set; }

        public int Timeout { get; private set; }
    }
    ```

    You may use the `Guard` class to validate your input and notify if a required field is missing during runtime.

1. Register the settings class into the DI

    ```c#
    using Microsoft.Azure.Functions.StreamingDataFlow.Extensions;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var settings = new StreamingDataFlowSettings(builder.GetContext().Configuration);
            builder.Services.AddSingleton<IStreamingDataFlowSettings>(settings);
            builder.Services.RegisterServices(builder.GetContext().Configuration);
        }
    }
    ```

1. You can now inject this dependency object into your service constructors. For example:

    ```c#
    [DependencyInjection(typeof(IIntegrationEventHandlerAsync<StreamingDataChanged>), ServiceType.Scoped, 1)]  
    public class StreamingDataChangedEventHandlerAsync : IIntegrationEventHandlerAsync<StreamingDataChanged>
    {
        public StreamingDataChangedEventHandlerAsync(
            IStreamingDataFlowSettings settings)
        {
            this.settings = settings;
        }

        public async Task Handle(StreamingDataChanged eventData)
        {
            // add business logic...
        }
    }
    ```

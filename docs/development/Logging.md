# Structured Loggings

We will use structured logging across all implementation as per [Microsoft guidelines](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line#log-message-template).
In order to use a structured logging, we should follow the following principals:

- No interpolated strings are allowed when logging. Otherwise, there is no need to search the parameters used in the log event messages.
Searching of log messages with interpolated strings with parameters will be not efficient.
- All log events should have a unique id along with a name.
- Logs can be written by `ILogger<ClassName>` class directly or using extension functions annotated with a `LoggerMessage` attribute as below:

Either within a class:

```C#
public class MicrosoftDataEventHandler
{

    ILogger<MicrosoftDataEventHandler> logger;

    ...

    [LoggerMessage(
    EventId = 1001,
    EventName = "DataEventHandlerStarted",
    Message = "Started handling {eventData}",
    Level = LogLevel.Debug)]
    public static partial void DataEventHandlerStarted(JsonWellFormatter<MicrosoftDataEventChanged> eventData);

}
```

Or within an Logger extension class (preferred):
We recommend to use an extension class to implement all required extension functions for logging for better maintainability, visibility
and reusability.

```C#
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 1001,
        EventName = "DataEventHandlerStarted",
        Message = "Started handling {eventData}",
        Level = LogLevel.Debug)]
    public static partial void DataEventHandlerStarted(this ILogger logger, JsonWellFormatter<MicrosoftDataEventChanged> eventData);

```

- All structured logs will include event type along with the correlation id coming from Microsoft side automatically.
All parameters passed using structured logging will be stored as a parameter in the `customDimensions` column in the `tracing` table in Application insights.
Here are a few example on how query based on event type and correlation id or any other parameter in the structured logs in Application Insight:

```kql
traces 
| where customDimensions[ 'prop__event_type' ] == 'Microsoft.Azure.EventHubs.Events.V1.MicrosoftDataEventChanged'
 or customDimensions[ 'prop__trace_id' ] == 'a143ed71-2aa9-4b4a-bc2e-b1d8be78ebeb'
```

## Configuration for Logger

```json
"logging": {
        "fileLoggingMode": "debugOnly",
        "includeScopes": true,
        "logLevel": {
            "Function": "Debug",
            "Function.StreamingDataProcessedFunction": "Debug",
            "Function.StreamingDataChangedFunction": "Debug",
            "Microsoft": "Debug",
            "default": "None"
        },
```

The log level can be configured in the function level or based on namespace. For example, `Function.{FunctionName}`
will change the log level only for a specific function.
Or can simply controlled by specifying a namespace like `Microsoft` to change the log level in the classes inside of `Microsoft` namespace.

## Application Insight Logger Settings

```json
"applicationInsights": {
    "samplingSettings": {
        "isEnabled": true,
        "maxTelemetryItemsPerSecond" : 20,
        "evaluationInterval": "01:00:00",
        "initialSamplingPercentage": 100.0, 
        "samplingPercentageIncreaseTimeout" : "00:00:01",
        "samplingPercentageDecreaseTimeout" : "00:00:01",
        "minSamplingPercentage": 0.1,
        "maxSamplingPercentage": 100.0,
        "movingAverageRatio": 1.0,
        "includedTypes" : "Trace,Event,Exception"
    },
    "enableLiveMetrics": true,
    "enableDependencyTracking": true,
    "enablePerformanceCountersCollection": true
}
```

For Application Insights, we have used all default values that can be adjusted as per
[Microsoft guideline](https://learn.microsoft.com/en-us/azure/azure-functions/functions-host-json#applicationinsightssamplingsettings)

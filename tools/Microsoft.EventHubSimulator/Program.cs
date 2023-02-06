using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Azure.Models.EventHubs;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.EventHubSimulator;

const string settingsFile = "local.settings.json";

Config BuildConfig()
{
    if (!File.Exists(settingsFile))
    {
        throw new Exception($"{settingsFile} does not exist. Please add {settingsFile} and retry.");
    }

    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile(settingsFile)
        .AddEnvironmentVariables()
        .Build();

    var config = new Config(configuration);
    return config;
}

Console.WriteLine("Reading config...");
var config = BuildConfig();
var integrationInstanceStringified = File.ReadAllText(config.EventDetailsFile);
Console.WriteLine("Finished reading the config.");

// Create a producer client that you can use to send events to an event hub
var producerClient = new EventHubProducerClient(config.EventHubConnectionString, config.EventHubName);

// creating the batch of events
Console.WriteLine("Building the list of events to send...");
var eventDataBatch = new List<EventData>();
var eventData = new EventData(integrationInstanceStringified);
eventData.Properties.Add("EventType", config.EventType);
eventDataBatch.Add(eventData);

try
{
    // Use the producer client to send the batch of events to the event hub
    await producerClient.SendAsync(eventDataBatch);
    Console.WriteLine($"The event sent to {config.EventHubName} with the type of {config.EventType}.");
}
catch (Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}



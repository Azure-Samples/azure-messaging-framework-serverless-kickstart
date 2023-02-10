using System.Text;
using Azure.Messaging.EventHubs;
using Newtonsoft.Json;

namespace Microsoft.Azure.Functions.EventHubs.Extensions;

/// <summary>
/// Extension methods for EventData.
/// </summary>
public static class EventDataExtensions
{
    /// <summary>
    /// Returns the IntegrationEvent from the EventData.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public static IntegrationEvent? TryGetEvent(this EventData eventData)
    {
        var eventTypeName = GetEventType(eventData);
        var integrationEventType = typeof(IntegrationEvent);

        if (!eventTypeName.StartsWith("Microsoft.Azure.Models.EventHubs.Events"))
        {
            return null;
        }

        var eventType = Type.GetType($"{eventTypeName}, Microsoft.Azure.Models");

        if (eventType == null)
        {
            return null;
        }

        var jsonBody = Encoding.UTF8.GetString(eventData.EventBody);
        var eventTypeValue = JsonConvert.DeserializeObject(jsonBody, eventType);

        if (eventTypeValue == null)
        {
            return null;
        }

        var integrationEvent = (IntegrationEvent)eventTypeValue;
        integrationEvent.MetaData.PartitionKey = eventData.PartitionKey;
        integrationEvent.MetaData.QueuedTime = eventData.EnqueuedTime;

        integrationEvent.MetaData.TraceId =
            string.IsNullOrWhiteSpace(eventData.CorrelationId) ? Guid.NewGuid().ToString() : eventData.CorrelationId;

        return integrationEvent;
    }

    /// <summary>
    /// Returns the IntegrationEvent type from the EventData.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public static string GetEventType(this EventData eventData)
    {
        return (string)eventData.Properties["EventType"];
    }

    /// <summary>
    /// Setting the IntegrationEvent type to the EventData.
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="eventType"></param>
    public static void SetEventType(this EventData eventData, string eventType)
    {
        eventData.Properties["EventType"] = eventType;
    }
}

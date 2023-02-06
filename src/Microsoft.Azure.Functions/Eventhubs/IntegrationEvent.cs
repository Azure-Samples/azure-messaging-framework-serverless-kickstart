namespace Microsoft.Azure.Functions.EventHubs;

/// <summary>
/// Base class for all integration events
/// </summary>
public class IntegrationEvent
{
    /// <summary>
    /// Meta data contains all information about the event that has been received from event hub.
    /// This meta data will be used by logging framework, tracing and etc.
    /// </summary>
    public MetaData MetaData { get; set; } = new MetaData();
}
/// <summary>
/// The configuration used for Streaming Data Flow function.
/// </summary>
public interface IStreamingDataFlowSettings
{
    /// <summary>
    /// The name of the Event Hub for incoming payloads in the Streaming Data Flow.
    /// </summary>
    string EventHubName1 { get; }

    /// <summary>
    /// The name of event hub for outgoing payloads in the Streaming Data Flow after processing the incoming payloads.
    /// </summary>
    string EventHubName2 { get; }
}
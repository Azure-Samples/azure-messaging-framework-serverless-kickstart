using Microsoft.Azure.Functions.EventHubs;

namespace Microsoft.Azure.Models.EventHubs.Events.V1;

/// <summary>
/// Azure integration event whenever Streaming data changed.
/// </summary>
public class StreamingDataChanged : IntegrationEvent
{
    /// <summary>
    /// Unique identifier of the machine.
    /// </summary>
    /// <value></value>
    public string MachineId { get; set; } = string.Empty;

    /// <summary>
    /// Temperature of the machine.
    /// </summary>
    /// <value></value>
    public double Temperature { get; set; }

    /// <summary>
    /// Humidity of the machine.
    /// </summary>
    /// <value></value>
    public double Humidity { get; set; }
}

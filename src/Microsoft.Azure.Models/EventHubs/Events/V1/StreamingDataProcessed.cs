using Microsoft.Azure.Functions.EventHubs;

namespace Microsoft.Azure.Models.EventHubs.Events.V1;

/// <summary>
/// Integration event whenever streaming data processed.
/// </summary>
public class StreamingDataProcessed : IntegrationEvent
{
    /// <summary>
    /// Gets or sets the machine id.
    /// </summary>
    /// <value>
    /// The machine id.
    /// </value>
    public string MachineId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the temperature.
    /// </summary>
    /// <value>
    /// The temperature.
    /// </value>
    public double Temperature { get; set; }

    /// <summary>
    /// Gets or sets the humidity.
    /// </summary>
    /// <value>
    /// The humidity.
    /// </value>
    public double Humidity { get; set; }
}

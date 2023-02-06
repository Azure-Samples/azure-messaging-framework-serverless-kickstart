using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Services.Utils;

[ExcludeFromCodeCoverage]
public class StreamingDataFlowSettings : IStreamingDataFlowSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataFlowSettings"/> class.
    /// </summary>
    /// <param name="config">A configuration.</param>
    public StreamingDataFlowSettings(IConfiguration config)
    {
        this.EventHubName1 = config.GetValue<string>("EVENT_HUB_NAME1");
        this.EventHubName2 = config.GetValue<string>("EVENT_HUB_NAME2");

        Guard.ThrowIfNull("EVENT_HUB_NAME1", this.EventHubName1);
        Guard.ThrowIfNull("EVENT_HUB_NAME2", this.EventHubName2);
    }

    /// <inheritdoc />
    public string EventHubName1 { get; private set; }

    /// <inheritdoc />
    public string EventHubName2 { get; private set; }
}
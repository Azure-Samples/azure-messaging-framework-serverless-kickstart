using Microsoft.Azure.Functions.Attributes;
using Microsoft.Azure.Functions.Interfaces;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Adapters;

/// <summary>
/// This class is an example of an adapter that converts StreamingDataChanged to AasStreamingDataChanged.
/// </summary>
[DependencyInjection(Extends = typeof(IAdapter<StreamingDataChanged, AasStreamingDataChanged>), ServiceType = ServiceLifetime.Singleton)]
public class StreamingDataToAasAdapter : IAdapter<StreamingDataChanged, AasStreamingDataChanged>
{
    private readonly ILogger<StreamingDataToAasAdapter> logger;

    public StreamingDataToAasAdapter(
        ILogger<StreamingDataToAasAdapter> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Convert StreamingDataChanged to AasStreamingDataChanged.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public AasStreamingDataChanged Convert(StreamingDataChanged from)
    {
        var aasStreamingDataChanged = new AasStreamingDataChanged()
        {
            MachineId = from.MachineId,
            Temperature = from.Temperature,
            Humidity = from.Humidity,
            MetaData = from.MetaData,
        };

        return aasStreamingDataChanged!;
    }
}

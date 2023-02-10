using Microsoft.Azure.Functions.Attributes;
using Microsoft.Azure.Functions.Interfaces;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Adapters;

/// <summary>
/// This class is an example of an adapter that converts StreamingDataChanged to StreamingDataProcessed.
/// </summary>
[DependencyInjection(Extends = typeof(IAdapter<StreamingDataChanged, StreamingDataProcessed>), ServiceType = ServiceLifetime.Singleton)]
public class StreamingDataToProcessedAdapter : IAdapter<StreamingDataChanged, StreamingDataProcessed>
{
    private readonly ILogger<StreamingDataToProcessedAdapter> logger;

    public StreamingDataToProcessedAdapter(
        ILogger<StreamingDataToProcessedAdapter> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Convert StreamingDataChanged to StreamingDataProcessed.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public StreamingDataProcessed Convert(StreamingDataChanged from)
    {
        var streamingDataProcessed = new StreamingDataProcessed()
        {
            MachineId = from.MachineId,
            Temperature = from.Temperature,
            Humidity = from.Humidity,
            MetaData = from.MetaData,
        };

        return streamingDataProcessed!;
    }
}

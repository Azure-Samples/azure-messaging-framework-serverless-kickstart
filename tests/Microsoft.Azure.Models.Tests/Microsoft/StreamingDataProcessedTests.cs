using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.TestFramework;

namespace Microsoft.Azure.Models.Tests.Microsoft;

public class StreamingDataProcessedTests : BaseTest
{
    [Theory]
    [ComplexLoadInlineData("Microsoft/Data/StreamingData/StreamingDataProcessedExample.json")]
    [Trait("Area", "ProcessedStreamingData")]
    [Trait("Category", "UnitTest")]
    public void WhenProcessedStreamingDataReceived_ThenValidateData(
        StreamingDataProcessed processedStreamingDataActual,
        string expectedMachineId,
        double expectedTemperature,
        double expectedHumidity)
    {
        // arrange

        // act

        // assert
        Assert.NotNull(processedStreamingDataActual);
        Assert.Equal(processedStreamingDataActual.MachineId, expectedMachineId);
        Assert.Equal(processedStreamingDataActual.Temperature, expectedTemperature);
        Assert.Equal(processedStreamingDataActual.Humidity, expectedHumidity);
    }
}
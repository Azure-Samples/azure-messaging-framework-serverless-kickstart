using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.TestFramework;

namespace Microsoft.Azure.Models.Tests.Microsoft;

public class StreamingDataChangedTests : BaseTest
{
    [Theory]
    [ComplexLoadInlineData("Microsoft/Data/StreamingData/StreamingDataChangedExample.json")]
    [Trait("Area", "StreamingData")]
    [Trait("Category", "UnitTest")]
    public void WhenStreamingDataReceived_ThenValidateData(
        StreamingDataChanged streamingDataActual,
        string expectedMachineId,
        double expectedTemperature,
        double expectedHumidity)
    {
        // arrange

        // act

        // assert
        Assert.NotNull(streamingDataActual);
        Assert.Equal(streamingDataActual.MachineId, expectedMachineId);
        Assert.Equal(streamingDataActual.Temperature, expectedTemperature);
        Assert.Equal(streamingDataActual.Humidity, expectedHumidity);
    }
}
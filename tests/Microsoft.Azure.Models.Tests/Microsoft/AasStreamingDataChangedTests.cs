using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.TestFramework;

namespace Microsoft.Azure.Models.Tests.Microsoft;

public class AasStreamingDataChangedTests : BaseTest
{
    [Theory]
    [ComplexLoadInlineData("Microsoft/Data/StreamingData/AasStreamingDataChangedExample.json")]
    [Trait("Area", "aasStreamingData")]
    [Trait("Category", "UnitTest")]
    public void WhenAasStreamingDataReceived_ThenValidateData(
        AasStreamingDataChanged aasStreamingDataActual,
        string expectedMachineId,
        double expectedTemperature,
        double expectedHumidity)
    {
        // arrange

        // act

        // assert
        Assert.NotNull(aasStreamingDataActual);
        Assert.Equal(aasStreamingDataActual.MachineId, expectedMachineId);
        Assert.Equal(aasStreamingDataActual.Temperature, expectedTemperature);
        Assert.Equal(aasStreamingDataActual.Humidity, expectedHumidity);
    }
}
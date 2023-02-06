using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Azure.Functions.StreamingDataFlow.EventHandler;
using Microsoft.TestFramework;
using Microsoft.Azure.Functions.StreamingDataFlow.Adapters;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Tests.Handlers;

public class StreamingDataChangedHandlerTests : BaseTest
{
    [Fact]
    public async Task GivenHandleCalled_WhenServiceHasResult_ThenSuccessfullyReturnsAasEventData()
    {
        // Arrange
        var StreamingDataChanged = new StreamingDataChanged();
        var aasStreamingDataChanged = new AasStreamingDataChanged();
        ILogger<StreamingDataChangedHandlerAsync> logger = GetLogger<StreamingDataChangedHandlerAsync>();
        var settings = new Mock<IStreamingDataFlowSettings>();
        var adapter = new StreamingDataToAasAdapter(GetLogger<StreamingDataToAasAdapter>());
        var StreamingDataChangedHandler = new StreamingDataChangedHandlerAsync(settings.Object, adapter, logger);
        StreamingDataChanged.MetaData.TraceId = "traceid1";

        // Act
        var evt = await StreamingDataChangedHandler.Handle(StreamingDataChanged);
        // Assert
        Assert.NotNull(evt);
        Assert.Equal(StreamingDataChanged.MetaData.TraceId, evt.CorrelationId);
        Assert.Equal(typeof(AasStreamingDataChanged).ToString(), evt.Properties["EventType"].ToString());
        Mock.VerifyAll();
    }
}
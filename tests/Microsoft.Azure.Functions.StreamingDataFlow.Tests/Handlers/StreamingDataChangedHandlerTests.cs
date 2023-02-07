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
    public async Task GivenHandleCalled_WhenServiceHasResult_ThenSuccessfullyReturnsProcessedEventData()
    {
        // Arrange
        var StreamingDataChanged = new StreamingDataChanged();
        var StreamingDataProcessed = new StreamingDataProcessed();
        ILogger<StreamingDataChangedHandlerAsync> logger = GetLogger<StreamingDataChangedHandlerAsync>();
        var settings = new Mock<IStreamingDataFlowSettings>();
        var adapter = new StreamingDataToProcessedAdapter(GetLogger<StreamingDataToProcessedAdapter>());
        var StreamingDataChangedHandler = new StreamingDataChangedHandlerAsync(settings.Object, adapter, logger);
        StreamingDataChanged.MetaData.TraceId = "traceid1";

        // Act
        var evt = await StreamingDataChangedHandler.Handle(StreamingDataChanged);
        // Assert
        Assert.NotNull(evt);
        Assert.Equal(StreamingDataChanged.MetaData.TraceId, evt.CorrelationId);
        Assert.Equal(typeof(StreamingDataProcessed).ToString(), evt.Properties["EventType"].ToString());
        Mock.VerifyAll();
    }
}
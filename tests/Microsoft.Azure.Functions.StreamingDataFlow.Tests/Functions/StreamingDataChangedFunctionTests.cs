using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.EventHubs.Extensions;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.Azure.WebJobs;
using Microsoft.TestFramework;
using Moq;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Tests.Functions;

public class StreamingDataChangedFunctionTests : BaseTest
{
    [Fact]
    [Trait("Area", "StreamingDataChanged")]
    [Trait("Category", "UnitTest")]
    public async Task GivenFunctionTrigered_WhenHandlerReturnProccessedResult_ThenFunctionSendEvent()
    {
        // arrange
        var inputEvent = new EventData();

        var processedEventData = new EventData();
        processedEventData.SetEventType(typeof(StreamingDataProcessed).FullName);

        var collector = new Mock<IAsyncCollector<EventData>>();

        collector.Setup( collector => collector.AddAsync(processedEventData, default(CancellationToken)));

        var serviceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
        var function = new Mock<StreamingDataChangedFunction>(
            serviceProvider.Object, GetLogger<StreamingDataChangedFunction>());

        function.CallBase = true;
        function.Setup( func => func.RunFunctionWithReturnAsync(It.IsAny<EventData>())).Returns(Task.FromResult<EventData>(processedEventData));

        // act
        await function.Object.Run(inputEvent, collector.Object);

        // assert
        Mock.VerifyAll();
    }

    [Fact]
    [Trait("Area", "StreamingDataToChanged")]
    [Trait("Category", "UnitTest")]
    public async Task GivenFunctionTrigered_WhenHandleReturnNull_ThenFunctionSendNoEvent()
    {
        // arrange
        var inputEvent = new EventData();

        var collector = new Mock<IAsyncCollector<EventData>>();
        var adxCollector = new Mock<IAsyncCollector<EventData>>();

        var serviceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
        var function = new Mock<StreamingDataChangedFunction>(
            serviceProvider.Object, GetLogger<StreamingDataChangedFunction>());

        function.CallBase = true;
        function.Setup(func => func.RunFunctionWithReturnAsync(It.IsAny<EventData>())).Returns(Task.FromResult<EventData>(null!));

        // act
        await function.Object.Run(inputEvent, null);
        // assert
        collector.Verify( collector => collector.AddAsync(It.IsAny<EventData>(), default(CancellationToken)), Times.Never);
    }
}

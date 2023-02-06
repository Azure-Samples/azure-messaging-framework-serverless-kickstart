using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Moq;
using Microsoft.Azure.Models.EventHubs.Events.V1;
using Microsoft.TestFramework;
using Microsoft.Azure.Functions.EventHubs.Extensions;

namespace Microsoft.Azure.Functions.StreamingDataFlow.Tests.Functions;

public class StreamingDataChangedFunctionTests : BaseTest
{
    [Fact]
    [Trait("Area", "StreamingDataToAdt")]
    [Trait("Category", "UnitTest")]
    public async Task GivenFunctionTrigered_WhenHandlerReturnAasResult_ThenFunctionSendEvent()
    {
        // arrange
        var inputEvent = new EventData();

        var aasEventData = new EventData();
        aasEventData.SetEventType(typeof(AasStreamingDataChanged).FullName);

        var adtCollector = new Mock<IAsyncCollector<EventData>>();

        adtCollector.Setup( collector => collector.AddAsync(aasEventData, default(CancellationToken)));

        var serviceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
        var function = new Mock<StreamingDataChangedFunction>(
            serviceProvider.Object, GetLogger<StreamingDataChangedFunction>());

        function.CallBase = true;
        function.Setup( func => func.RunFunctionWithReturnAsync(It.IsAny<EventData>())).Returns(Task.FromResult<EventData>(aasEventData));

        // act
        await function.Object.Run(inputEvent, adtCollector.Object);

        // assert
        Mock.VerifyAll();
    }

    [Fact]
    [Trait("Area", "StreamingDataToAas")]
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

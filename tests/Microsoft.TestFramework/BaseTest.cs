using Microsoft.Extensions.Logging;

namespace Microsoft.TestFramework
{
    public class BaseTest{
        public void Test<TArrangeResult, TActResult>(
            Func<TArrangeResult> arrange,
            Func<TArrangeResult, TActResult> act,
            Action<TActResult> assert)
        {
            assert(act(arrange()));
        }

        public ILoggerFactory GetLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
        }

        public ILogger<T> GetLogger<T>()
        {
            return GetLoggerFactory().CreateLogger<T>();
        }
    }
}
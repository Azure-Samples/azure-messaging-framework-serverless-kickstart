using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Microsoft.Azure.Functions.StreamingDataFlow.Startup))]

namespace Microsoft.Azure.Functions.StreamingDataFlow;

/// <summary>
/// This startup class allows for dependency injection.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    /// <summary>
    /// This method is called during startup to allow for the registration of services.
    /// </summary>
    /// <param name="builder">The builder that contains the service collection.</param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = this.GetConfiguration(builder);

        // config
        var settings = new StreamingDataFlowSettings(config);
        builder.Services.AddSingleton<IStreamingDataFlowSettings>(settings);

        builder.Services.RegisterServices(this.GetConfiguration(builder));
    }

    public virtual IConfiguration GetConfiguration(IFunctionsHostBuilder builder)
    {
        return builder.GetContext().Configuration;
    }
}
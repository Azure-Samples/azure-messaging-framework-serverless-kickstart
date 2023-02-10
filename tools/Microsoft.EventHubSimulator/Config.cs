using Microsoft.Extensions.Configuration;

namespace Microsoft.EventHubSimulator;

class Config
{
    private readonly IConfiguration configuration;
    public Config(IConfiguration configuration)
    {
        this.configuration = configuration;

        this.EventHubConnectionString = this.TryGetStringConfigValue("EVENT_HUB_CONNECTION_STRING");
        this.EventHubName = this.TryGetStringConfigValue("EVENT_HUB_NAME");
        this.EventDetailsFile = this.TryGetStringConfigValue("EVENT_DETAILS_FILE");
        this.EventType = this.TryGetEventTypeConfigValue("EVENT_TYPE");
    }

    private string TryGetStringConfigValue(string key)
    {
        var configValue = this.configuration.GetValue<string>(key);
        if (string.IsNullOrEmpty(configValue))
        {
            throw new Exception($"{key} must be defined");
        }

        return configValue;
    }

    private string TryGetEventTypeConfigValue(string key)
    {
        return this.configuration.GetValue<string>(key);
    }

    /// <summary>
    /// Gets or sets the event hub connection string.
    /// </summary>
    public string EventHubConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event hub name.
    /// </summary>
    public string EventHubName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event details file.
    /// </summary>
    public string EventDetailsFile { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event type.
    /// </summary>
    public string EventType { get; set; } = string.Empty;
}
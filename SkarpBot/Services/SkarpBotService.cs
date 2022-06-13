namespace SkarpBot.Services
{
    using Discord.Addons.Hosting;
    using Discord.WebSocket;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SkarpBot.Data;

    /// <summary>
    /// Кастомная имплементация <see cref="DiscordClientService"/> для SkarpBot.
    /// </summary>
    public abstract class SkarpBotService : DiscordClientService
    {
        public readonly DiscordSocketClient Client;
        public readonly ILogger<DiscordClientService> Logger;
        public readonly IConfiguration Configuration;
        public readonly DataAccessLayer DataAccessLayer;

        public SkarpBotService(DiscordSocketClient client, ILogger<DiscordClientService> logger, IConfiguration configuration, DataAccessLayer dataAccessLayer)
            : base(client, logger)
        {
            Client = client;
            Logger = logger;
            Configuration = configuration;
            DataAccessLayer = dataAccessLayer;
        }
    }
}

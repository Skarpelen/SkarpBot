namespace SkarpBot
{
    using Discord;
    using Discord.Commands;
    using Discord.Interactions;
    using Discord.WebSocket;
    using InteractionFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SkarpBot.Data;
    using SkarpBot.Data.Context;
    using SkarpBot.Data.Models;
    using SkarpBot.Logger;
    using System.Threading.Tasks;
    using Utf8Json;

    /// <summary>
    /// Сердце бота.
    /// </summary>
    public class Program
    {
        private DiscordSocketClient _client;

        // Program entry point
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()

            // this will be used more later on
            .SetBasePath(AppContext.BaseDirectory)

            // I chose using YML files for my config data as I am familiar with them
            .AddJsonFile("appsettings.json")
            .Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services

            // Add the configuration to the registered services
            .AddSingleton(config)

            // Add DataAccessLayer as a singleton
            .AddSingleton<DataAccessLayer>()

            // Add SkarpBotDbContext as a scoped service
            .AddDbContext<SkarpBotDbContext>(options => options.UseSqlite(config.GetConnectionString("DjangoConnection")))

            // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All,
                LogGatewayIntentWarnings = false,
                AlwaysDownloadUsers = true,
                LogLevel = LogSeverity.Debug,
            }))

            // Adding console logging
            .AddTransient<ConsoleLogger>()

            // Used for slash commands and their registration with Discord
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))

            // Required to subscribe to the various client events used in conjunction with Interactions
            .AddSingleton<InteractionHandler>()

            // Adding the prefix Command Service
            .AddSingleton(x => new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                DefaultRunMode = Discord.Commands.RunMode.Async,
            }))

            // Adding the prefix command handler
            .AddSingleton<PrefixHandler>())
            .Build();

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = provider.GetRequiredService<PrefixHandler>();

            // prefixCommands.AddModule<Modules.PrefixGeneral>();
            await prefixCommands.InitializeAsync();

            // Subscribe to client log events
            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            // Subscribe to slash command log events
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () =>
            {
                var registrationTask = commands.RegisterCommandsGloballyAsync(true);
                var delayTask = Task.Delay(TimeSpan.FromSeconds(10)); // Установите желаемое время ожидания

                var completedTask = await Task.WhenAny(registrationTask, delayTask);
                if (completedTask == delayTask)
                {
                    // Прошло время ожидания, выполните соответствующие действия
                    // например, выведите сообщение об ошибке или предпримите другие меры
                }
            };

            await _client.LoginAsync(TokenType.Bot, config["Token"]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}

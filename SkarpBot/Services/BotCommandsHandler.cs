namespace SkarpBot.Services
{
    using System;
    using System.Reflection;
    using Discord;
    using Discord.Addons.Hosting;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SkarpBot.Data;

    /// <summary>
    /// Основа для команд.
    /// </summary>
    public class BotCommandsHandler : SkarpBotService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotCommandsHandler"/> class.
        /// </summary>
        /// <param name="provider">_provider.</param>
        /// <param name="client">_client.</param>
        /// <param name="service">_service.</param>
        /// <param name="configuration">config.</param>
        /// <param name="logger">log.</param>
        public BotCommandsHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration, ILogger<DiscordClientService> logger, DataAccessLayer dataAccessLayer)
            : base(client, logger, configuration, dataAccessLayer)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (result.IsSuccess)
            {
                return;
            }

            await commandContext.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            var argPos = 0;
            var user = message.Author as SocketGuildUser;
            var prefix = DataAccessLayer.GetPrefix(user.Guild.Id);
            if (!message.HasStringPrefix(prefix, ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                CaseCommands(message);
                return;
            }

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private Task CaseCommands(SocketMessage msg)
        {
            string[] str = msg.Content.Split();

            switch (str[0])
            {
                case "лецгер":
                    msg.Channel.SendMessageAsync("душнила");
                    //if (((SocketGuildUser)msg.Author).Roles.Any(r => r.Id == 926417680347570176))
                    //    msg.Channel.SendMessageAsync("И это факт");
                    break;

                case "крох":
                    msg.Channel.SendMessageAsync("https://tenor.com/view/behead-chopping-block-guillotine-bonk-gif-20619536");
                    break;

                case "кристина":
                    msg.Channel.SendMessageAsync("https://tenor.com/view/mgr-raiden-senator-armstrong-metal-gear-rising-punch-pog-gif-25287262");
                    break;

                case "павук":
                    msg.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/940699969030086736/968568975598420059/1.jpg");
                    break;

                case "слап":
                    msg.Channel.SendMessageAsync("<@527049437252681749>");
                    break;

                case "буграш":
                    string[] mushrooms = {"https://tenor.com/view/mushroom-mushroom-movie-high-five-gif-13655765",
                                                          "https://tenor.com/view/mushroom-mushroom-movie-dance-gif-13655792",
                                                          "https://tenor.com/view/mushroom-mushrooms-sticker-angry-tribe-gif-18025901",
                                                          "https://tenor.com/view/mushroom-mushroom-movie-dance-happy-gif-13655817",
                                                          "https://tenor.com/view/mushroom-dance-dancing-gif-13803320" };
                    Random rnd = new();
                    int value = rnd.Next(0, 4);
                    msg.Channel.SendMessageAsync(mushrooms[value]);
                    break;

                case "вождь":
                    msg.Channel.SendMessageAsync("<@395574258698289162> я тебя слапнул чтоби просто сказать что ти" +
                        " очень крутой 👉👈");
                    break;

                case "ревеж":
                    msg.Channel.SendMessageAsync("Погромист https://cdn.discordapp.com/attachments/968239152409747526/979768146888126504/unknown.png");
                    break;
            }

            return Task.CompletedTask;
        }
    }
}

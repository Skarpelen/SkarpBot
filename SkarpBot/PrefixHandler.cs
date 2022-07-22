namespace SkarpBot
{
    using Discord;
    using Discord.Addons.Hosting;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SkarpBot.Data;
    using SkarpBot.Services;
    using System;

    public class PrefixHandler : SkarpBotService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _configuration;


        public PrefixHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration, ILogger<DiscordClientService> logger, DataAccessLayer dataAccessLayer)
            : base(client, logger, configuration, dataAccessLayer)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.MessageReceived += OnMessageReceived;
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
            var prefix = "!";//DataAccessLayer.GetPrefix(user.Guild.Id);
            if (!message.HasStringPrefix(prefix, ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                CaseCommands(message);
                return;
            }

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        // Retrieve client and CommandService instance via ctor

        public async Task InitializeAsync()
        {
            _client.MessageReceived += OnMessageReceived;
        }
        public void AddModule<T>()
        {
            _service.AddModuleAsync<T>(null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            SocketGuildUser socketGuildUser = message.Author as SocketGuildUser;
            // manage_message = socketGuildUser.GuildPermissions.ViewAuditLog;
            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(_configuration["Prefix"][0], ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _service.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }

        private Task CaseCommands(SocketMessage msg)
        {
            string[] str = msg.Content.Split();
            Random rnd = new();


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

                case "-":
                    string[] answers = { "Воскликнул ", "Произнес", "Задумался", "Прокричал", "Сказал", "Заорал", "Заверещал", "Заплакал", "Пропел", "Пропищал", "Проныл" };
                    int idx = rnd.Next(answers.Length);
                    msg.Channel.SendMessageAsync(answers[idx] + "<@" + msg.Author.Id + '>');
                    break;
            }

            return Task.CompletedTask;
        }
    }
}

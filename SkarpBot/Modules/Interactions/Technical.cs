namespace SkarpBot.Modules
{
    using Discord;
    using Discord.Interactions;
    using Discord.WebSocket;
    using SkarpBot.Data;

    public partial class SlashGeneral : SkarpBotInteractionsModuleBase
    {
        // You can use a number of parameter types in you Slash Command handlers (string, int, double, bool, IUser, IChannel, IMentionable, IRole, Enums) by default. Optionally,
        // you can implement your own TypeConverters to support a wider range of parameter types. For more information, refer to the library documentation.
        // Optional method parameters(parameters with a default d100Value) also will be displayed as optional on Discord.

        // [Summary] lets you customize the name and the description of a parameter
        [SlashCommand("echo", "Repeat the input")]
        public async Task Echo(string echo, [Summary(description: "mention the user")] bool mention = false)
            => await RespondAsync(echo + (mention ? Context.User.Mention : string.Empty));

        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        public async Task GreetUserAsync()
            => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!", ephemeral: true);

        [SlashCommand("bitrate", "Gets the bitrate of a specific voice channel.")]
        public async Task GetBitrateAsync([ChannelTypes(ChannelType.Voice, ChannelType.Stage)] IChannel channel)
            => await RespondAsync(text: $"This voice channel has a bitrate of {(channel as IVoiceChannel).Bitrate}");

        // Use [ComponentInteraction] to handle message component interactions. Message component interaction with the matching customId will be executed.
        // Alternatively, you can create a wild card pattern using the '*' character. Interaction Service will perform a lazy regex search and capture the matching strings.
        // You can then access these capture groups from the method parameters, in the order they were captured. Using the wild card pattern, you can cherry pick component interactions.
        [ComponentInteraction("musicSelect:*,*")]
        public async Task ButtonPress(string id, string name)
        {
            // ...
            await RespondAsync($"Playing song: {name}/{id}");
        }

        // This command will greet target user in the channel this was executed in.
        [UserCommand("greet")]
        public async Task GreetUserAsync(IUser user)
            => await RespondAsync(text: $":wave: {Context.User} said hi to you, <@{user.Id}>!");

        // Pins a message in the channel it is in.
        [MessageCommand("pin")]
        public async Task PinMessageAsync(IMessage message)
        {
            // make a safety cast to check if the message is ISystem- or IUserMessage
            if (message is not IUserMessage userMessage)
            {
                await RespondAsync(text: ":x: You cant pin system messages!");
            }

            // if the pins in this channel are equal to or above 50, no more messages can be pinned.
            else if ((await Context.Channel.GetPinnedMessagesAsync()).Count >= 50)
            {
                await RespondAsync(text: ":x: You cant pin any more messages, the max has already been reached in this channel!");
            }
            else
            {
                await userMessage.PinAsync();
                await RespondAsync(":white_check_mark: Successfully pinned message!");
            }
        }

        [SlashCommand("join", "jidsda")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();
        }

        [SlashCommand("тестис", "тестис в ебало")]
        public async Task Testo()
        {
            var equipButton = new ButtonBuilder()
            {
                Label = "Экипировать/снять",
                CustomId = "equip-button",
                Style = ButtonStyle.Success,
            };

            var destroyButton = new ButtonBuilder()
            {
                Label = "Убрать",
                CustomId = "destroy-button",
                Style = ButtonStyle.Danger,
            };

            var menu = new SelectMenuBuilder()
            {
                CustomId = "menu",
                Placeholder = "Sample Menu",
            };

            var weapons = DataAccessLayer.GetInventoryWeapons(Context.User.Id, Context.Guild.Id);
            var grenades = DataAccessLayer.GetInventoryGrenades(Context.User.Id, Context.Guild.Id);
            var briefcaseEmoji = new Emoji("\U0001F4BC");
            var handEmoji = new Emoji("\U0001F590");
            foreach (var weapon in weapons)
            {
                menu.AddOption(FirstLetterToUpper(weapon.WeaponName), $"{weapon.WeaponId}.W",
                    $"Количество оставшихся снарядов в магазине: {weapon.CurrentAmmo}", weapon.InventoryName == "equipped" ? handEmoji : briefcaseEmoji);
            }

            foreach (var grenade in grenades)
            {
                menu.AddOption(FirstLetterToUpper(grenade.GrenadeName), $"{grenade.GrenadeId}.G",
                    $"Количество гранат в инвентаре: {grenade.Amount}", briefcaseEmoji);
            }

            var component = new ComponentBuilder();

            component.WithSelectMenu(menu);
            component.WithButton(equipButton);
            component.WithButton(destroyButton);

            await RespondAsync("testing", components: component.Build());
        }

        [ComponentInteraction("menu")]
        public async Task MyMenuHandle1r(string value)
        {
            await DataAccessLayer.MenuPicker(Context.User.Id, Context.Guild.Id, value);
            await RespondAsync("Как же я не хочу чтобы это сообщение оставалось в боте, но к сожалению я никак не могу от него избавиться", null, false, true);
        }

        [ComponentInteraction("equip-button")]
        public async Task Equipper()
        {
            string answer = await DataAccessLayer.EquipWeapon(Context.User.Id, Context.Guild.Id);
            await RespondAsync(answer);
        }

        [ComponentInteraction("destroy-button")]
        public async Task Destroyer()
        {
            string answer = await DataAccessLayer.DestroyInventory(Context.User.Id, Context.Guild.Id);
            await RespondAsync(answer, null, false, true);
        }

        public async Task MyMenuHandler(SocketMessageComponent arg)
        {
            var text = string.Join(", ", arg.Data.Values);
            await DataAccessLayer.MenuPicker(Context.User.Id, Context.Guild.Id, text);
            await arg.RespondAsync("Как же я не хочу чтобы это сообщение оставалось в боте, но к сожалению я никак не могу от него избавиться", null, false, true);
        }

        private string FirstLetterToUpper(string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }

            return str.ToUpper();
        }
    }
}

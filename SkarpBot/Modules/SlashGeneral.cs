namespace SkarpBot.Modules
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Interactions;
    using InteractionFramework;
    using SkarpBot.Data;
    using SkarpBot.OnlyWar;

    // Interation modules must be public and inherit from an IInterationModuleBase
    public class SlashGeneral : SkarpBotInteractionsModuleBase
    {
        // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
        public InteractionService Commands { get; set; }

        private InteractionHandler _handler;

        // Constructor injection is also a valid way to access the dependencies
        public SlashGeneral(InteractionHandler handler, DataAccessLayer dataAccessLayer)
            : base(dataAccessLayer)
        {
            _handler = handler;
        }

        // You can use a number of parameter types in you Slash Command handlers (string, int, double, bool, IUser, IChannel, IMentionable, IRole, Enums) by default. Optionally,
        // you can implement your own TypeConverters to support a wider range of parameter types. For more information, refer to the library documentation.
        // Optional method parameters(parameters with a default value) also will be displayed as optional on Discord.

        // [Summary] lets you customize the name and the description of a parameter
        [SlashCommand("echo", "Repeat the input")]
        public async Task Echo(string echo, [Discord.Interactions.Summary(description: "mention the user")] bool mention = false)
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

        // Select Menu interactions, contain ids of the menu options that were selected by the user. You can access the option ids from the method parameters.
        // You can also use the wild card pattern with Select Menus, in that case, the wild card captures will be passed on to the method first, followed by the option ids.
        [ComponentInteraction("roleSelect")]
        public async Task RoleSelect(string[] selections)
        {
            throw new NotImplementedException();
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
                await RespondAsync(text: ":x: You cant pin system messages!");

            // if the pins in this channel are equal to or above 50, no more messages can be pinned.
            else if ((await Context.Channel.GetPinnedMessagesAsync()).Count >= 50)
                await RespondAsync(text: ":x: You cant pin any more messages, the max has already been reached in this channel!");

            else
            {
                await userMessage.PinAsync();
                await RespondAsync(":white_check_mark: Successfully pinned message!");
            }
        }

        [SlashCommand("хп", "Выдает хп из бд")]
        public async Task HpGet()
        {
            await RespondAsync(DataAccessLayer.GetHp(Context.User.Id));
        }

        [SlashCommand("регистрация", "Регистрирует пользователя в системе")]
        public async Task Register(string aType)
        {
            Armour armour = new Armour(aType);
            if (armour.error)
            {
                await ReplyAsync("Некорректное название брони");
                return;
            }

            await DataAccessLayer.Nuller(Context.User.Id, aType);
            await RespondAsync("Что то случилось");
        }

        [SlashCommand("установить", "Изменяет количество хп у выбранной части тела")]
        public async Task SetHp(int point, int val)
        {
            val *= -1;
            await DataAccessLayer.ChangeHp(Context.User.Id, point, val);
            await RespondAsync("Значения применены");
        }

        [SlashCommand("изменить", "Изменяет количество хп у пользователя")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetHp(IUser user, int point, int val)
        {
            val *= -1;
            await DataAccessLayer.ChangeHp(user.Id, point, val);
            await RespondAsync("Значения применены");
        }

        /// <summary>
        /// Команда, преобразующая стандартное время в десятичную систему времен французской революции.
        /// </summary>
        /// <param name="str">Отправляемое на обработку время.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("время", "Ну как там во Франции")]
        public async Task TimeAsync(string str)
        {
            Regex rgx = new(@"[0-23]:[0-59]");
            if (rgx.IsMatch(str))
            {
                await RespondAsync(FrenchTime(str));
            }
            else
            {
                await RespondAsync("Неправильный формат времени (00:00)");
            }
        }

        /// <summary>
        /// Команда для расстрела слапами.
        /// </summary>
        /// <param name="str">Жертва.</param>
        /// <param name="len">Количество слапов.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("расстрелять", "Почувствуй себя НКВДшником")]
        public Task SlapKill(string str, int len)
        {
            Shooting(str, len);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Стрельба в обычном режиме.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="mode">Режим стрельбы.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="user">Цель.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("стрелять", "Стреляет")]
        public async Task FireWeapon(int accuracy, int mode, string wType, IUser user, int aim = 1)
        {
            if (mode > 2 || mode < 0 || accuracy < 0)
            {
                await RespondAsync("Неправильные значения режима стрельбы/меткости");
                return;
            }

            var gunFire = new FireWeapon(accuracy, mode, wType, DataAccessLayer.GetArmour(user.Id), user.Id, aim);
            var shoot = await gunFire.Shoot(DataAccessLayer);
            await RespondAsync(shoot);

            /*gunFire.Shoot(out point, out dmg);
            await DataAccessLayer.ChangeHp(Context.User.Id, point, dmg);
            await ReplyAsync(DataAccessLayer.GetHp(Context.User.Id));*/
        }

        /// <summary>
        /// Прицельный выстрел.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="user">Цель.</param>
        /// <param name="aimPoint">Куда надо попасть.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("прицел", "Прицеливает")]
        public async Task FireWeapon(int accuracy, string wType, IUser user, string aimpoint, int aim = 1)
        {
            if (accuracy < 0)
            {
                await RespondAsync("Неправильное значение меткости");
                return;
            }

            var gunFire = new FireWeapon(accuracy, wType, DataAccessLayer.GetArmour(user.Id), user.Id, aimpoint, aim);
            var shoot = await gunFire.CalledShot(DataAccessLayer);
            await RespondAsync(shoot);
        }

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Оружие для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("статы", "Статы выбранного оружия")]
        public async Task Stats(string wType)
        {
            var gun = new FireWeapon(wType);
            var grenade = new Grenade(wType);
            var knife = new Melee(wType);

            if (!gun.error)
            {
                await RespondAsync(gun.WriteQualitiesFire());
            }
            else if (!grenade.error)
            {
                await RespondAsync(grenade.WriteQualitiesGrenade());
            }
            else if (!knife.error)
            {
                await RespondAsync(knife.WriteQualitiesMelee());
            }
            else
            {
                RespondAsync("Ошибка в названии оружия");
            }
        }

        /// <summary>
        /// Бросок гранаты в обычном режиме.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="wType">Гранта стреляющего.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("бросить", "Бросает")]
        public async Task GrenadeWeapon(int accuracy, string wType)
        {
            if (accuracy < 0)
            {
                await RespondAsync("Неправильное значение меткости");
                return;
            }

            var grenadeThrow = new Grenade(accuracy, wType);
            await RespondAsync(grenadeThrow.GrenadeThrow());
        }

        [SlashCommand("резать", "Режет")]
        public async Task MeleeWeapon(int accuracy, string wType, IUser user)
        {

            if (accuracy < 0)
            {
                await RespondAsync("Неправильное значение меткости");
                return;
            }
            var knifePower = new Melee(accuracy, wType, DataAccessLayer.GetArmour(user.Id), user.Id);
            var swing = await knifePower.Swing(DataAccessLayer);
            await RespondAsync(swing);
        }

        /// <summary>
        /// Информация об имеющихся вариантах оружия.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("инфо", "Как пользоваться ботом")]
        public async Task Info()
        {
            await OWInfo();
        }

        [SlashCommand("механики", "Информация о доступных механиках боя")]
        public async Task Mech()
        {
            await OWMech();
        }

        private async Task OWInfo()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Как использовать бота")
                .AddField("Стрельба", "`-стрелять {меткость} {режим} {оружие} {слап цели}` - стандартная стрельба\n`-стрелять {меткость} {оружие} {желаемое место попадания} {слап цели}` - прицельная стрельба\n`-стрелять {оружие}` - получить информацию об оружии\n\nДля увеличения меткости в конце команд для стрельбы можно выставить \"0\"\n")
                .AddField("Гранаты", "`-бросать {меткость} {граната}` - бросок гранаты\n`-бросать {граната}` - получить информацию о гранате\n")
                .AddField("Ближний бой", "`-резать {меткость} {оружие} {слап цели}` - атака оружием ближнего боя\n`-резать {оружие}` - получить информацию об оружии\n")
                .AddField("Взаимодействия с хит поинтами", "`-хп` - показывает ваши текущие хп\n`-установить {номер части тела} {значение}` - прибавляет к указанной части тела то количество хп, которое указано в значении\n")
                .AddField("Дополнительные команды", "`-инфо` - вызывает эту таблицу\n`-механ` - показывает дополнительные механики боя\n\n")
                .AddField("Стандартное оружие", "Пистолет\nАвтомат\nДробовик\nПулемёт\nВинтовка\nОгнемёт", true)
                .AddField("Гранаты", "Осколочная\nЗажигательная\nСветошумовая\nГазовая\nТактическая\nДымовая", true)
                .Build();

            await RespondAsync(embed: embed);
        }

        private async Task OWMech()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Дополнительные механики боя")
                .AddField("Огонь на подавление", "Персонаж должен использовать дистанционное оружие, способное вести огонь короткими или длинными очередями.Заявив огонь на " + "подавление, персонаж выбирает область, которую он будет подавлять огнѐм. Затем персонаж производит выстрел выбранной очередью." + "Дополнительное попадание в таком случае высчитывается за каждые 2 ступени успеха, а не за 1. Цели, попавшие под обстрел и не " + "прошедшие тест, получают -20 к меткости", true)
                .AddField("Осторожная атака", "Персонаж действует осторожно и продуманно, в любой миг готовясь уйти в оборону. В этом ходу он получает штраф -10 на тесты Ближнего или Дальнего боя, однако противники в следующем ходу так же будут получать -10 к своим тестам направленным против персонажа.", true)
                .Build();

            await RespondAsync(embed: embed);
        }

        private static string FrenchTime(string msg)
        {
            string[] str = msg.Split(':');
            int h = Convert.ToInt32(str[0]);
            int m = Convert.ToInt32(str[1]);
            const double coef = 1.157407407407407;
            h *= 3600;
            m *= 60;
            double frenchSec = (h + m) * coef;
            double frenchMin = Math.Round(frenchSec) / 100;
            double frenchHour = Math.Floor(frenchMin / 100);
            frenchMin = Math.Floor(frenchMin % 100);
            return $"По французской десятичной системе это будет {Convert.ToInt32(frenchHour)}:{Convert.ToInt32(frenchMin)}";
        }

        private async void Shooting(string msg, int len)
        {
            if (len > 50)
            {
                await RespondAsync("Слишком много, пожалей человека");
                return;
            }

            string[] slap = msg.Split();
            string result = string.Empty;
            for (int i = 0; i < slap.Length; i++)
            {
                if (slap[i] == "<@968224677803745290>" | slap[i] == "<@402837633220214784>")
                {
                    await RespondAsync("Пососеш, ок?");
                    return;
                }

                result += slap[i] + " ";
            }

            for (int i = 0, k = 0; i < len; i++, k++)
            {
                await ReplyAsync($"Пиу {result}");
                if (k == 6)
                {
                    await ReplyAsync("Перезарядка");
                    Thread.Sleep(8000);
                    k = 0;
                }

                // Thread.Sleep(1000);
            }

            return;
        }
    }
}

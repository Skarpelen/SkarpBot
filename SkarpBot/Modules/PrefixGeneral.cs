namespace SkarpBot.Modules
{
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using Discord;
    using Discord.Commands;
    using SkarpBot.Data;
    using SkarpBot.OnlyWar;

    /// <summary>
    /// Все доступные команды для бота.
    /// </summary>
    public class PrefixGeneral : SkarpBotModuleBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefixGeneral"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> to be used.</param>
        /// <param name="dataAccessLayer">The <see cref="DataAccessLayer"/> to be used.</param>
        public PrefixGeneral(IHttpClientFactory httpClientFactory, DataAccessLayer dataAccessLayer)
            : base(dataAccessLayer)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Команда пишущая Pong!.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("ping")]
        public async Task PingAsync(IUser user)
        {
            await ReplyAsync("Pong! " + user.Username + " " + user.Id);
        }

        [Command("регистрация")]
        public async Task Register(string aType)
        {
            Armour armour = new Armour(aType);
            if (armour.error)
            {
                await ReplyAsync("Некорректное название брони");
                return;
            }

            await DataAccessLayer.Nuller(Context.User.Id, aType);
            await ReplyAsync("Что то случилось");
        }

        [Command("префикс")]
        [RequireOwner]
        public async Task PrefixAsync(string prefix = null)
        {
            if (prefix == null)
            {
                await ReplyAsync($"Префикс данного сервера — \"{Prefix}\"");
                return;
            }

            await DataAccessLayer.SetPrefix(Context.Guild.Id, prefix);
            await ReplyAsync($"Префикс \"{prefix} успешно установлен\"");
        }

        /// <summary>
        /// Команда, преобразующая стандартное время в десятичную систему времен французской революции.
        /// </summary>
        /// <param name="str">Отправляемое на обработку время.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("время")]
        public async Task TimeAsync(string str)
        {
            Regex rgx = new (@"[0-23]:[0-59]");
            if (rgx.IsMatch(str))
            {
                await ReplyAsync(FrenchTime(str));
            }
            else
            {
                await ReplyAsync("Неправильный формат времени (00:00)");
            }
        }

        /// <summary>
        /// Команда для расстрела слапами.
        /// </summary>
        /// <param name="str">Жертва.</param>
        /// <param name="len">Количество слапов.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("расстрелять")]
        public Task SlapKill(string str, int len)
        {
            Shooting(str, len);
            return Task.CompletedTask;
        }

        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        /// 

        [Command("хп")]
        public async Task HpGet()
        {
            await ReplyAsync(DataAccessLayer.GetHp(Context.User.Id));
        }

        [Command("стрелять")]
        [Alias("стрельба", "с")]
        public async Task FireWeapon(int accuracy, int mode, string wType, IUser user)
        {
            if (mode > 2 || mode < 0 || accuracy < 0)
            {
                await ReplyAsync("Неправильные значения режима стрельбы/меткости");
                return;
            }

            var gunFire = new FireWeapon(accuracy, mode, wType, DataAccessLayer.GetArmour(user.Id), user.Id);
            var shoot = await gunFire.Shoot(DataAccessLayer);
            await ReplyAsync(shoot);

            /*gunFire.Shoot(out point, out dmg);
            await DataAccessLayer.ChangeHp(Context.User.Id, point, dmg);
            await ReplyAsync(DataAccessLayer.GetHp(Context.User.Id));*/
        }

        /// <summary>
        /// Стрельба в обычном режиме.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="mode">Режим стрельбы.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="aType">Броня цели.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("стрелять")]
        [Alias("стрельба", "с")]
        public async Task FireWeapon(int accuracy, int mode, string wType, string aType)
        {
            if (mode > 2 || mode < 0 || accuracy < 0)
            {
                await ReplyAsync("Неправильные значения режима стрельбы/меткости");
                return;
            }

            var gunFire = new FireWeapon(accuracy, mode, wType, aType);
            int point, dmg;
            // await ReplyAsync(gunFire.Shoot());

            /*gunFire.Shoot(out point, out dmg);
            await DataAccessLayer.ChangeHp(Context.User.Id, point, dmg);
            await ReplyAsync(DataAccessLayer.GetHp(Context.User.Id));*/
        }

        /// <summary>
        /// Прицельный выстрел.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="aType">Броня цели.</param>
        /// <param name="aimPoint">Куда надо попасть.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("стрелять")]
        [Alias("стрельба", "с")]
        public async Task FireWeapon(int accuracy, string wType, string aType, string aimPoint)
        {
            if (accuracy < 0)
            {
                await ReplyAsync("Неправильное значение меткости");
                return;
            }

            var gunFire = new FireWeapon(accuracy, wType, aType, aimPoint);
            await ReplyAsync(gunFire.CalledShot());
        }

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Оружие для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("стрелять")]
        [Alias("стрельба", "с")]
        public async Task FireWeapon(string wType)
        {
            var gunFire = new FireWeapon(wType);
            await ReplyAsync(gunFire.WriteQualitiesFire());
        }

        /// <summary>
        /// Бросок гранаты в обычном режиме.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="wType">Гранта стреляющего.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("бросить")]
        [Alias("бросать", "б")]
        public async Task GrenadeWeapon(int accuracy, string wType)
        {
            if (accuracy < 0)
            {
                await ReplyAsync("Неправильное значение меткости");
                return;
            }

            var grenadeThrow = new Grenade(accuracy, wType);
            await ReplyAsync(grenadeThrow.GrenadeThrow());
        }

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Граната для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("бросить")]
        [Alias("бросать", "б")]
        public async Task GrenadeWeapon(string wType)
        {
            var grenadeThrow = new Grenade(wType);
            await ReplyAsync(grenadeThrow.WriteQualitiesGrenade());
        }

        /// <summary>
        /// Информация об имеющихся вариантах оружия.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("инфо")]
        public async Task Info()
        {
            await OWInfo();
        }

        /// <summary>
        /// Какает.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("какаха")]
        public async Task Poop(int len)
        {
            for (int i = 0; i < len; i++)
            {
                await ReplyAsync(Congrats());
                Thread.Sleep(1000);
            }
        }

        [Command("клоуны")]
        public async Task Clowns()
        {
            await ReplyAsync("🤡🤡🤡🤡🤡🤡Гифко-клоуны в чате 🤡🤡🤡🤡🤡🤡🤡");
        }

        [Command("эмбед")]
        public async Task Embed(string title, string desc)
        {
            await SendEmbedAsync(title, desc);
        }

        private async Task OWInfo()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Как использовать бота")
                .AddField("Стандартная стрельба", "?стрелять {меткость} {режим} {оружие} {броня противника}")
                .AddField("Прицельная стрельба", "?стрелять {меткость} {оружие} {броня противника} {желаемое место попадания}")
                .AddField("Получить информацию об **огнестрельном** оружии", "?стрелять {оружие}")
                .AddField("Стандартный бросок метательного вооружения", "?стрелять {меткость} {граната}")
                .AddField("Получить информацию о метательном вооружении", "?бросать {граната}\n")
                .AddField("Оружие", "Пистолет\nАвтомат\nДробовик\nПулемёт\nВинтовка\nОгнемёт", true)
                .AddField("Гранаты", "Осколочная\nЗажигательная\nСветошумовая\nГазовая\nТактическая\nДымовая", true)
                .Build();

            await ReplyAsync(embed: embed);

            //return "```diff\nПеречень всех команд для стрельбы:\n+?стрелять {меткость} {режим} {оружие} {броня противника}" +
            //    "\n+?стрелять {меткость} {оружие} {броня противника} {желаемое место попадания}\n+?стрелять {оружие}\n\n" +
            //    "+?бросить {меткость} {граната}\n+?бросить {граната}\n\nИмеющиеся наборы оружия и гранат на данный момент:\n" +
            //    "-Оружие                    Гранаты\r-Пистолет                  Осколочная\r-Автомат                   Зажигательная\r-Дробовик                  Светошумовая\r-Пулемет                   Газовая\r-Винтовка                  Тактическая\r-Огнемет                   Дымовая" +
            //    "```";
        }

        private string Congrats()
        {
            Random rnd = new Random();
            string[] congrats = { "https://media.discordapp.net/attachments/402838262227664901/983134004263931934/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134004561739817/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134004792418375/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134005060841563/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134005362827274/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134005606109264/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983134721259237467/unknown.png",
                                  "ты кто",
                                  "приветик, сдр тебя",
                                  "воняеш",
                                  "https://cdn.discordapp.com/attachments/886737795597623327/983133488838479904/unknown.png",
                                  "https://media.discordapp.net/attachments/402838262227664901/983140044028149830/unknown.png"
            };

            return congrats[rnd.Next(12)];
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
                await ReplyAsync("Слишком много, пожалей человека");
                return;
            }

            string[] slap = msg.Split();
            string result = string.Empty;
            for (int i = 0; i < slap.Length; i++)
            {
                if (slap[i] == "<@968224677803745290>" | slap[i] == "<@402837633220214784>")
                {
                    await ReplyAsync("Пососеш, ок?");
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

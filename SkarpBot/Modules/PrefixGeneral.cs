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
        public async Task PingAsync()
        {
            await ReplyAsync("Pong! ");
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

            //await DataAccessLayer.Nuller(Context.User.Id, aType);
            await ReplyAsync("Что то случилось");
        }

        [Command("установить")]
        public async Task SetHp(int point, int val)
        {
            val *= -1;
            //await DataAccessLayer.ChangeHp(Context.User.Id, point, val);
            await ReplyAsync("Значения применены");
        }

        [Command("установить")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetHp(IUser user, int point, int val)
        {
            val *= -1;
            //await DataAccessLayer.ChangeHp(user.Id, point, val);
            await ReplyAsync("Значения применены");
        }

        [Command("префикс")]
        [RequireOwner]
        public async Task PrefixAsync(string? prefix = null)
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
            await ReplyAsync(DataAccessLayer.GetHp(Context.User.Id, Context.Guild.Id));
        }

        /// <summary>
        /// Стрельба в обычном режиме.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="mode">Режим стрельбы.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="user">Цель.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("стрелять")]
        [Alias("стрельба", "с", "c")]
        public async Task FireWeapon(int accuracy, int mode, string wType, IUser user, int aim = 1)
        {
            if (mode > 2 || mode < 0 || accuracy < 0)
            {
                await ReplyAsync("Неправильные значения режима стрельбы/меткости");
                return;
            }

            //var gunFire = new FireWeapon(accuracy, mode, wType, DataAccessLayer.GetArmour(user.Id), user.Id, aim);
            //var shoot = await gunFire.Shoot(DataAccessLayer);
            //await ReplyAsync(shoot);

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
        [Command("стрелять")]
        [Alias("стрельба", "с", "c", "прицел")]
        public async Task FireWeapon(int accuracy, string wType, IUser user, string aimpoint, int aim = 1)
        {
            if (accuracy < 0)
            {
                await ReplyAsync("Неправильное значение меткости");
                return;
            }

            //var gunFire = new FireWeapon(accuracy, wType, DataAccessLayer.GetArmour(user.Id), user.Id, aimpoint, aim);
            //var shoot = await gunFire.CalledShot(DataAccessLayer);
            //await ReplyAsync(shoot);
        }

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Оружие для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Command("стрелять")]
        [Alias("стрельба", "с", "c")]
        public async Task FireWeapon(string wType)
        {
            //var gunFire = new FireWeapon(wType);
            //await ReplyAsync(gunFire.WriteQualitiesFire());
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

        [Command("резать")]
        [Alias("р")]
        public async Task MeleeWeapon(int accuracy, string wType, IUser user)
        {
            //var knifePower = new Melee(accuracy, wType, DataAccessLayer.GetArmour(user.Id), user.Id);
            //var swing = await knifePower.Swing(DataAccessLayer);
            //await ReplyAsync(swing);
        }

        [Command("резать")]
        [Alias("р")]
        public async Task MeleeWeapon(string wType)
        {
            var knifePower = new Melee(wType);
            await ReplyAsync(knifePower.WriteQualitiesMelee());
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

        [Command("механ")]
        public async Task Mech()
        {
            await OWMech();
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
                .AddField("Стрельба", "`-стрелять {меткость} {режим} {оружие} {слап цели}` - стандартная стрельба\n`-стрелять {меткость} {оружие} {желаемое место попадания} {слап цели}` - прицельная стрельба\n`-стрелять {оружие}` - получить информацию об оружии\n\nДля увеличения меткости в конце команд для стрельбы можно выставить \"0\"\n")
                .AddField("Гранаты", "`-бросать {меткость} {граната}` - бросок гранаты\n`-бросать {граната}` - получить информацию о гранате\n")
                .AddField("Ближний бой", "`-резать {меткость} {оружие} {слап цели}` - атака оружием ближнего боя\n`-резать {оружие}` - получить информацию об оружии\n")
                .AddField("Взаимодействия с хит поинтами", "`-хп` - показывает ваши текущие хп\n`-установить {номер части тела} {значение}` - прибавляет к указанной части тела то количество хп, которое указано в значении\n")
                .AddField("Дополнительные команды", "`-инфо` - вызывает эту таблицу\n`-механ` - показывает дополнительные механики боя\n\n")
                .AddField("Стандартное оружие", "Пистолет\nАвтомат\nДробовик\nПулемёт\nВинтовка\nОгнемёт", true)
                .AddField("Гранаты", "Осколочная\nЗажигательная\nСветошумовая\nГазовая\nТактическая\nДымовая", true)
                .Build();

            await ReplyAsync(embed: embed);
        }

        private async Task OWMech()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Дополнительные механики боя")
                .AddField("Огонь на подавление", "Персонаж должен использовать дистанционное оружие, способное вести огонь короткими или длинными очередями.Заявив огонь на " + "подавление, персонаж выбирает область, которую он будет подавлять огнѐм. Затем персонаж производит выстрел выбранной очередью." + "Дополнительное попадание в таком случае высчитывается за каждые 2 ступени успеха, а не за 1. Цели, попавшие под обстрел и не " + "прошедшие тест, получают -20 к меткости", true)
                .AddField("Осторожная атака", "Персонаж действует осторожно и продуманно, в любой миг готовясь уйти в оборону. В этом ходу он получает штраф -10 на тесты Ближнего или Дальнего боя, однако противники в следующем ходу так же будут получать -10 к своим тестам направленным против персонажа.", true)
                .Build();

            await ReplyAsync(embed: embed);
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

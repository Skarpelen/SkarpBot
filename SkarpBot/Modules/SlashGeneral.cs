namespace SkarpBot.Modules
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Interactions;
    using SkarpBot.Data;
    using SkarpBot.OnlyWar;

    /// <summary>
    /// Все доступные команды для бота через /.
    /// </summary>
    public class SlashGeneral : SkarpBotModuleBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SlashGeneral(IHttpClientFactory httpClientFactory, DataAccessLayer dataAccessLayer)
            : base(dataAccessLayer)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Команда пишущая Pong!.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("ping", "Пингует")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
        }

        /// <summary>
        /// Команда, преобразующая стандартное время в десятичную систему времен французской революции.
        /// </summary>
        /// <param name="str">Отправляемое на обработку время.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("время", "Узнай сколько сейчас времени во Франции")]
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
        /// <param name="aType">Броня цели.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("стрелять", "Стреляет из оружия")]
        public async Task FireWeapon(int accuracy, int mode, string wType, string aType)
        {
            if (mode > 2 || mode < 0 || accuracy < 0)
            {
                await ReplyAsync("Неправильные значения режима стрельбы/меткости");
                return;
            }

            ////////////// на переработке
            //var gunFire = new FireWeapon(accuracy, mode, wType, aType);
            //await ReplyAsync(gunFire.Shoot());
        }

        /// <summary>
        /// Прицельный выстрел.
        /// </summary>
        /// <param name="accuracy">Коэфициент меткости.</param>
        /// <param name="wType">Оружие стреляющего.</param>
        /// <param name="aType">Броня цели.</param>
        /// <param name="aimPoint">Куда надо попасть.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("стрелять", "Прицельный выстрел")]
        public async Task FireWeapon(int accuracy, string wType, string aimPoint, IUser user)
        {
            if (accuracy < 0)
            {
                await ReplyAsync("Неправильное значение меткости");
                return;
            }

            /*var gunFire = new FireWeapon(accuracy, wType, aimPoint, DataAccessLayer.GetArmour(user.Id), user.Id);
            var shoot = await gunFire.CalledShot(DataAccessLayer);
            await ReplyAsync(shoot);*/
        }

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Оружие для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("стрелять", "Особенности оружия")]
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
        [SlashCommand("бросить", "Бросает гранату")]
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
        [SlashCommand("бросить", "Особенности гранаты")]
        public async Task GrenadeWeapon(string wType)
        {
            var grenadeThrow = new Grenade(wType);
            await ReplyAsync(grenadeThrow.WriteQualitiesGrenade());
        }

        /// <summary>
        /// Информация об имеющихся вариантах оружия.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("инфо", "Информация о доступном вооружении")]
        public async Task Info()
        {
            await ReplyAsync(OWInfo());
        }

        private static string OWInfo()
        {
            return "```diff\nПеречень всех команд для стрельбы:\n+?стрелять {меткость} {режим} {оружие} {броня противника}" +
                "\n+?стрелять {меткость} {оружие} {броня противника} {желаемое место попадания}\n+?стрелять {оружие}\n\n" +
                "+?бросить {меткость} {граната}\n+?бросить {граната}\n\nИмеющиеся наборы оружия и гранат на данный момент:\n" +
                "-Оружие                    Гранаты\r-Пистолет                  Осколочная\r-Автомат                   Зажигательная\r-Дробовик                  Светошумовая\r-Пулемет                   Газовая\r-Винтовка                  Тактическая\r-Огнемет                   Дымовая" +
                "```";
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

                Thread.Sleep(1000);
            }

            return;
        }
    }
}

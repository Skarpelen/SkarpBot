namespace SkarpBot.Modules
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Interactions;
    using InteractionFramework;
    using SkarpBot.Data;

    // Interation modules must be public and inherit from an IInterationModuleBase
    public partial class SlashGeneral : SkarpBotInteractionsModuleBase
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

        /// <summary>
        /// Команда, преобразующая стандартное время в десятичную систему времен французской революции.
        /// </summary>
        /// <param name="str">Отправляемое на обработку время.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("время", "Ну как там во Франции")]
        public async Task TimeAsync(string str)
        {
            Regex rgx = new (@"[0-23]:[0-59]");
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
        public Task SlapKill(IUser str, int len)
        {
            Shooting(str.Id, len);
            return Task.CompletedTask;
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

        private async void Shooting(ulong msg, int len)
        {
            if (len > 50)
            {
                await RespondAsync("Слишком много, пожалей человека");
                return;
            }

            if (msg != 968224677803745290 & msg != 402837633220214784)
            {
                for (int i = 0, k = 0; i < len; i++, k++)
                {
                    await ReplyAsync($"Пиу <@{msg}>");
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

            await RespondAsync("Пососеш, ок?");
            return;
        }
    }
}

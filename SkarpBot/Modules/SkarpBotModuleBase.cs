namespace SkarpBot.Modules
{
    using Discord;
    using Discord.Commands;
    using Discord.Rest;
    using SkarpBot.Data;

    /// <summary>
    /// Кастомная имплементация <see cref="ModuleBase{T}"/> для SkarpBot.
    /// </summary>
    public abstract class SkarpBotModuleBase : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// The <see cref="DataAccessLayer"/> of SkarpBot.
        /// </summary>
        public readonly DataAccessLayer DataAccessLayer;

        public string Prefix
        {
            get
            {
                if (string.IsNullOrEmpty(_prefix))
                {
                    _prefix = DataAccessLayer.GetPrefix(Context.Guild.Id);
                }

                return _prefix;
            }
        }

        private string _prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkarpBotModuleBase"/> class.
        /// </summary>
        /// <param name="dataAccessLayer">The <see cref="DataAccessLayer"/> to inject.</param>
        public SkarpBotModuleBase(DataAccessLayer dataAccessLayer)
        {
            DataAccessLayer = dataAccessLayer;
        }

        /// <summary>
        /// Отправляет эмбед с титульником и описанием в канал.
        /// </summary>
        /// <param name="title">Титульник.</param>
        /// <param name="description">Описание.</param>
        /// <returns>A <see cref="RestUserMessage"/> с эмбедом.</returns>
        public async Task<RestUserMessage> SendEmbedAsync(string title, string description)
        {
            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description);

            return await Context.Channel.SendMessageAsync(embed: builder.Build());
        }
    }
}

namespace SkarpBot.Modules
{
    using System.Threading.Tasks;
    using Discord;
    using Discord.Interactions;
    using Discord.Rest;
    using SkarpBot.Data;

    public abstract class SkarpBotInteractionsModuleBase : InteractionModuleBase<SocketInteractionContext>
    {
        /// <summary>
        /// The <see cref="DataAccessLayer"/> of SkarpBot.
        /// </summary>
        public readonly DataAccessLayer DataAccessLayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkarpBotModuleBase"/> class.
        /// </summary>
        /// <param name="dataAccessLayer">The <see cref="DataAccessLayer"/> to inject.</param>
        public SkarpBotInteractionsModuleBase(DataAccessLayer dataAccessLayer)
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

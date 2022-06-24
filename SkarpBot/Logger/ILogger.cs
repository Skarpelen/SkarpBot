namespace SkarpBot.Logger
{
    using Discord;

    public interface ILogger
    {
        // Establish required method for all Loggers to implement
        public Task Log(LogMessage message);
    }
}

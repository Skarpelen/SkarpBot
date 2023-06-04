using Newtonsoft.Json;

namespace SkarpBot.Data.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_discord_id")]
        public ulong UserDiscordId { get; set; }

        [JsonProperty("guild")]
        public int GuildId { get; set; }
    }
}

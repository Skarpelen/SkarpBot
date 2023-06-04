using Newtonsoft.Json;

namespace SkarpBot.Data.Models
{
    public class Guild
    {
        [JsonProperty("guild_id")]
        public int Id { get; set; }

        [JsonProperty("server_id")]
        public ulong ServerId { get; set; }

        [JsonProperty("server_name")]
        public string ServerName { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; } = "!";
    }
}

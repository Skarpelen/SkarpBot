using Newtonsoft.Json;

namespace SkarpBot.Data.Models
{
    public class Grenade
    {
        [JsonProperty("grenade_id")]
        public int GrenadeId { get; set; }

        [JsonProperty("grenade_name")]
        public string GrenadeName { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("user")]
        public int UserId { get; set; }
    }
}

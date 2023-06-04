using Newtonsoft.Json;

namespace SkarpBot.Data.Models
{
    public class Status
    {
        [JsonProperty("status_id")]
        public int Id { get; set; }

        [JsonProperty("armour")]
        public string Armour { get; set; }

        [JsonProperty("head")]
        public int Head { get; set; }

        [JsonProperty("body")]
        public int Body { get; set; }

        [JsonProperty("l_hand")]
        public int LHand { get; set; }

        [JsonProperty("r_hand")]
        public int RHand { get; set; }

        [JsonProperty("l_foot")]
        public int LFoot { get; set; }

        [JsonProperty("r_foot")]
        public int RFoot { get; set; }

        [JsonProperty("user")]
        public int UserId { get; set; }
    }
}

using Newtonsoft.Json;

namespace SkarpBot.Data.Models
{
    public class Weapon
    {
        [JsonProperty("weapon_id")]
        public int WeaponId { get; set; }

        [JsonProperty("weapon_name")]
        public string WeaponName { get; set; }

        [JsonProperty("current_ammo")]
        public int CurrentAmmo { get; set; }

        [JsonProperty("inventory_name")]
        public string InventoryName { get; set; }

        [JsonProperty("user")]
        public int UserId { get; set; }
    }
}

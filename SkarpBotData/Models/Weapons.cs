using System.ComponentModel.DataAnnotations;

namespace SkarpBot.Data.Models
{
    public class Weapons
    {
        [Key]
        public int WeaponId { get; set; }
        public int UserId { get; set; }
        public string WeaponName { get; set; }
        public int CurrentAmmo { get; set; } = -1;
        public string InventoryName { get; set; }
    }
}

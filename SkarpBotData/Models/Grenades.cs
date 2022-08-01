using System.ComponentModel.DataAnnotations;

namespace SkarpBot.Data.Models
{
    public class Grenades
    {
        [Key]
        public int GrenadeId { get; set; }
        public int UserId { get; set; }
        public string GrenadeName { get; set; }
        public int Amount { get; set; }
    }
}

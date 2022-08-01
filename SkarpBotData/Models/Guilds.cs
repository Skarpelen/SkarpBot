using System.ComponentModel.DataAnnotations;

namespace SkarpBot.Data.Models
{
    public class Guilds
    {
        [Key]
        public int Id { get; set; }
        public ulong ServerId { get; set; }
        public string ServerName { get; set; }
        public string Prefix { get; set; } = "!";
    }
}

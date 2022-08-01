namespace SkarpBot.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ulong UserDiscordID { get; set; }
        public int GuildsId { get; set; }
    }
}

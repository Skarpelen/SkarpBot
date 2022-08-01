namespace SkarpBot.Data.Models
{
    public class MenuHandler
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GuildId { get; set; }
        public string PickedValue { get; set; }
    }
}

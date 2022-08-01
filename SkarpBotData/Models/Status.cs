using System.ComponentModel.DataAnnotations.Schema;

namespace SkarpBot.Data.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public string Armour { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int LHand { get; set; }
        public int RHand { get; set; }
        public int LFoot { get; set; }
        public int RFoot { get; set; }
    }
}

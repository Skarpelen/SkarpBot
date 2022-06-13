using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkarpBot.Data.Models
{
    public class PersonHP
    {
        public ulong Id { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int LHand { get; set; }
        public int RHand { get; set; }
        public int LFoot { get; set; }
        public int RFoot { get; set; }
    }
}

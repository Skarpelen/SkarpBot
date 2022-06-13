using Microsoft.EntityFrameworkCore;
using SkarpBot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkarpBot.Data.Context
{
    public class SkarpBotDbContext : DbContext
    {
        public SkarpBotDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Guild> Guilds { get; set; }
        public DbSet<PersonHP> Persons { get; set; }
        public DbSet<PersonArmour> Armour { get; set; }
    }
}

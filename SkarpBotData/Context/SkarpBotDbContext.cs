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

        public DbSet<Guilds> Guilds { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Weapons> Weapons { get; set; }
        public DbSet<Grenades> Grenades { get; set; }
        public DbSet<MenuHandler> MenuHandler { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkarpBot.Data.Models;

namespace SkarpBot.Data.Context
{
    public class SkarpBotDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SkarpBotDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Guild> Guilds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Grenade> Grenades { get; set; }
        public DbSet<MenuHandler> MenuHandler { get; set; }
    }
}

using SkarpBot.Data.Models;

namespace SkarpBot.Data
{
    partial class DataAccessLayer
    {
        private int[] maxHp = new int[] { 12, 28, 17, 20 };

        public string GetUsers(ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .ToList();

            if (guild.Count() == 0)
            {
                return "Этого сервера нет в базе данных, зарегистрируйте хотя бы одного пользователя чтобы сервер появился в хранилище";
            }

            var users = context.Users
                .Where(u => u.GuildsId == guild.First().Id)
                .ToList();

            if (users.Count() == 0)
            {
                return "На сервере нет зарегистрированных пользователей";
            }

            string result = String.Format("{0, -25} {1, -15} {2, -10} {3, -10}\n", "Ник", "Оружие", "Патроны", "Здоровье");
            Weapons currentWeapon;
            foreach(Users user in users)
            {
                currentWeapon = GetEquippedWeapon(user.Id);
                result += String.Format("{0, -25} {1, -15} {2, -10} {3, -10}\n",
                    user.Name,
                    currentWeapon != null ? currentWeapon.WeaponName : "",
                    currentWeapon != null ? currentWeapon.CurrentAmmo : "",
                    GetAveraveHpPercentage(user.Id) + "%");
            }

            return "```cpp\n" + result + "```";
        }

        private double GetAveraveHpPercentage(int userid)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = context.Status
                .Where(s => s.UserId == userid)
                .First();

            double sum = status.Head / (double)maxHp[0] * 100.0
                       + status.Body / (double)maxHp[1] * 100.0
                       + status.LHand / (double)maxHp[2] * 100.0
                       + status.RHand / (double)maxHp[2] * 100.0
                       + status.LFoot / (double)maxHp[3] * 100.0
                       + status.RFoot / (double)maxHp[3] * 100.0;

            return Math.Round(sum / 6);
        }

        public async Task RegisterGuild(ulong id, string name)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(new Guilds { Prefix = "!", ServerId = id, ServerName = name });

            await context.SaveChangesAsync();
        }

        public async Task RegisterUser(string name, ulong id, int serverid)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(new Users { Name = name, UserDiscordID = id, GuildsId = serverid });

            await context.SaveChangesAsync();
        }

        public async Task RegisterStatus(int id, string armour)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(new Status
            {
                Armour = armour,
                UserId = id,
                Head = maxHp[0],
                Body = maxHp[1],
                LHand = maxHp[2],
                RHand = maxHp[2],
                LFoot = maxHp[3],
                RFoot = maxHp[3],
            });

            await context.SaveChangesAsync();
        }

        public async Task UserStart(ulong guildId, string guildName, ulong userId, string userName, string armourName)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildId)
                .ToList();

            if (guild.Count() == 0)
            {
                await RegisterGuild(guildId, guildName);
            }

            var user = context.Users
                .Where(u => u.UserDiscordID == userId & u.GuildsId == guild.First().Id)
                .ToList();

            if (user.Count() == 0)
            {
                await RegisterUser(userName, userId, guild.First().Id);
            }

            var status = context.Status
                .Where(s => s.UserId == user.First().Id)
                .ToList();

            if (status.Count() == 0)
            {
                await RegisterStatus(user.First().Id, armourName);
            }
            else
            {
                status.First().Armour = armourName;
                status.First().Head = maxHp[0];
                status.First().Body = maxHp[1];
                status.First().LHand = maxHp[2];
                status.First().RHand = maxHp[2];
                status.First().LFoot = maxHp[3];
                status.First().RFoot = maxHp[3];
            }

            await context.SaveChangesAsync();
        }

        public async Task MenuPicker(ulong userid, ulong guildid, string pickedvalue)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid & u.GuildsId == guild.Id)
                .First();

            var result = context.MenuHandler.SingleOrDefault(mh => mh.UserId == user.Id & mh.GuildId == guild.Id);

            if (result != null)
            {
                result.PickedValue = pickedvalue;
                await context.SaveChangesAsync();
                return;
            }

            context.Add(new MenuHandler
            {
                UserId = user.Id,
                GuildId = guild.Id,
                PickedValue = pickedvalue,
            });

            await context.SaveChangesAsync();
        }
    }
}

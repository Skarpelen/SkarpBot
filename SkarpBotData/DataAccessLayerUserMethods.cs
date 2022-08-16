using SkarpBot.Data.Models;

namespace SkarpBot.Data
{//добавь транзакции
    public partial class DataAccessLayer
    {
        public async Task<string> EquipWeapon(ulong userid, ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();

            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid && u.GuildsId == guild.Id)
                .First();

            var menu = context.MenuHandler
                .Where(m => m.UserId == user.Id && m.GuildId == guild.Id)
                .First();

            string[] pickedvalue = menu.PickedValue.Split('.');

            if (pickedvalue[1] != "W")
            {
                return "Ты долбаеб";
            }

            var weapon = context.Weapons
                .Where(w => w.WeaponId == Convert.ToInt32(pickedvalue[0]))
                .First();

            if (weapon.InventoryName == "equipped")
            {
                weapon.InventoryName = "unequipped";
                await context.SaveChangesAsync();
                return $"Успешно сослано в инвентарь следующее снаряжение: {weapon.WeaponName}";
            }
            else
            {
                var equippedWeapon = context.Weapons
                    .Where(w => w.InventoryName == "equipped" & w.UserId == user.Id)
                    .ToList();

                if (equippedWeapon.Count() > 0)
                {
                    equippedWeapon.First().InventoryName = "unequipped";
                }

                weapon.InventoryName = "equipped";
                await context.SaveChangesAsync();
                return $"Успешно экипировано следующее снаряжение: {weapon.WeaponName}\n" +
                    (equippedWeapon.Count() > 0 ? $"Успешно сослано в инвентарь следующее снаряжение: {equippedWeapon.First().WeaponName}" : "");
            }
        }

        public async Task<string> ReloadWeapon(int weaponid, int maxAmmo)
        {
            using var context = _contextFactory.CreateDbContext();
            var weapon = context.Weapons
                .Where(w => w.WeaponId == weaponid)
                .First();

            weapon.CurrentAmmo = maxAmmo;

            await context.SaveChangesAsync();
            return $"Перезарядка `{weapon.WeaponName}` произведена успешно";
        }

        public Weapons GetEquippedWeapon(ulong userid, ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();

            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid && u.GuildsId == guild.Id)
                .First();

            var weapon = context.Weapons
                .Where(w => w.UserId == user.Id && w.InventoryName == "equipped")
                .First();

            return weapon;
        }

        public Weapons GetEquippedWeapon(int userid)
        {
            using var context = _contextFactory.CreateDbContext();

            var user = context.Users
                .Where(u => u.Id == userid)
                .First();

            var weapon = context.Weapons
                .Where(w => w.UserId == user.Id && w.InventoryName == "equipped")
                .ToList();

            if (weapon.Count() != 0)
            {
                return weapon.First();
            }

            return null;
        }

        public Status GetArmour(ulong userid, ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();

            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid && u.GuildsId == guild.Id)
                .First();

            var status = context.Status
                .Where(s => s.UserId == user.Id)
                .First();

            return status;
        }

        public async Task<string> DestroyInventory(ulong userid, ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();

            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid && u.GuildsId == guild.Id)
                .First();

            var menu = context.MenuHandler
                .Where(m => m.UserId == user.Id && m.GuildId == guild.Id)
                .First();

            string[] pickedvalue = menu.PickedValue.Split('.');

            if (pickedvalue[1] == "W")
            {
                var weapons = context.Weapons
                    .Where(w => w.WeaponId == Convert.ToInt32(pickedvalue[0]))
                    .First();

                context.Weapons.Remove(weapons);
                await context.SaveChangesAsync();
                return "Снаряжение было удалено";
            }

            if (pickedvalue[1] == "G")
            {
                var grenades = context.Grenades
                    .Where(w => w.GrenadeId == Convert.ToInt32(pickedvalue[0]))
                    .First();

                if (grenades.Amount == 1)
                {
                    context.Grenades.Remove(grenades);
                    await context.SaveChangesAsync();
                    return "Снаряжение было удалено";
                }

                grenades.Amount -= 1;
                await context.SaveChangesAsync();
                return $"Из инвентаря была удалена 1 граната типа `{grenades.GrenadeName}`. В инвентаре осталось {grenades.Amount}";
            }

            return "???";
        }

        public async Task DestroyGrenade(int grenadeid)
        {
            using var context = _contextFactory.CreateDbContext();
            var grenades = context.Grenades
                .Where(g => g.GrenadeId == grenadeid)
                .First();

            if (grenades.Amount == 1)
            {
                context.Grenades.Remove(grenades);
                await context.SaveChangesAsync();
                return;
            }

            grenades.Amount -= 1;
            await context.SaveChangesAsync();
            return;
        }

        public async Task AddWeapon(ulong userId, ulong guildId, string weaponName, int maxAmmo)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildId)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userId && u.GuildsId == guild.Id)
                .First();

            context.Add(new Weapons { WeaponName = weaponName, CurrentAmmo = maxAmmo, UserId = user.Id, InventoryName = "unequipped" });

            await context.SaveChangesAsync();
        }

        public async Task AddGrenades(ulong userId, ulong guildId, string grenadeName, int amount)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildId)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userId && u.GuildsId == guild.Id)
                .First();

            context.Add(new Grenades { GrenadeName = grenadeName, Amount = amount, UserId = user.Id });

            await context.SaveChangesAsync();
        }

        public List<Weapons> GetInventoryWeapons(ulong userId, ulong guildId)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildId)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userId & u.GuildsId == guild.Id)
                .First();

            var weapons = context.Weapons
                .Where(w => w.UserId == user.Id)
                .ToList();

            return weapons;
        }

        public List<Grenades> GetInventoryGrenades(ulong userId, ulong guildId)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildId)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userId & u.GuildsId == guild.Id)
                .First();

            var grenades = context.Grenades
                .Where(w => w.UserId == user.Id)
                .ToList();

            return grenades;
        }

        public async Task ChangeAmmo(int weaponid, int coeff)
        {
            using var context = _contextFactory.CreateDbContext();
            var weapon = context.Weapons
                .Where(w => w.WeaponId == weaponid)
                .First();

            weapon.CurrentAmmo += coeff;
            if (weapon.CurrentAmmo < 0)
            {
                weapon.CurrentAmmo = 0;
            }

            await context.SaveChangesAsync();
        }

        public async Task ChangeHp(int statusid, int point, int coeff)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = context.Status
                .Where(w => w.StatusId == statusid)
                .First();

            switch (point)
            {
                case 0:
                    status.Head += coeff;
                    if (status.Head < 0)
                        status.Head = 0;
                    break;

                case 1:
                    status.Body += coeff;
                    if (status.Body < 0)
                        status.Body = 0;
                    break;

                case 2:
                    status.LHand += coeff;
                    if (status.LHand < 0)
                        status.LHand = 0;
                    break;

                case 3:
                    status.RHand += coeff;
                    if (status.RHand < 0)
                        status.RHand = 0;
                    break;

                case 4:
                    status.LFoot += coeff;
                    if (status.LFoot < 0)
                        status.LFoot = 0;
                    break;

                case 5:
                    status.RFoot += coeff;
                    if (status.RFoot < 0)
                        status.RFoot = 0;
                    break;
            }

            await context.SaveChangesAsync();
        }

        public async Task CangeFullHp(int statusid, int coeff)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = context.Status
                .Where(w => w.StatusId == statusid)
                .First();

            status.Head += (int)(coeff * 0.1);
            if (status.Head < 0)
                status.Head = 0;

            status.Body += coeff;
            if (status.Body < 0)
                status.Body = 0;

            status.LHand += (int)(coeff * 0.3);
            if (status.LHand < 0)
                status.LHand = 0;

            status.RHand += (int)(coeff * 0.3);
            if (status.RHand < 0)
                status.RHand = 0;

            status.LFoot += (int)(coeff * 0.3);
            if (status.LFoot < 0)
                status.LFoot = 0;

            status.RFoot += (int)(coeff * 0.3);
            if (status.RFoot < 0)
                status.RFoot = 0;

            await context.SaveChangesAsync();
        }

        public string GetHp(ulong userid, ulong guildid)
        {
            using var context = _contextFactory.CreateDbContext();
            var guild = context.Guilds
                .Where(g => g.ServerId == guildid)
                .First();

            var user = context.Users
                .Where(u => u.UserDiscordID == userid & u.GuildsId == guild.Id)
                .First();

            var status = context.Status
                .Where(s => s.UserId == user.Id)
                .First();

            string result = $"Текущее состояние <@{userid}>\n" +
                $"\n**Голова: **{status.Head / (double) maxHp[0] * 100.0}%" +
                $"\n**Тело: **{status.Body / (double)maxHp[1] * 100.0}%" +
                $"\n**Левая рука: **{status.LHand / (double)maxHp[2] * 100.0}%" +
                $"\n**Правая рука: **{status.RHand / (double)maxHp[2] * 100.0}%" +
                $"\n**Левая нога: **{status.LFoot / (double)maxHp[3] * 100.0}%" +
                $"\n**Правая нога: **{status.RFoot / (double)maxHp[3] * 100.0}%";

            return result;
        }
    }
}

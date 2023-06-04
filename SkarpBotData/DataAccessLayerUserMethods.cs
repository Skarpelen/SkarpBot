//using SkarpBot.Data.Models;

//namespace SkarpBot.Data
//{//добавь транзакции
//    public partial class DataAccessLayer
//    {
//        public async Task<string> EquipWeapon(ulong userid, ulong guildid)
//        {
//            var guild = _dbContext.Guilds
//                .Where(g => g.ServerId == guildid)
//                .First();

//            var user = _dbContext.Users
//                .Where(u => u.UserDiscordId == userid && u.guild == guild)
//                .First();

//            var menu = _dbContext.MenuHandler
//                .Where(m => m.UserId == user.id && m.GuildId == guild.id)
//                .First();

//            string[] pickedvalue = menu.PickedValue.Split('.');

//            if (pickedvalue[1] != "W")
//            {
//                return "Ты долбаеб";
//            }

//            var weapon = _dbContext.Weapons
//                .Where(w => w.WeaponId == Convert.ToInt32(pickedvalue[0]))
//                .First();

//            if (weapon.InventoryName == "equipped")
//            {
//                weapon.InventoryName = "unequipped";
//                await _dbContext.SaveChangesAsync();
//                return $"Успешно сослано в инвентарь следующее снаряжение: {weapon.WeaponName}";
//            }
//            else
//            {
//                var equippedWeapon = _dbContext.Weapons
//                    .Where(w => w.InventoryName == "equipped" & w.UserId == user.id)
//                    .ToList();

//                if (equippedWeapon.Count() > 0)
//                {
//                    equippedWeapon.First().InventoryName = "unequipped";
//                }

//                weapon.InventoryName = "equipped";
//                await _dbContext.SaveChangesAsync();
//                return $"Успешно экипировано следующее снаряжение: {weapon.WeaponName}\n" +
//                    (equippedWeapon.Count() > 0 ? $"Успешно сослано в инвентарь следующее снаряжение: {equippedWeapon.First().WeaponName}" : "");
//            }
//        }

//        public async Task<string> ReloadWeapon(int weaponid, int maxAmmo)
//        {
//            var weapon = _dbContext.Weapons
//                .Where(w => w.WeaponId == weaponid)
//                .First();

//            weapon.CurrentAmmo = maxAmmo;

//            await _dbContext.SaveChangesAsync();
//            return $"Перезарядка `{weapon.WeaponName}` произведена успешно";
//        }

//        public async Task<string> DestroyInventory(ulong userid, ulong guildid)
//        {
//            var guild = _dbContext.Guilds
//                .Where(g => g.ServerId == guildid)
//                .First();

//            var user = _dbContext.Users
//                .Where(u => u.UserDiscordId == userid && u.guild == guild)
//                .First();

//            var menu = _dbContext.MenuHandler
//                .Where(m => m.UserId == user.id && m.GuildId == guild.id)
//                .First();

//            string[] pickedvalue = menu.PickedValue.Split('.');

//            if (pickedvalue[1] == "W")
//            {
//                var weapons = _dbContext.Weapons
//                    .Where(w => w.WeaponId == Convert.ToInt32(pickedvalue[0]))
//                    .First();

//                _dbContext.Weapons.Remove(weapons);
//                await _dbContext.SaveChangesAsync();
//                return "Снаряжение было удалено";
//            }

//            if (pickedvalue[1] == "G")
//            {
//                var grenades = _dbContext.Grenades
//                    .Where(w => w.GrenadeId == Convert.ToInt32(pickedvalue[0]))
//                    .First();

//                if (grenades.Amount == 1)
//                {
//                    _dbContext.Grenades.Remove(grenades);
//                    await _dbContext.SaveChangesAsync();
//                    return "Снаряжение было удалено";
//                }

//                grenades.Amount -= 1;
//                await _dbContext.SaveChangesAsync();
//                return $"Из инвентаря была удалена 1 граната типа `{grenades.GrenadeName}`. В инвентаре осталось {grenades.Amount}";
//            }

//            return "???";
//        }
//    }
//}

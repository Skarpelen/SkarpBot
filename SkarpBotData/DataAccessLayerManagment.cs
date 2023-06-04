//using Microsoft.EntityFrameworkCore;
//using SkarpBot.Data.Models;
//using System.Text;
//using Utf8Json;

//namespace SkarpBot.Data
//{
//    partial class DataAccessLayer
//    {
//        public async Task MenuPicker(ulong userid, ulong guildid, string pickedvalue)
//        {
//            var guild = _dbContext.Guilds
//                .Where(g => g.ServerId == guildid)
//                .First();

//            var user = _dbContext.Users
//                .Where(u => u.UserDiscordId == userid && u.guild == guild)
//                .First();

//            var result = _dbContext.MenuHandler.SingleOrDefault(mh => mh.UserId == user.id && mh.GuildId == guild.id);

//            if (result != null)
//            {
//                result.PickedValue = pickedvalue;
//                await _dbContext.SaveChangesAsync();
//                return;
//            }

//            _dbContext.Add(new MenuHandler
//            {
//                UserId = user.id,
//                GuildId = guild.id,
//                PickedValue = pickedvalue,
//            });

//            await _dbContext.SaveChangesAsync();
//        }
//    }
//}

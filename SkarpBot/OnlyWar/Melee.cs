namespace SkarpBot.OnlyWar
{
    using System.Threading.Tasks;
    using SkarpBot.Data;

    partial class Melee : Weapon
    {
        public Melee(int accuracy, int mode, string type, string armour, ulong id, bool aim)
            : base(accuracy, mode, type, armour, id, aim)
        {
            if (!error)
            {
                if (!(equipment.WeaponClass == 2 || equipment.WeaponClass == 3))
                {
                    error = true;
                }
            }
        }

        public Melee(string type)
            : base(type)
        {
            if (!error)
            {
                if (!(equipment.WeaponClass == 2 || equipment.WeaponClass == 3))
                {
                    error = true;
                }
            }
        }

        public async Task<string> Swing(int targetid)
        {
            shotValues.CalculateShot();
            if (shotValues.Degree < 0)
            {
                return $"Взмашок прошёл мимо цели\nСтепень успеха: {shotValues.Degree}";
            }

            Armour armour = new(aType);
            int result;

            if (equipment.DamageStats[0] > 30)
            {
                result = GetFullDamage(armour, equipment.DamageStats);
                await DataAccessLayer.ChangeFullHpAsync(targetid, -result);
                return $"*Поток красных частиц полностью дезинтегрировал бойца и помещение вокруг него.*\n\n\n\n\n\n\n\n\nИ҉͑͜͝С̶͢͞Ч̷̨͞Е̷̢͞З̵̧͞Н̵̧͝И̶̧͞.҈̨҇̆ О̸̢͠С̷̢͠О̴̢̕З̸̢̛Н̷̧͠А̷̧͞Й̴̛͜ С̶̧͡В̶̨͡О҉̡̋͞Ю̵̨҇ Н̷̡͝Е̷͢͝М̸̨͞О̶̢͡Щ҈҇̃͜Н̷̢҇О̷̧̕С̸̨͠Т̶̨̛Ь̶̢͡";
            }

            result = armour.Damage(shotValues.HittedPartId, equipment.DamageStats, equipment.Penetration);
            await DataAccessLayer.ChangeHpAsync(targetid, shotValues.HittedPartId, -result);
            return $"Взмашок успешно поразил {hitPoints[shotValues.HittedPartId]}, нанеся <@{shotValues.TargetId}> {result} урона.\nСтепень успеха: {shotValues.Degree}";
        }
    }
}

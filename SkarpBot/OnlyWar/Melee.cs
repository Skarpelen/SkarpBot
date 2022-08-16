namespace SkarpBot.OnlyWar
{
    using System.Threading.Tasks;
    using SkarpBot.Data;

    partial class Melee
    {
        public async Task<string> Swing(DataAccessLayer dataAccessLayer, int targetid)
        {
            if (GotHit())
            {
                Armour armour = new (aType);
                int result;

                if (equipment.damageStats[0] > 30)
                {
                    result = GetFullDamage(armour, equipment.damageStats);
                    await dataAccessLayer.CangeFullHp(targetid, -result);
                    return $"*Поток красных частиц полностью дезинтегрировал бойца и помещение вокруг него.*\n\n\n\n\n\n\n\n\nИ҉͑͜͝С̶͢͞Ч̷̨͞Е̷̢͞З̵̧͞Н̵̧͝И̶̧͞.҈̨҇̆ О̸̢͠С̷̢͠О̴̢̕З̸̢̛Н̷̧͠А̷̧͞Й̴̛͜ С̶̧͡В̶̨͡О҉̡̋͞Ю̵̨҇ Н̷̡͝Е̷͢͝М̸̨͞О̶̢͡Щ҈҇̃͜Н̷̢҇О̷̧̕С̸̨͠Т̶̨̛Ь̶̢͡";
                }

                result = armour.Damage(hittedPartID, equipment.damageStats, equipment.penetration);
                await dataAccessLayer.ChangeHp(targetid, hittedPartID, -result);
                return $"Взмашок успешно поразил {hitPoints[hittedPartID]}, нанеся <@{weapon.TargetId}> {result} урона.\nСтепень успеха: {Degree}";
            }

            return $"Взмашок прошёл мимо цели\nСтепень успеха: {Degree}";
        }
    }
}

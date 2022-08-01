namespace SkarpBot.OnlyWar
{
    using System.Threading.Tasks;
    using SkarpBot.Data;

    partial class Melee
    {
        public async Task<string> Swing(DataAccessLayer dataAccessLayer, int statusid)
        {
            if (GotHit())
            {
                Armour armour = new(aType);
                int result;
                string buffString;

                if (error)
                {
                    return "Неверное название оружия";
                }

                if (armour.error)
                {
                    return "Неверное название брони";
                }

                result = armour.Damage(hittedPartID, dmgStats, pen);
                await dataAccessLayer.ChangeHp(statusid, hittedPartID, result);
                return $"Взмашок успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\nСтепень успеха: {degree}";
            }

            return $"Взмашок прошёл мимо цели\nСтепень успеха: {degree}";
        }
    }
}

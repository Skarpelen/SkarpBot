namespace SkarpBot.OnlyWar
{
    using System.Threading.Tasks;
    using SkarpBot.Data;

    partial class Melee
    {
        public async Task<string> Swing(DataAccessLayer dataAccessLayer)
        {
            Armour armr = new(aType);
            int MeleeSkill = GetValue();
            hitIndex = HitPointCalculate(hitValue);

            if (error | armr.error)
            {
                return "Неверное название оружия или брони";
            }

            if (MeleeSkill - value > 0)
            {
                var CalcDmg = await CalcDamage(armr, hitPoints, hitIndex, dataAccessLayer);
                return "Взмашок успешно поразил " + hitPoints[hitIndex] +
                    CalcDmg;
            }
            else
            {
                return $"Взмашок прошёл мимо цели\nСтепень успеха: {degree}";
            }
        }

        private async Task<string> CalcDamage(Armour armour, string[] hitPoint, int index, DataAccessLayer dataAccessLayer)
        {
            int roll = armour.Damage(index, dmgStats, pen);
            await dataAccessLayer.ChangeHp(targetId, index, roll);
            string result = ", нанеся цели " + roll + " урона.\n";
            return result + "Степень успеха: " + degree;
        }
    }
}

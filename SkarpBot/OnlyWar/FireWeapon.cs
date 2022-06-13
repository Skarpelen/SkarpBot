using SkarpBot.Data;

namespace SkarpBot.OnlyWar
{
    partial class FireWeapon : Weapon
    {
        public void Shoot(out int point, out int dmg)
        {
            Armour armour = new (AType);
            switch (WClass)
            {
                case 0:
                    point = HitPointCalculate(Point());
                    dmg = armour.Damage(point, dmgInd, pen);
                    break;

                default:
                    point = -1;
                    dmg = -1;
                    break;
            }
        }

        private int Point()
        {
            Random rnd = new ();
            int value = rnd.Next(100);
            int hitPoint = (value / 10) + ((value % 10) * 10);
            int ballisticSkill = accuracy + buffs[0, aim] + buffs[1, range] + buffs[2, mode];
            degree = (ballisticSkill - value) / 10.0;

            return hitPoint;
        }

        // gggggggggggggg

        public async Task<string> Shoot(DataAccessLayer dataAccessLayer)
        {
            Armour armr = new (AType);
            switch (WClass)
            {
                case 0:
                    int BallisticSkill = GetValue();
                    hitIndex = HitPointCalculate(hitValue);
                    var output = await Output(armr, BallisticSkill, dataAccessLayer);
                    return output;

                case 1:
                    hitIndex = 3;
                    return "Выстрел успешно поразил " + CalculateDmg(armr, hitPoints, hitIndex,
                    Convert.ToInt32(Math.Floor(degree)));

                default:
                    return "Ошибка в классе оружия";
            }
        }

        public string CalledShot()
        {
            Armour armr = new(AType);
            if (GetIndex() == -1)
            {
                return "Некорректная часть тела";
            }

            switch (WClass)
            {
                case 0:
                    hitIndex = GetIndex();
                    int BallisticSkill = GetValue();
                    return ""; //Output(armr, BallisticSkill);

                case 1:
                    return "Невозможно совершить прицельный выстрел из этого оружия";

                default:
                    return "Ошибка в классе оружия";
            }

        }

        private int GetIndex()
        {
            switch (aimPoint)
            {
                case "голов":
                    return 0;

                case "рук":
                    return 1;

                case "тор":
                    return 3;

                case "ног":
                    return 4;
            }

            return -1;
        }

        private async Task<string> Output(Armour armr, int BallisticSkill, DataAccessLayer dataAccessLayer)
        {
            if (error | armr.error)
            {
                return "Неверное название оружия или брони";
            }

            if (CheckMode(mode))
            {
                return "Выбранное оружие не может стрелять в этом режиме";
            }

            if (BallisticSkill - value > 0)
            {
                var CalcDmg = await CalculateDmg(armr, hitPoints, hitIndex, Convert.ToInt32(Math.Floor(degree)), dataAccessLayer);
                return "Выстрел успешно поразил " + hitPoints[hitIndex] +
                    CalcDmg;
            }
            else
            {
                return $"Выстрел{((mode != 0 & mode != 3) ? "ы" : string.Empty)} прош{((mode != 0 & mode != 3) ? "ли" : "ёл")} мимо цели\nСтепень успеха: {degree}";
            }
        }

        private static int[] Multiple(int ind)
        {
            int[,] hit = new int[,] { { 0, 0, 2, 3, 1, 3 }, { 1, 1, 3, 0, 3, 2 }, { 2, 2, 3, 0, 3, 2 },
                { 3, 3, 1, 0, 2, 3 }, { 4, 4, 3, 1, 0, 3 }, { 5, 5, 3, 1, 0, 3 },
            };
            int[] res = new int[6];

            for (int i = 0; i < 6; i++)
            {
                res[i] = hit[ind, i];
            }

            return res;
        }

        public async Task<string> CalculateDmg(Armour armr, string[] hitPoint, int index, int lim, DataAccessLayer dataAccessLayer)
        {
            int roll = armr.GetDamage(hitPoint[index], RollDmg(dmgInd), pen);
            await dataAccessLayer.ChangeHp(targetId, index, roll);
            string result = ", нанеся цели " + roll + " урона.\n";
            if ((mode == 0 || mode == 3) && shots[mode] != 0)
            {
                return result + "Степень успеха: " + degree;
            }

            if (lim > shots[mode])
            {
                lim = shots[mode];
            }

            int[] hits = Multiple(index);
            int k = 0;
            if (shots[mode] != 0)
            {
                for (int i = 1; i <= lim; i++)
                {
                    if (i == 6)
                    {
                        k = 6;
                    }

                    roll = armr.GetDamage(hitPoint[hits[i - k]], RollDmg(dmgInd), pen);
                    await dataAccessLayer.ChangeHp(targetId, hits[i - k], roll);
                    result += "Дополнительное попадание в " + hitPoint[hits[i - k]] + " с уроном " +
                        roll + "\n";
                }

                result += "Степень успеха: " + degree;
                return result;
            }
            else
            {
                return _ = "ошибка";
            }
        }

        public string CalculateDmg(Armour armr, string[] hitPoint, int index, int lim)
        {
            string result = ", нанеся цели " + armr.GetDamage(hitPoint[index], RollDmg(dmgInd), pen) + " урона.\n";
            if ((mode == 0 || mode == 3) && shots[mode] != 0)
            {
                return result + "Степень успеха: " + degree;
            }

            if (lim > shots[mode])
            {
                lim = shots[mode];
            }

            int[] hits = Multiple(index);
            int k = 0;
            if (shots[mode] != 0)
            {
                for (int i = 1; i <= lim; i++)
                {
                    if (i == 6)
                    {
                        k = 6;
                    }

                    result += "Дополнительное попадание в " + hitPoint[hits[i - k]] + " с уроном " +
                        armr.GetDamage(hitPoint[hits[i - k]], RollDmg(dmgInd), pen) + "\n";
                }

                result += "Степень успеха: " + degree;
                return result;
            }
            else
                return _ = "ошибка";
        }

        public bool CheckMode(int mode)
        {
            if (shots[mode] == 0)
            {
                return true;
            }

            return false;
        }

        public void FillUp()
        {
            int k = 0;
            for (int i = 0; i < qualities.Length; i++)
            {
                if (qualities[i])
                {
                    switch (i)
                    {
                        case 0:
                            while (degree - 2 > 0 & k < 2)
                            {
                                dmgInd += RollDmg(0);
                            }

                            break;
                    }
                }
            }
        }
    }
}

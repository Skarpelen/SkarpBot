﻿namespace SkarpBot.OnlyWar
{
    using SkarpBot.Data;

    partial class FireWeapon : Weapon
    {
        public async Task<string> Shoot(DataAccessLayer dataAccessLayer)
        {
            Armour armr = new (aType);
            switch (wClass)
            {
                case 0:
                    int BallisticSkill = GetValue();
                    hitIndex = HitPointCalculate(hitValue);
                    var output = await Output(armr, BallisticSkill, dataAccessLayer);
                    return output;

                case 1:
                    hitIndex = 3;
                    var CalcDmg = await CalculateDmg(armr, hitPoints, hitIndex, Convert.ToInt32(Math.Floor(degree)), dataAccessLayer);
                    return "Выстрел успешно поразил " + CalcDmg;

                default:
                    return "Ошибка в классе оружия";
            }
        }

        public async Task<string> CalledShot(DataAccessLayer dataAccessLayer)
        {
            Armour armr = new(aType);
            if (GetIndex() == -1)
            {
                return "Некорректная часть тела";
            }

            switch (wClass)
            {
                case 0:
                    hitIndex = GetIndex();
                    int BallisticSkill = GetValue();
                    var output = await Output(armr, BallisticSkill, dataAccessLayer);
                    return output;

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
                return $"Выстрел{((mode != 0 & mode != 3) ? "ы" : string.Empty)} прош{((mode != 0 & mode != 3) ? "ли" : "ёл")} мимо цели\nПотрачен" +
                    $"{((mode != 0 & mode != 3) ? "о" : string.Empty)} {shots[mode]} патрон{((mode != 0 & mode != 3) ? "ов" : string.Empty)}\nСтепень успеха: {degree}";
            }
        }

        private static int[] Multiple(int ind)
        {
            int[,] hit = new int[,] { { 0, 0, 2, 1, 3, 1 }, { 1, 1, 3, 0, 2, 1 }, { 2, 2, 1, 0, 1, 3 },
                { 3, 3, 1, 0, 1, 2 }, { 4, 4, 1, 2, 0, 1 }, { 5, 5, 1, 3, 0, 1 },
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
            if (shots[mode] != 0)
            {
                int sniper = 0;
                if (qualities[0] || aim == 0)
                {
                    sniper = (int)degree;
                }

                int roll = armr.Damage(index, dmgStats, pen, sniper);
                await dataAccessLayer.ChangeHp(targetId, index, roll);
                string result = ", нанеся цели " + roll + " урона.\n";
                if ((mode == 0 || mode == 3) && shots[mode] != 0)
                {
                    return result + "Потрачен 1 патрон.\nСтепень успеха: " + degree;
                }

                if (lim > shots[mode])
                {
                    lim = shots[mode];
                }

                int[] hits = Multiple(index);
                int k = 0;
                for (int i = 1; i < lim; i++)
                {
                    if (i == 6)
                    {
                        k = 6;
                    }

                    roll = armr.Damage(hits[i - k], dmgStats, pen);
                    await dataAccessLayer.ChangeHp(targetId, hits[i - k], roll);
                    result += "Дополнительное попадание в " + hitPoint[hits[i - k]] + " с уроном " +
                        roll + "\n";
                }

                result += $"Потрачено {shots[mode]} патронов\nСтепень успеха: " + degree;
                return result;
            }
            else
            {
                return _ = "ошибка";
            }
        }

        public bool CheckMode(int mode)
        {
            if (shots[mode] == 0)
            {
                return true;
            }

            return false;
        }
    }
}

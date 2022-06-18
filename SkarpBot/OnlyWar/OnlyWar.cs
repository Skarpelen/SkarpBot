namespace SkarpBot.OnlyWar
{
    internal class OnlyWar
    {
        int Accuracy, Aim, mode;
        string WType, AType;
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlyWar"/> class.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="x3"></param>
        /// <param name="x4"></param>
        public OnlyWar(int x1, int x2, string x3, string x4)
        {

            Accuracy = x1;
            Aim = 1;
            //Range = x3;
            //Rate = x4;
            mode = x2;
            WType = x3;
            AType = x4;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlyWar"/> class.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="extrax2"></param>
        /// <param name="x2"></param>
        /// <param name="x3"></param>
        /// <param name="x4"></param>
        public OnlyWar(int x1, int extrax2, int x2, string x3, string x4)
        {

            Accuracy = x1;
            Aim = extrax2;
            mode = x2;
            WType = x3;
            AType = x4;
        }

        /*public string Start()
        {
            Random rnd = new();
            string[] hitPoints = new string[] { "голову", "левую руку", "правую руку", "торс", "левую ногу", "правую ногу" };
            int[,] buffs = new int[,] { { 10, 0, 20, 0 }, { 10, 0, -10, -20 }, { 20, 10, 0, -20 } };
            int Value = rnd.Next(100);
            int hitValue = (Value / 10) + (Value % 10 * 10);
            int hitIndex = HitPointCalculate(hitValue);
            var fireweapon = new FireWeapon(175, mode, WType, aType);
            Armour armr = new(aType);

            if (fireweapon.error | armr.error)
            {
                return "Неверное название оружия или брони";
            }

            if (fireweapon.CheckMode(mode))
            {
                return "Выбранное оружие не может стрелять в этом режиме";
            }

            int BallisticSkill = Accuracy + buffs[0, Aim] + buffs[1, 1]+ buffs[2, mode];
            fireweapon.degree = (BallisticSkill - Value) / 10.0;
            if (BallisticSkill - Value > 0)
            {
                return "Выстрел успешно поразил " + hitPoints[hitIndex] +
                    fireweapon.CalculateDmg(armr, hitPoints, hitIndex,
                    Convert.ToInt32(Math.Floor(fireweapon.degree)));
            }
            else
            {
                return $"Выстрел{(mode != 0 ? "ы" : string.Empty)} прош{(mode != 0 ? "ли" : "ёл")} мимо цели\nСтепень успеха: {fireweapon.degree}";
            }
        }*/

        private static int HitPointCalculate(int hitValue)
        {
            if (hitValue > 0 & hitValue < 11)
            {
                return 0;
            }

            if (hitValue > 10 & hitValue < 31)
            {
                if (hitValue > 20)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }

            if (hitValue > 30 & hitValue < 71)
            {
                return 3;
            }

            if (hitValue > 70 & hitValue < 100 || hitValue == 0)
            {
                if (hitValue == 0 || hitValue > 85)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }

            return 6;
        }
    }
}

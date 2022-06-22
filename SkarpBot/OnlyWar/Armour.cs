namespace SkarpBot.OnlyWar
{
    public class Armour
    {
        private readonly int[] human = new int[6];
        public bool error = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Armour"/> class.
        /// Конструктор брони.
        /// </summary>
        /// <param name="type">Тип брони.</param>
        public Armour(string type)
        {
            switch (type)
            {
                case "легкая":
                    human[0] = 0;
                    human[1] = 2;
                    human[2] = 1;
                    human[3] = 1;
                    human[4] = 1;
                    human[5] = 1;
                    break;

                case "средняя":
                    human[0] = 2;
                    human[1] = 5;
                    human[2] = 3;
                    human[3] = 3;
                    human[4] = 3;
                    human[5] = 3;
                    break;

                case "хорошая":
                    human[0] = 7;
                    human[1] = 12;
                    human[2] = 9;
                    human[3] = 9;
                    human[4] = 9;
                    human[5] = 9;
                    break;

                case "тяжелая":
                    human[0] = 10;
                    human[1] = 15;
                    human[2] = 12;
                    human[3] = 12;
                    human[4] = 12;
                    human[5] = 12;
                    break;

                case "гок":
                    human[0] = 7;
                    human[1] = 8;
                    human[2] = 6;
                    human[3] = 6;
                    human[4] = 5;
                    human[5] = 5;
                    break;

                case "дом":
                    human[0] = 4;
                    human[1] = 4;
                    human[2] = 3;
                    human[3] = 3;
                    human[4] = 3;
                    human[5] = 3;
                    break;

                case "левантий":
                    human[0] = 5;
                    human[1] = 5;
                    human[2] = 4;
                    human[3] = 4;
                    human[4] = 4;
                    human[5] = 4;
                    break;

                case "электрик":
                    human[0] = 7;
                    human[1] = 7;
                    human[2] = 6;
                    human[3] = 6;
                    human[4] = 5;
                    human[5] = 5;
                    break;

                case "крутые":
                    human[0] = 7;
                    human[1] = 8;
                    human[2] = 6;
                    human[3] = 6;
                    human[4] = 5;
                    human[5] = 5;
                    break;

                case "мстители":
                    human[0] = 70;
                    human[1] = 80;
                    human[2] = 60;
                    human[3] = 60;
                    human[4] = 50;
                    human[5] = 50;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        public int Damage(int point, int[] stats, int pen, int sniper = 0)
        {
            Random random = new Random();
            int roll = 0;
            int additional = 0;
            for (int i = 0; i < stats[0]; i++)
            {
                roll += random.Next(11);
            }

            if (sniper != 0)
            {
                for (int i = 1; i < 3 && sniper - 2 * i > 0; i++)
                {
                    roll += random.Next(11);
                }
            }

            return GetDamage(point, roll + stats[1], pen);
        }

        private int GetDamage(int point, int roll, int pen)
        {
            int buf = human[point] - pen;
            if (buf < 0)
            {
                buf = 0;
            }

            return ZeroTurn(roll - buf);
        }

        protected static int RollDmg(int add)
        {
            Random rnd1 = new();
            return rnd1.Next(11) + add;
        }

        /*public int GetDamage(string hitPoint, int roll, int pen)
        {
            int[] buf = new int[4];
            for (int i = 0; i < human.Length; i++)
            {
                buf[i] = human[i] - pen;
                if (buf[i] < 0)
                {
                    buf[i] = 0;
                }
            }

            return hitPoint switch
            {
                "голову" => ZeroTurn(roll - buf[0]),
                "торс" => ZeroTurn(roll - buf[1]),
                "правую руку" => ZeroTurn(roll - buf[2]),
                "левую руку" => ZeroTurn(roll - buf[2]),
                "правую ногу" => ZeroTurn(roll - buf[3]),
                "левую ногу" => ZeroTurn(roll - buf[3]),
                _ => 0,
            };
        }*/

        private static int ZeroTurn(int num)
        {
            if (num < 0)
            {
                return 0;
            }

            return num;
        }
    }
}

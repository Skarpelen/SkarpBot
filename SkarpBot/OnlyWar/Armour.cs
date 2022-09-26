namespace SkarpBot.OnlyWar
{
    public class Armour
    {
        public bool Error = false;
        private readonly int[] human = new int[6];

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
                    human[0] = 1;
                    human[1] = 3;
                    human[2] = 2;
                    human[3] = 2;
                    human[4] = 2;
                    human[5] = 2;
                    break;

                case "средняя":
                    human[0] = 5;
                    human[1] = 9;
                    human[2] = 6;
                    human[3] = 6;
                    human[4] = 6;
                    human[5] = 6;
                    break;

                case "улучшенная":
                    human[0] = 7;
                    human[1] = 11;
                    human[2] = 7;
                    human[3] = 7;
                    human[4] = 8;
                    human[5] = 8;
                    break;

                case "хорошая":
                    human[0] = 9;
                    human[1] = 14;
                    human[2] = 11;
                    human[3] = 11;
                    human[4] = 12;
                    human[5] = 12;
                    break;

                case "тяжелая":
                    human[0] = 12;
                    human[1] = 18;
                    human[2] = 14;
                    human[3] = 14;
                    human[4] = 15;
                    human[5] = 15;
                    break;

                case "мститель":
                    human[0] = 100;
                    human[1] = 105;
                    human[2] = 102;
                    human[3] = 102;
                    human[4] = 102;
                    human[5] = 102;
                    break;

                default:
                    Error = true;
                    break;
            }
        }

        public int Damage(int point, int[] stats, int pen, int sniper = 0)
        {
            Random random = new Random();
            int roll = 0;
            for (int i = 0; i < stats[0]; i++)
            {
                roll += random.Next(11);
            }

            if (sniper != 0)
            {
                for (int i = 1; i < 3 && sniper - (2 * i) > 0; i++)
                {
                    roll += random.Next(11);
                }
            }

            if (roll < 0)
            {
                roll = 0;
            }

            return GetDamage(point, roll + stats[1], pen);
        }

        public double DefenceDegree()
        {
            return human[1] / 20.0;
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

namespace SkarpBot.OnlyWar
{
    public class Armour
    {
        private readonly int[] human = new int[4];
        public bool error = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Armour"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Armour(string type)
        {
            switch (type)
            {
                case "легкая":
                    human[0] = 0;
                    human[1] = 2;
                    human[2] = 1;
                    human[3] = 1;
                    break;

                case "средняя":
                    human[0] = 3;
                    human[1] = 6;
                    human[2] = 5;
                    human[3] = 4;
                    break;

                case "хорошая":
                    human[0] = 5;
                    human[1] = 10;
                    human[2] = 7;
                    human[3] = 6;
                    break;

                case "тяжелая":
                    human[0] = 7;
                    human[1] = 12;
                    human[2] = 9;
                    human[3] = 9;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        internal int Damage(int point, int roll, int pen)
        {
            return GetDamage(point, roll, pen);
        }

        private int GetDamage(int point, int roll, int pen)
        {
            int buf = human[point] - pen;
            if (buf < 0)
            {
                buf = 0;
            }

            return ZeroTurn(RollDmg(roll) - buf);
        }

        protected static int RollDmg(int add)
        {
            Random rnd1 = new();
            return rnd1.Next(11) + add;
        }

        // ggggggggggg

        public int GetDamage(string hitPoint, int roll, int pen)
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

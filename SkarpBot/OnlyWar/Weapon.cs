namespace SkarpBot.OnlyWar
{
    using SkarpBot.OnlyWar.Classes;

    public class Weapon
    {
        public bool Error = false;
        protected DamageDealer weapon;
        protected Equipment equipment;
        protected readonly string[] hitPoints = new string[] { "голову", "торс", "левую руку", "правую руку", "левую ногу", "правую ногу" };
        protected string aType;

        /// <summary>
        /// Переведи число от 0 до 99 в айди части тела.
        /// </summary>
        /// <param name="d100">Число от 0 до 99.</param>
        /// <returns>Айди части тела.</returns>
        protected int GetHittedPartID(int d100)
        {
            int d100Reversed = (d100 / 10) + ((d100 % 10) * 10);

            if (d100Reversed > 0 & d100Reversed < 11)
            {
                return 0;
            }

            if (d100Reversed > 10 & d100Reversed < 31)
            {
                if (d100Reversed > 20)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }

            if (d100Reversed > 30 & d100Reversed < 71)
            {
                return 1;
            }

            if (d100Reversed > 70 & d100Reversed < 100 || d100Reversed == 0)
            {
                if (d100Reversed == 0 || d100Reversed > 85)
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

        /// <summary>
        /// Дамажит полностью все тело(все в БД заносит само).
        /// </summary>
        /// <param name="armour">Броня цели.</param>
        /// <param name="dmgStats">Статы оружия.</param>
        /// <returns>Урон в тело(максимальный из всех значений).</returns>
        protected int GetFullDamage(Armour armour, int[] dmgStats)
        {
            Random random = new Random();
            int roll = dmgStats[1];
            for (int i = 0; i < dmgStats[0]; i++)
            {
                roll += random.Next(11);
            }

            double defcoeff = armour.DefenceDegree();
            return (int)(roll - (roll * defcoeff));
        }

        protected static int RollDmg(int[] stats)
        {
            Random rnd1 = new ();
            int result = 0;
            for (int i = 0; i < stats[0]; i++)
            {
                result += rnd1.Next(11);
            }

            return result + stats[1];
        }
    }
}

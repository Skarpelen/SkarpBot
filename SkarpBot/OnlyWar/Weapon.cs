namespace SkarpBot.OnlyWar
{
    using Discord;
    using SkarpBot.OnlyWar.Classes;

    public class Weapon
    {
        protected DamageDealer shotValues;
        protected Equipment equipment;
        protected readonly string[] hitPoints = new string[] { "голову", "торс", "левую руку", "правую руку", "левую ногу", "правую ногу" };
        protected string aType;
        protected bool error;

        public Weapon(int accuracy, int mode, string type, string armour, ulong id, bool aim)
        {
            shotValues = new DamageDealer(accuracy, mode, id, aim);
            aType = armour;
            equipment = new Equipment(type);
            error = equipment.Error;
        }

        public Weapon(string type)
        {
            equipment = new Equipment(type);
            error = equipment.Error;
        }

        public bool Error
        {
            get { return error; }
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
            Random rnd1 = new();
            int result = 0;
            for (int i = 0; i < stats[0]; i++)
            {
                result += rnd1.Next(11);
            }

            return result + stats[1];
        }

        public Embed GetWeaponInfo()
        {
            return equipment.FormEquipmentInfo();
        }
    }
}

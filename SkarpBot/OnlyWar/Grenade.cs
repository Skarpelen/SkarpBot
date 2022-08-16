using SkarpBot.OnlyWar.Classes;

namespace SkarpBot.OnlyWar
{
    public class Grenade : Weapon
    {
        private int effectType;
        private string type;

        public Grenade(int accuracy, string type)
        {
            weapon = new DamageDealer(accuracy, 0, 1, false);
            Type = type;
            equipment = new Equipment(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grenade"/> class.
        /// </summary>
        /// <param name="type">Название гранаты.</param>
        public Grenade(string type)
        {
            equipment = new Equipment(type);
        }

        public int EffectType { get => effectType; set => effectType = value; }

        public string Type { get => type; set => type = value; }

        public string GrenadeThrow()
        {
            if (Error)
            {
                return "Неверное название гранаты";
            }

            if (GotHit())
            {
                return ThrowResult();
            }
            else
            {
                return ThrowMiss();
            }
        }

        private string ThrowMiss()
        {
            string result = $"Граната типа \"{Type}\" была брошена криво и, в результате столкновения с препятствием, " +
                $"отскочила ";
            string[] direction = new string[8]
            {
                "в сторону изначального направления",
                "на 45° правее изначального направления",
                "на 90° правее изначального направления",
                "на 135° правее изначального направления",
                "на 180° от изначального направления",
                "на 45° левее изначального направления",
                "на 90° левее изначального направления",
                "на 135° левее изначального направления",
            };
            Random rnd = new ();
            result += direction[rnd.Next(8)] + ", после чего ";
            switch (EffectType)
            {
                case 0:
                    result += $"нанесла в области действия {RollDmg(equipment.DamageStats)} единиц урона.";
                    break;

                case 1:
                    result += $"применила свои эффекты в области действия";
                    break;
            }

            return result;
        }

        private string ThrowResult()
        {
            return EffectType switch
            {
                0 => $"Граната типа \"{Type}\" успешно долетела до цели и нанесла в области действия {RollDmg(equipment.DamageStats)} единиц урона.",
                1 => $"Граната типа \"{Type}\" успешно долетела до цели и применила свои эффекты в области действия",
                _ => "доделай",
            };
        }
    }
}

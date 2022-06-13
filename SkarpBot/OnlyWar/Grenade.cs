namespace SkarpBot.OnlyWar
{
    public class Grenade : Weapon
    {
        private int effectType;
        private string type;
        /// <summary>
        /// Initializes a new instance of the <see cref="Grenade"/> class.
        /// </summary>
        /// <param name="accuracy"></param>
        /// <param name="type"></param>
        public Grenade(int accuracy, string type)
        {
            Type = type;
            this.accuracy = accuracy;
            switch (type)
            {
                case "осколочная":
                    EffectType = 0;
                    dmgInd = RollDmg(0) + RollDmg(0);
                    break;

                case "зажигательная":
                    EffectType = 0;
                    dmgInd = RollDmg(3);
                    break;

                case "дымовая":
                    EffectType = 1;
                    break;

                case "светошумовая":
                    EffectType = 1;
                    break;

                case "тактическая":
                    EffectType = 1;
                    break;

                case "газовая":
                    EffectType = 0;
                    dmgInd = RollDmg(0) + RollDmg(0) + RollDmg(0);
                    break;

                default:
                    error = true;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grenade"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Grenade(string type)
        {
            Type = type;
            switch (type)
            {
                case "осколочная":
                    EffectType = 0;
                    pen = 0;
                    stats = new string[] { "Метательное", "15", "2к10", "0.5" };
                    qualities[3] = true;
                    x = 3;
                    break;

                case "зажигательная":
                    EffectType = 0;
                    pen = 6;
                    stats = new string[] { "Метательное", "15", "1к10+3", "0.5" };
                    qualities[3] = true;
                    qualities[5] = true;
                    x = 3;
                    break;

                case "дымовая":
                    EffectType = 1;
                    pen = 0;
                    stats = new string[] { "Метательное", "25", "-", "0.5" };
                    qualities[4] = true;
                    x = 6;
                    break;

                case "светошумовая":
                    EffectType = 1;
                    pen = 0;
                    stats = new string[] { "Метательное", "15", "-", "0.5" };
                    qualities[4] = true;
                    x = 3;
                    break;

                case "тактическая":
                    EffectType = 1;
                    pen = 0;
                    stats = new string[] { "Метательное", "15", "-", "0.5" };
                    qualities[5] = true;
                    break;

                case "газовая":
                    EffectType = 1;
                    pen = 100;
                    stats = new string[] { "Метательное", "15", "3к10", "0.5" };
                    qualities[3] = true;
                    x = 3;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        public int EffectType { get => effectType; set => effectType = value; }
        public string Type { get => type; set => type = value; }
        public string GrenadeThrow()
        {
            if (error)
            {
                return "Неверное название гранаты";
            }

            if (GetValue() - value > 0)
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
                "на 135° левее изначального направления"
            };
            Random rnd = new();
            result += direction[rnd.Next(8)] + ", после чего ";
            switch (EffectType)
            {
                case 0:
                    result += $"нанесла в области действия {dmgInd} единиц урона.";
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
                0 => $"Граната типа \"{Type}\" успешно долетела до цели и нанесла в области действия {dmgInd} единиц урона.",
                1 => $"Граната типа \"{Type}\" успешно долетела до цели и применила свои эффекты в области действия",
                _ => "доделай",
            };
        }
    }
}

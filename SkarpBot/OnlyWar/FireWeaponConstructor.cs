using Discord;

namespace SkarpBot.OnlyWar
{
    partial class FireWeapon : Weapon
    {
        private readonly int[] shots;
        private int WClass;
        private string aimPoint;
        private readonly ulong targetId;
        readonly string[] hitPoints = new string[] { "голову", "торс", "левую руку", "правую руку", "левую ногу", "правую ногу" };

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// Стандартный вид стрельбы
        /// </summary>
        /// <param name="accuracy">Показатель меткости</param>
        /// <param name="m">Режим стрельбы</param>
        /// <param name="type">Тип оружия</param>
        /// <param name="atype">Тип брони противника</param>
        public FireWeapon(int accuracy, int m, string type, string atype)
        {
            mode = m;
            this.accuracy = accuracy;
            aim = 1;
            range = 1;
            // Rate = x4;
            AType = atype;
            WClass = 0;
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0 };
                    dmgInd = -2;
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgInd = 2;
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgInd = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgInd = 4;
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    WClass = 1;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        public FireWeapon(int accuracy, int m, string type, string atype, ulong id)
        {
            mode = m;
            this.accuracy = accuracy;
            aim = 1;
            range = 1;
            WClass = 0;
            AType = atype;
            targetId = id;
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0 };
                    dmgInd = -2;
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgInd = 2;
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgInd = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgInd = 4;
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    WClass = 1;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// </summary>
        /// <param name="accuracy"></param>
        /// <param name="type"></param>
        /// <param name="atype"></param>
        /// <param name="aim"></param>
        public FireWeapon(int accuracy, string type, string atype, string aim)
        {
            aimPoint = aim;
            mode = 3;
            this.accuracy = accuracy;
            this.aim = 1;
            range = 1;
            AType = atype;
            WClass = 0;
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0, 1 };
                    dmgInd = -2;
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10, 1 };
                    dmgInd = 2;
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0, 1 };
                    dmgInd = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0, 1 };
                    dmgInd = 5;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8, 1 };
                    dmgInd = 4;
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    WClass = 1;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// </summary>
        /// <param name="type"></param>
        public FireWeapon(string type)
        {
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0 };
                    dmgInd = -2;
                    stats = new string[] { "Пистолет", "30", "9", "1.5" };
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgInd = 2;
                    stats = new string[] { "Основное", "100", "30", "5" };
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgInd = 8;
                    qualities[1] = true;
                    stats = new string[] { "Основное", "30", "18", "6.5" };
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    qualities[0] = true;
                    stats = new string[] { "Основное", "200", "20", "5" };
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgInd = 4;
                    stats = new string[] { "Тяжёлое", "100", "75", "30" };
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgInd = 5;
                    stats = new string[] { "Тяжёлое", "30", "10", "45" };
                    qualities[5] = true;
                    qualities[6] = true;
                    break;

                default:
                    error = true;
                    break;
            }
        }
    }
}

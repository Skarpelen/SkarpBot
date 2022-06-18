﻿namespace SkarpBot.OnlyWar
{
    partial class FireWeapon : Weapon
    {
        private readonly int[] shots;
        private readonly int wClass;
        private readonly ulong targetId;
        private string aimPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// Стандартный вид стрельбы.
        /// </summary>
        /// <param name="accuracy">Показатель меткости.</param>
        /// <param name="m">Режим стрельбы.</param>
        /// <param name="type">Тип оружия.</param>
        /// <param name="atype">Тип брони противника.</param>

        public FireWeapon(int accuracy, int m, string type, string atype, ulong id)
        {
            mode = m;
            this.accuracy = accuracy;
            aim = 1;
            range = 1;
            wClass = 0;
            aType = atype;
            targetId = id;
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = -2;
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    wClass = 1;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// Конструктор для прицельной стрельбы.
        /// </summary>
        /// <param name="accuracy">Параметр меткости.</param>
        /// <param name="type">Тип оружия.</param>
        /// <param name="aim">Место куда целятся.</param>
        /// <param name="atype">Броня противника.</param>
        /// <param name="id">Дискорд айди противника.</param>
        public FireWeapon(int accuracy, string type, string aim, string atype, ulong id)
        {
            aimPoint = aim;
            mode = 3;
            this.accuracy = accuracy;
            this.aim = 1;
            range = 1;
            wClass = 0;
            aType = atype;
            targetId = id;
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = -2;
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    wClass = 1;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// Выводит информацию об оружии.
        /// </summary>
        /// <param name="type">Тип оружия</param>
        public FireWeapon(string type)
        {
            switch (type)
            {
                case "пистолет":
                    pen = 0;
                    shots = new int[] { 1, 2, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = -2;
                    stats = new string[] { "Пистолет", "30", "9", "1.5" };
                    break;

                case "автомат":
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    stats = new string[] { "Основное", "100", "30", "5" };
                    break;

                case "дробовик":
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 8;
                    qualities[1] = true;
                    stats = new string[] { "Основное", "30", "18", "6.5" };
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    qualities[0] = true;
                    stats = new string[] { "Основное", "200", "20", "5" };
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    stats = new string[] { "Тяжёлое", "100", "75", "30" };
                    break;

                case "огнемет":
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
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
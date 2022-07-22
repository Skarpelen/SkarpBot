namespace SkarpBot.OnlyWar
{
    partial class FireWeapon : Weapon
    {
        private readonly int[] shots;
        private readonly int wClass;
        private readonly ulong targetId;
        private string aimPoint;

        public FireWeapon(int accuracy, int mode, string type, string aType, ulong id, bool aim)
        {
            this.mode = mode;
            this.accuracy = accuracy;
            this.aim = aim;
            this.aType = aType;
            hittedPartID = -1;
            targetId = id;
            range = 1;
            wClass = 0;

            switch (type)
            {
                case "пистолет":
                    name = "пистолет";
                    pen = 4;
                    shots = new int[] { 1, 2, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = -2;
                    weight = 1.5;
                    maxRange = 30;
                    maxAmmo = 9;
                    break;

                case "транквил":
                    name = "транквил";
                    pen = 1;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 0;
                    dmgStats[1] = 1;
                    weight = 1.5;
                    maxRange = 30;
                    maxAmmo = 9;
                    break;

                case "автомат":
                    name = "автомат";
                    pen = 2;
                    shots = new int[] { 1, 3, 10 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    weight = 5;
                    maxRange = 100;
                    maxAmmo = 30;
                    break;

                case "фестон":
                    name = "фестон";
                    pen = 3;
                    shots = new int[] { 1, 5, 15 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 0;
                    weight = 5;
                    maxRange = 100;
                    maxAmmo = 50;
                    break;

                case "п90":
                    name = "п90";
                    pen = 1;
                    shots = new int[] { 1, 5, 15 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 0;
                    weight = 5;
                    maxRange = 100;
                    maxAmmo = 50;
                    break;

                case "дробовик":
                    name = "дробовик";
                    pen = 0;
                    shots = new int[] { 1, 3, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 8;
                    weight = 6.5;
                    maxRange = 30;
                    maxAmmo = 18;
                    qualities[1] = true;
                    break;

                case "винтовка":
                    name = "винтовка";
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    weight = 5;
                    maxRange = 200;
                    maxAmmo = 20;
                    qualities[0] = true;
                    break;

                case "пулемет":
                    name = "пулемет";
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    weight = 30;
                    maxRange = 100;
                    maxAmmo = 75;
                    break;

                case "турель":
                    name = "турель";
                    pen = 5;
                    shots = new int[] { 0, 0, 10 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 7;
                    weight = 30;
                    maxRange = 100;
                    maxAmmo = 75;
                    break;

                case "огнемет":
                    name = "огнемет";
                    pen = 4;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    weight = 45;
                    maxRange = 30;
                    maxAmmo = 10;
                    qualities[5] = true;
                    qualities[6] = true;
                    wClass = 1;
                    break;

                case "бластер":
                    name = "бластер";
                    pen = 0;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 10;
                    weight = 3;
                    maxRange = 10;
                    maxAmmo = 3;
                    qualities[8] = true;
                    qualities[9] = true;
                    break;

                case "ббпп":
                    name = "ббпп";
                    pen = 5;
                    shots = new int[] { 1, 3, 7 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 1;
                    weight = 4;
                    maxRange = 50;
                    maxAmmo = 24;
                    break;

                case "игольный":
                    name = "игольный";
                    pen = 0;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 0;
                    weight = 1.5;
                    maxRange = 30;
                    maxAmmo = 6;
                    qualities[0] = true;
                    qualities[10] = true;
                    break;

                case "эзопистолет":
                    name = "эзопистолет";
                    pen = 4;
                    shots = new int[] { 1, 2, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    weight = 4;
                    maxRange = 30;
                    maxAmmo = 10;
                    qualities[7] = true;
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
        public FireWeapon(int accuracy, string type, string atype, ulong id, int aimpoint, bool aim)
        {
            hittedPartID = aimpoint;
            mode = 3;
            this.accuracy = accuracy;
            this.aim = aim;
            range = 1;
            wClass = 0;
            aType = atype;
            targetId = id;
            switch (type)
            {
                case "пистолет":
                    pen = 4;
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
                    shots = new int[] { 1, 3, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 8;
                    break;

                case "винтовка":
                    pen = 6;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    qualities[0] = true;
                    break;

                case "пулемет":
                    pen = 3;
                    shots = new int[] { 0, 0, 8 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    break;

                case "огнемет":
                    pen = 400;
                    shots = new int[] { 1, 0, 0 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 50;
                    wClass = 1;
                    break;

                case "бластер":
                    pen = 0;
                    shots = new int[] { 1, 0, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 10;
                    break;

                case "ббпп":
                    pen = 5;
                    shots = new int[] { 1, 3, 7, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 1;
                    break;

                case "игольный":
                    pen = 0;
                    shots = new int[] { 1, 0, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 0;
                    qualities[0] = true;
                    break;

                case "эзопистолет":
                    pen = 4;
                    shots = new int[] { 1, 2, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 2;
                    break;

                case "эзопистолетд":
                    pen = 6;
                    shots = new int[] { 1, 2, 0, 1 };
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    break;

                default:
                    error = true;
                    break;
            }
        }
    }
}

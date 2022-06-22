﻿namespace SkarpBot.OnlyWar
{
    partial class Melee : Weapon
    {
        private ulong targetId;

        public Melee(int accuracy, string type, string atype, ulong id)
        {
            this.accuracy = accuracy;
            aType = atype;
            targetId = id;
            switch (type)
            {
                case "кулак":
                    dmgStats[0] = 2;
                    dmgStats[1] = 0;
                    pen = 9;
                    break;

                case "лезвие":
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    pen = 5;
                    break;

                case "эзомеч":
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    pen = 5;
                    break;

                case "эзопосох":
                    dmgStats[0] = 1;
                    dmgStats[1] = 3;
                    pen = 5;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        public Melee(string type)
        {
            switch (type)
            {
                case "кулак":
                    dmgStats[0] = 2;
                    dmgStats[1] = 0;
                    pen = 9;
                    stats = new string[] { "Ближний бой", " - ", "-", "9" };
                    break;

                case "лезвие":
                    dmgStats[0] = 1;
                    dmgStats[1] = 5;
                    pen = 5;
                    stats = new string[] { "Ближний бой", " - ", "-", "3" };
                    break;

                case "эзомеч":
                    dmgStats[0] = 1;
                    dmgStats[1] = 4;
                    pen = 5;
                    stats = new string[] { "Ближний бой", " — ", "-", "5" };
                    qualities[7] = true;
                    break;

                case "эзопосох":
                    dmgStats[0] = 1;
                    dmgStats[1] = 3;
                    pen = 3;
                    stats = new string[] { "Ближний бой", " - ", "-", "2" };
                    qualities[7] = true;
                    break;

                default:
                    error = true;
                    break;
            }
        }
    }
}

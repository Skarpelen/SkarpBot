using SkarpBot.OnlyWar.Classes;

namespace SkarpBot.OnlyWar
{
    partial class Melee : Weapon
    {

        public Melee(int accuracy, string type, string atype, ulong id)
        {
            weapon = new WeaponValues(accuracy, 0, id, false);
            range = 1;
            aType = atype;
            hittedPartID = -1;
            equipment = new Equipment(type);
        }

        public Melee(string type)
        {
            equipment = new Equipment(type);
        }
    }
}

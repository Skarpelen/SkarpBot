namespace SkarpBot.OnlyWar
{
    using SkarpBot.OnlyWar.Classes;

    partial class FireWeapon : Weapon
    {
        public FireWeapon(int accuracy, int mode, string type, string aType, ulong id, bool aim)
        {
            weapon = new DamageDealer(accuracy, mode, id, aim);
            this.aType = aType;
            equipment = new Equipment(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireWeapon"/> class.
        /// Конструктор для прицельной стрельбы.
        /// </summary>
        /// <param name="accuracy">Параметр меткости.</param>
        /// <param name="type">Название оружия.</param>
        /// <param name="atype">Название брони противника.</param>
        /// <param name="id">DiscordID противника.</param>
        /// <param name="aimpoint">Айди части тела куда нужно попасть.</param>
        /// <param name="aim">Есть дополнительное прицеливание или нет.</param>
        public FireWeapon(int accuracy, string type, string atype, ulong id, int aimpoint, bool aim)
        {
            weapon = new DamageDealer(accuracy, 0, id, aim);
            weapon.HittedPartId = aimpoint;
            aType = atype;
            equipment = new Equipment(type);
        }
    }
}

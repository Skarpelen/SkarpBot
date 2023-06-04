namespace SkarpBot.OnlyWar
{
    using SkarpBot.Data;

    partial class FireWeapon : Weapon
    {
        public FireWeapon(int accuracy, int mode, string type, string armour, ulong id, bool aim)
            : base(accuracy, mode, type, armour, id, aim)
        {
            if (!error)
            {
                if (!(equipment.WeaponClass == 0 || equipment.WeaponClass == 1))
                {
                    error = true;
                }
            }
        }

        public FireWeapon(string type)
            : base(type)
        {
            if (!error)
            {
                if (!(equipment.WeaponClass == 0 || equipment.WeaponClass == 1))
                {
                    error = true;
                }
            }
        }

        public async Task<string> RegularShot(int weaponid, int targetid)
        {
            if (equipment.RoF[shotValues.Mode] == 0)
            {
                return "Оружие не может стрелять в выбранном режиме";
            }

            shotValues.CalculateShot();
            if (shotValues.Degree < 0)
            {
                await DataAccessLayer.ChangeAmmoAsync(weaponid, -equipment.RoF[shotValues.Mode]);
                return $"Выстрел{((shotValues.Mode != 0) ? "ы" : string.Empty)} прош{((shotValues.Mode != 0) ? "ли" : "ёл")} мимо цели" +
                       $"\nПотрачен{((shotValues.Mode != 0) ? "о" : string.Empty)} {equipment.RoF[shotValues.Mode]} патрон{((shotValues.Mode != 0) ? "ов" : string.Empty)}" +
                       $"\nСтепень успеха: {shotValues.Degree}";
            }

            Armour armour = new(aType);
            int result, sniper;
            string buffString;

            switch (equipment.WeaponClass)
            {
                case 0:
                    if (shotValues.Mode == 0)
                    {
                        sniper = 0;
                        if (equipment.Qualities[0] && shotValues.AdditionalAim)
                        {
                            sniper = (int)shotValues.Degree;
                        }

                        result = armour.Damage(shotValues.HittedPartId, equipment.DamageStats, equipment.Penetration, sniper);
                        await DataAccessLayer.ChangeHpAsync(targetid, shotValues.HittedPartId, -result);
                        await DataAccessLayer.ChangeAmmoAsync(weaponid, -1);

                        return $"Выстрел успешно поразил {hitPoints[shotValues.HittedPartId]}, нанеся <@{shotValues.TargetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {shotValues.Degree}";
                    }

                    result = armour.Damage(shotValues.HittedPartId, equipment.DamageStats, equipment.Penetration);
                    await DataAccessLayer.ChangeHpAsync(targetid, shotValues.HittedPartId, -result);
                    buffString = $"Выстрел успешно поразил {hitPoints[shotValues.HittedPartId]}, нанеся <@{shotValues.TargetId}> {result} урона.\n";

                    for (int i = 1; i < (int)shotValues.Degree && i < equipment.RoF[shotValues.Mode]; i++)
                    {
                        shotValues.ThrowRoll100();
                        shotValues.GetHittedPartId();
                        result = armour.Damage(shotValues.HittedPartId, equipment.DamageStats, equipment.Penetration);
                        await DataAccessLayer.ChangeHpAsync(targetid, shotValues.HittedPartId, -result);
                        buffString += $"Дополнительное попадание в {hitPoints[shotValues.HittedPartId]} нанесло {result} урона\n";
                    }

                    await DataAccessLayer.ChangeAmmoAsync(weaponid, -equipment.RoF[shotValues.Mode]);

                    buffString += $"Потрачено {equipment.RoF[shotValues.Mode]} патронов.\nСтепень успеха: {shotValues.Degree}";
                    return buffString;

                case 1:
                    result = GetFullDamage(armour, equipment.DamageStats);
                    await DataAccessLayer.ChangeFullHpAsync(targetid, result);
                    await DataAccessLayer.ChangeAmmoAsync(weaponid, -1);
                    return $"Заряд успешно поразил <@{shotValues.TargetId}>, нанеся максимально " + result + " урона";

                default:
                    return "Ошибка в классе оружия";
            }
        }

        public async Task<string> CalledShot(int weaponid, int statusid, int aimpoint)
        {
            Armour armour = new(aType);

            if (equipment.Error)
            {
                return "Неверное название оружия";
            }

            if (armour.Error)
            {
                return "Неверное название брони";
            }

            shotValues.CalculateShot();
            if (shotValues.Degree < 0)
            {
                await DataAccessLayer.ChangeAmmoAsync(weaponid, -1);
                return $"Выстрел прошёл мимо цели\nПотрачен 1 патрон\nСтепень успеха: {shotValues.Degree}";
            }

            int sniper, result;

            switch (equipment.WeaponClass)
            {
                case 0:
                    sniper = 0;
                    if (equipment.Qualities[0] || shotValues.AdditionalAim)
                    {
                        sniper = (int)shotValues.Degree;
                    }

                    result = armour.Damage(aimpoint, equipment.DamageStats, equipment.Penetration, sniper);
                    await DataAccessLayer.ChangeHpAsync(statusid, aimpoint, -result);
                    await DataAccessLayer.ChangeAmmoAsync(weaponid, -1);

                    return $"Выстрел успешно поразил {hitPoints[aimpoint]}, нанеся <@{shotValues.TargetId}> {result} урона." +
                           $"\nПотрачен 1 патрон." +
                           $"\nСтепень успеха: {shotValues.Degree}";

                case 1:
                    return "Невозможно совершить прицельный выстрел из этого оружия";

                default:
                    return "Ошибка в классе оружия";
            }
        }

        public int GetMaxAmmo()
        {
            return equipment.MaxAmmo;
        }
    }
}

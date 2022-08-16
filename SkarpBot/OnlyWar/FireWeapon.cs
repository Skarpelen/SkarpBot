namespace SkarpBot.OnlyWar
{
    using SkarpBot.Data;

    partial class FireWeapon : Weapon
    {
        public async Task<string> RegularShot(DataAccessLayer dataAccessLayer, int weaponid, int targetid)
        {
            if (equipment.RoF[weapon.Mode] == 0)
            {
                return "Оружие не может стрелять в выбранном режиме";
            }

            weapon.CalculateShot();
            if (weapon.Degree > 0)
            {
                Random random = new ();
                Armour armour = new (aType);
                int result, sniper, buf;
                string buffString;

                switch (equipment.WeaponClass)
                {
                    case 0:
                        if (weapon.Mode == 0)
                        {
                            sniper = 0;
                            if (equipment.Qualities[0] && weapon.AdditionalAim)
                            {
                                sniper = (int)weapon.Degree;
                            }

                            result = armour.Damage(weapon.HittedPartId, equipment.DamageStats, equipment.Penetration, sniper);
                            await dataAccessLayer.ChangeHp(targetid, weapon.HittedPartId, -result);
                            await dataAccessLayer.ChangeAmmo(weaponid, -1);

                            return $"Выстрел успешно поразил {hitPoints[weapon.HittedPartId]}, нанеся <@{weapon.TargetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {weapon.Degree}";
                        }

                        result = armour.Damage(weapon.HittedPartId, equipment.DamageStats, equipment.Penetration);
                        await dataAccessLayer.ChangeHp(targetid, weapon.HittedPartId, -result);
                        buffString = $"Выстрел успешно поразил {hitPoints[weapon.HittedPartId]}, нанеся <@{weapon.TargetId}> {result} урона.\n";

                        for (int i = 1; i < (int)weapon.Degree && i < equipment.RoF[weapon.Mode]; i++)
                            {
                                buf = GetHittedPartID(random.Next(100));
                                result = armour.Damage(buf, equipment.DamageStats, equipment.Penetration);
                                await dataAccessLayer.ChangeHp(targetid, buf, -result);
                                buffString += $"Дополнительное попадание в {hitPoints[buf]} нанесло {result} урона\n";
                            }

                        await dataAccessLayer.ChangeAmmo(weaponid, -equipment.RoF[weapon.Mode]);

                        buffString += $"Потрачено {equipment.RoF[weapon.Mode]} патронов.\nСтепень успеха: {weapon.Degree}";
                        return buffString;

                    case 1:
                        result = GetFullDamage(armour, equipment.DamageStats);
                        await dataAccessLayer.CangeFullHp(targetid, result);
                        await dataAccessLayer.ChangeAmmo(weaponid, -1);
                        return $"Заряд успешно поразил <@{weapon.TargetId}>, нанеся максимально " + result + " урона";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            await dataAccessLayer.ChangeAmmo(weaponid, -equipment.RoF[weapon.Mode]);
            return $"Выстрел{((weapon.Mode != 0) ? "ы" : string.Empty)} прош{((weapon.Mode != 0) ? "ли" : "ёл")} мимо цели" +
                   $"\nПотрачен{((weapon.Mode != 0) ? "о" : string.Empty)} {equipment.RoF[weapon.Mode]} патрон{((weapon.Mode != 0) ? "ов" : string.Empty)}" +
                   $"\nСтепень успеха: {weapon.Degree}";
        }

        private async Task SingleShot(DataAccessLayer dataAccessLayer, int weaponid, int targetid)
        {

        }

        public async Task<string> CalledShot(DataAccessLayer dataAccessLayer, int weaponid, int statusid)
        {
            if (GotHit())
            {
                Armour armour = new (aType);
                int sniper, result;

                if (Error)
                {
                    return "Неверное название оружия";
                }

                if (armour.Error)
                {
                    return "Неверное название брони";
                }

                switch (equipment.WeaponClass)
                {
                    case 0:
                        sniper = 0;
                        if (equipment.Qualities[0] || weapon.AdditionalAim)
                        {
                            sniper = (int)weapon.Degree;
                        }

                        result = armour.Damage(weapon.HittedPartId, equipment.DamageStats, equipment.Penetration, sniper);
                        await dataAccessLayer.ChangeHp(statusid, weapon.HittedPartId, -result);
                        await dataAccessLayer.ChangeAmmo(weaponid, -1);

                        return $"Выстрел успешно поразил {hitPoints[weapon.HittedPartId]}, нанеся <@{weapon.TargetId}> {result} урона." +
                               $"\nПотрачен 1 патрон." +
                               $"\nСтепень успеха: {weapon.Degree}";

                    case 1:
                        return "Невозможно совершить прицельный выстрел из этого оружия";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            await dataAccessLayer.ChangeAmmo(weaponid, -1);
            return $"Выстрел прошёл мимо цели\nПотрачен 1 патрон\nСтепень успеха: {weapon.Degree}";
        }

        public int GetMaxAmmo()
        {
            return equipment.MaxAmmo;
        }
    }
}

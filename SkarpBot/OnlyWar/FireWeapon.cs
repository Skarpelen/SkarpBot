namespace SkarpBot.OnlyWar
{
    using SkarpBot.Data;

    partial class FireWeapon : Weapon
    {
        public async Task<string> RegularShot(DataAccessLayer dataAccessLayer, int weaponid, int statusid)
        {
            if (GotHit())
            {
                Random random = new ();
                Armour armour = new (aType);
                int result, sniper, buf;
                string buffString;

                switch (wClass)
                {
                    case 0:
                        if (shots[mode] != 0)
                        {
                            if (mode == 0)
                            {
                                sniper = 0;
                                if (qualities[0] && aim)
                                {
                                    sniper = (int)degree;
                                }

                                result = armour.Damage(hittedPartID, dmgStats, pen, sniper);
                                await dataAccessLayer.ChangeHp(statusid, hittedPartID, -result);
                                await dataAccessLayer.ChangeAmmo(weaponid, -1);

                                return $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {degree}";
                            }

                            result = armour.Damage(hittedPartID, dmgStats, pen);
                            await dataAccessLayer.ChangeHp(statusid, hittedPartID, result);
                            buffString = $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\n";

                            for (int i = 1; i < (int)degree && i < shots[mode]; i++)
                            {
                                buf = GetHittedPartID(random.Next(100));
                                result = armour.Damage(buf, dmgStats, pen);
                                await dataAccessLayer.ChangeHp(statusid, buf, result);
                                buffString += $"Дополнительное попадание в {hitPoints[buf]} нанесло {result} урона\n";
                            }

                            await dataAccessLayer.ChangeAmmo(weaponid, -shots[mode]);

                            buffString += $"Потрачено {shots[mode]} патронов.\nСтепень успеха: {degree}";
                            return buffString;
                        }

                        return "Оружие не может стрелять в выбранном режиме";

                    case 1:
                        result = await GetFullDamage(armour, statusid, dmgStats, dataAccessLayer);
                        await dataAccessLayer.ChangeAmmo(weaponid, -1);
                        return $"Заряд успешно поразил <@{targetId}>, нанеся максимально " + result + " урона";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            await dataAccessLayer.ChangeAmmo(weaponid, -shots[mode]);
            return $"Выстрел{((mode != 0 & mode != 3) ? "ы" : string.Empty)} прош{((mode != 0 & mode != 3) ? "ли" : "ёл")} мимо цели\nПотрачен" +
                    $"{((mode != 0 & mode != 3) ? "о" : string.Empty)} {shots[mode]} патрон{((mode != 0 & mode != 3) ? "ов" : string.Empty)}\nСтепень успеха: {degree}";
        }

        public async Task<string> CalledShot(DataAccessLayer dataAccessLayer, int weaponid, int statusid)
        {
            if (GotHit())
            {
                Armour armour = new (aType);
                int sniper, result;

                if (error)
                {
                    return "Неверное название оружия";
                }

                if (armour.error)
                {
                    return "Неверное название брони";
                }

                switch (wClass)
                {
                    case 0:
                        sniper = 0;
                        if (qualities[0] || aim)
                        {
                            sniper = (int)degree;
                        }

                        result = armour.Damage(hittedPartID, dmgStats, pen, sniper);
                        await dataAccessLayer.ChangeHp(statusid, hittedPartID, result);
                        await dataAccessLayer.ChangeAmmo(weaponid, -1);

                        return $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {degree}";

                    case 1:
                        return "Невозможно совершить прицельный выстрел из этого оружия";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            await dataAccessLayer.ChangeAmmo(weaponid, -1);
            return $"Выстрел прошёл мимо цели\nПотрачен 1 патрон\nСтепень успеха: {degree}";
        }

        public int GetMaxAmmo()
        {
            return maxAmmo;
        }
    }
}

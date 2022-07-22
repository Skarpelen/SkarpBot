namespace SkarpBot.OnlyWar
{
    using SkarpBot.Data;

    partial class FireWeapon : Weapon
    {
        public async Task<string> RegularShot(DataAccessLayer dataAccessLayer)
        {
            if (GotHit())
            {
                Random random = new ();
                Armour armour = new (aType);
                int result, sniper, CalcDmg, buf;
                string buffString;

                if (error | armour.error)
                {
                    return "Неверное название оружия или брони";
                }

                switch (wClass)
                {
                    case 0:
                        if (shots[mode] != 0)
                        {
                            if (mode == 0)
                            {
                                sniper = 0;
                                if (qualities[0] || aim)
                                {
                                    sniper = (int)degree;
                                }
                                result = armour.Damage(hittedPartID, dmgStats, pen, sniper);
                                await dataAccessLayer.ChangeHp(targetId, hittedPartID, result);

                                return $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {degree}";
                            }

                            result = armour.Damage(hittedPartID, dmgStats, pen);
                            await dataAccessLayer.ChangeHp(targetId, hittedPartID, result);
                            buffString = $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\n";

                            for (int i = 1; i < (int)degree && i < shots[mode]; i++)
                            {
                                buf = GetHittedPartID(random.Next(100));
                                result = armour.Damage(buf, dmgStats, pen);
                                await dataAccessLayer.ChangeHp(targetId, buf, result);
                                buffString += $"Дополнительное попадание в {hitPoints[buf]} нанесло {result} урона\n";
                            }

                            buffString += $"Потрачено {shots[mode]} патронов.\nСтепень успеха: {degree}";
                            return buffString;
                        }

                        return "Оружие не может стрелять в выбранном режиме";

                    case 1:
                        CalcDmg = await GetFullDamage(armour, targetId, dmgStats, dataAccessLayer);
                        await dataAccessLayer.ChangeHp(targetId, 0, (int)(CalcDmg * 0.2));
                        await dataAccessLayer.ChangeHp(targetId, 1, CalcDmg);
                        await dataAccessLayer.ChangeHp(targetId, 2, (int)(CalcDmg * 0.5));
                        await dataAccessLayer.ChangeHp(targetId, 3, (int)(CalcDmg * 0.5));
                        await dataAccessLayer.ChangeHp(targetId, 4, (int)(CalcDmg * 0.4));
                        await dataAccessLayer.ChangeHp(targetId, 5, (int)(CalcDmg * 0.4));
                        return $"Заряд успешно поразил <@{targetId}>, нанеся максимально " + CalcDmg + " урона";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            return $"Выстрел{((mode != 0 & mode != 3) ? "ы" : string.Empty)} прош{((mode != 0 & mode != 3) ? "ли" : "ёл")} мимо цели\nПотрачен" +
                    $"{((mode != 0 & mode != 3) ? "о" : string.Empty)} {shots[mode]} патрон{((mode != 0 & mode != 3) ? "ов" : string.Empty)}\nСтепень успеха: {degree}";
        }

        public async Task<string> CalledShot(DataAccessLayer dataAccessLayer)
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
                        await dataAccessLayer.ChangeHp(targetId, hittedPartID, result);

                        return $"Выстрел успешно поразил {hitPoints[hittedPartID]}, нанеся <@{targetId}> {result} урона.\nПотрачен 1 патрон.\nСтепень успеха: {degree}";

                    case 1:
                        return "Невозможно совершить прицельный выстрел из этого оружия";

                    default:
                        return "Ошибка в классе оружия";
                }
            }

            return $"Выстрел прошёл мимо цели\nПотрачен 1 патрон\nСтепень успеха: {degree}";
        }

        public bool CheckMode(int mode)
        {
            if (shots[mode] == 0)
            {
                return true;
            }

            return false;
        }
    }
}

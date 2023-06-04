namespace SkarpBot.Modules
{
    using Discord;
    using Discord.Interactions;
    using SkarpBot.Data;
    using SkarpBot.OnlyWar;
    using System.Net.Sockets;

    public partial class SlashGeneral : SkarpBotInteractionsModuleBase
    {
        //[SlashCommand("тест", "тест")]
        //public async Task test()
        //{
        //    await DataAccessLayer.UserStart(Context.guild.id, Context.guild.name, Context.UserId.id, Context.UserId.Username, "легкая");
        //}

        [SlashCommand("список", "Cписок игроков на сервере")]
        [RequireUserPermission(ChannelPermission.ManageRoles)]
        public async Task UsersList()
        {
            var users = await DataAccessLayer.GetUsersAsync(Context.Guild.Id);

            var result = string.Format("{0, -25} {1, -15} {2, -10} {3, -10}\n", "Ник", "Оружие", "Патроны", "Здоровье");

            foreach (var user in users)
            {
                var currentWeapon = await DataAccessLayer.GetEquippedWeaponAsync(user);
                result += string.Format(
                    "{0, -25} {1, -15} {2, -10} {3, -10}\n",
                    user.Name,
                    currentWeapon != null ? currentWeapon.WeaponName : string.Empty,
                    currentWeapon != null ? currentWeapon.CurrentAmmo : string.Empty,
                    await DataAccessLayer.GetUserHealthAsync(user) + "%");
            }

            await RespondAsync($"```cpp\n{result}```");
        }

        [SlashCommand("хп", "Выдает хп из бд")]
        public async Task HpGet()
        {
            var users = await DataAccessLayer.GetUsersAsync(Context.Guild.Id);
            var currentUser = users.Where(user => user.UserDiscordId == Context.User.Id).FirstOrDefault();

            if (currentUser is null)
            {
                await RespondAsync("Не получилось достать пользователя");
                return;
            }

            var status = await DataAccessLayer.GetStatusAsync(currentUser);

            if (status is null)
            {
                await RespondAsync("Не получилось достать статус");
                return;
            }

            var response = await DataAccessLayer.CurrentHpAsync(status);

            await RespondAsync(response);
        }

        [SlashCommand("регистрация", "Регистрирует пользователя в системе")]
        public async Task Register(string aType)
        {
            var armour = new Armour(aType);

            if (armour.Error)
            {
                await RespondAsync("Некорректное название брони", null, false, true);
                return;
            }

            var guild = await DataAccessLayer.CreateGuildAsync(Context.Guild.Id, Context.Guild.Name);

            var user = await DataAccessLayer.CreateUserAsync(Context.User.Username, Context.User.Id, guild);

            await DataAccessLayer.UpdateStatusAsync(aType, user);

            await RespondAsync($"Пользователь {Context.User.Username} добавлен в систему с броней подтипа `{aType}`");
        }

        [SlashCommand("добавить", "Добавить в инвентарь")]
        public async Task AddToInventory(
            string name,
            [Choice("Оружие дальнего боя", 0), Choice("Оружие ближнего боя", 1), Choice("Метательное оружие", 2)] int type,
            int amount = 1)
        {
            var user = await DataAccessLayer.GetUserAsync(Context.User.Id, Context.Guild.Id);

            if (user is null)
            {
                await RespondAsync("У вас нет зарегистрированной записи");
                return;
            }

            switch (type)
            {
                case 0:
                    var weapon = new FireWeapon(name);
                    if (weapon.Error)
                    {
                        await RespondAsync("Некорректное название оружия", null, false, true);
                        return;
                    }

                    await DataAccessLayer.CreateWeaponAsync(user, name, weapon.GetMaxAmmo());
                    break;

                case 1:
                    var melee = new Melee(name);
                    if (melee.Error)
                    {
                        await RespondAsync("Некорректное название оружия", null, false, true);
                        return;
                    }

                    await DataAccessLayer.CreateWeaponAsync(user, name, 0);
                    break;

                //case 2:
                //    var grenade = new Grenade(name);
                //    if (grenade.Error)
                //    {
                //        await RespondAsync("Некорректное название гранаты", null, false, true);
                //        return;
                //    }

                //    await DataAccessLayer.AddGrenades(Context.UserId.id, Context.guild.id, name, amount);
                //    break;
            }

            await RespondAsync($"Предмет `{name}` был добавлен в инвентарь <@{Context.User.Id}>");
        }

        //[SlashCommand("инвентарь", "инвентарь игрока")]
        //public async Task ShowInventory()
        //{
        //    var equipButton = new ButtonBuilder()
        //    {
        //        Label = "Экипировать/снять",
        //        CustomId = "equip-button",
        //        Style = ButtonStyle.Success,
        //    };

        //    var destroyButton = new ButtonBuilder()
        //    {
        //        Label = "Убрать",
        //        CustomId = "destroy-button",
        //        Style = ButtonStyle.Danger,
        //    };

        //    var menu = new SelectMenuBuilder()
        //    {
        //        CustomId = "menu",
        //        Placeholder = "Sample Menu",
        //    };

        //    var weapons = DataAccessLayer.GetInventoryWeapons(Context.UserId.id, Context.guild.id);
        //    var grenades = DataAccessLayer.GetInventoryGrenades(Context.UserId.id, Context.guild.id);
        //    var briefcaseEmoji = new Emoji("\U0001F4BC");
        //    var handEmoji = new Emoji("\U0001F590");
        //    foreach (var weapon in weapons)
        //    {
        //        menu.AddOption(
        //            FirstLetterToUpper(weapon.WeaponName),
        //            $"{weapon.WeaponId}.W",
        //            $"Количество оставшихся снарядов в магазине: {weapon.CurrentAmmo}",
        //            weapon.InventoryName == "equipped" ? handEmoji : briefcaseEmoji);
        //    }

        //    foreach (var grenade in grenades)
        //    {
        //        menu.AddOption(
        //            FirstLetterToUpper(grenade.GrenadeName),
        //            $"{grenade.GrenadeId}.G",
        //            $"Количество гранат в инвентаре: {grenade.Amount}",
        //            briefcaseEmoji);
        //    }

        //    var component = new ComponentBuilder();

        //    component.WithSelectMenu(menu);
        //    component.WithButton(equipButton);
        //    component.WithButton(destroyButton);

        //    await RespondAsync("Пропиши `/добавить` для пополнения инвентаря", null, false, true, components: component.Build());
        //}

        //[SlashCommand("перезарядка", "Перезарядить экипированное оружие")]
        //public async Task Reload()
        //{
        //    var weaponname = DataAccessLayer.GetEquippedWeapon(Context.UserId.id, Context.guild.id);
        //    var weapon = new FireWeapon(-1, 0, weaponname.WeaponName, string.Empty, 0, false);

        //    if (weapon.Error)
        //    {
        //        await RespondAsync("Невозможно перезарядить это оружие", null, false, true);
        //        return;
        //    }

        //    string result = await DataAccessLayer.ReloadWeapon(weaponname.WeaponId, weapon.GetMaxAmmo());
        //    await RespondAsync(result);
        //}

        [SlashCommand("атаковать", "Атаковать экипированным оружием")]
        public async Task StandartAttack(
            int accuracy,
            [Choice("Стандартный режим/ближний бой", 0), Choice("Короткая очередь", 1), Choice("Длинная очередь", 2)] int mode,
            IUser targetUser,
            [Choice("Дополнительное прицеливание", 1), Choice("Простая стрельба", 0)] int isAim = 0)
        {
            if (accuracy < 0)
            {
                await RespondAsync("Неправильное значение режима меткости", null, false, true);
                return;
            }

            var user = await DataAccessLayer.GetUserAsync(Context.User.Id, Context.Guild.Id);

            if (user is null)
            {
                await RespondAsync("Пользователь не найден");
                return;
            }

            var status = await DataAccessLayer.GetStatusAsync(user);

            if (status is null)
            {
                await RespondAsync("Не найден статус пользователя");
                return;
            }

            var equippedWeapon = await DataAccessLayer.GetEquippedWeaponAsync(user);

            if (equippedWeapon is null)
            {
                await RespondAsync("Ты еще не экипировал никакого оружия");
                return;
            }

            var meleeCheck = new Melee(equippedWeapon.WeaponName);

            string result;

            if (meleeCheck.Error)
            {
                if (equippedWeapon.CurrentAmmo < 1)
                {
                    await RespondAsync("Клик-клик... оружие не стреляет");
                    return;
                }

                var gunFire = new FireWeapon(accuracy, mode, equippedWeapon.WeaponName, status.Armour, targetUser.Id, isAim == 1);
                result = await gunFire.RegularShot(equippedWeapon.WeaponId, status.Id);
            }
            else
            {
                var melee = new Melee(accuracy, 0, equippedWeapon.WeaponName, status.Armour, targetUser.Id, false);
                result = await melee.Swing(status.Id);
            }

            await RespondAsync(result);
        }

        //[SlashCommand("метко", "Выстрелить в определенную часть тела")]
        //public async Task CalledShot(
        //    int accuracy,
        //    IUser targetUser,
        //    [Choice("Голова", 0), Choice("Тело", 1), Choice("Левая рука", 2), Choice("Правая рука", 3), Choice("Левая нога", 4), Choice("Правая нога", 5)] int aimpoint,
        //    [Choice("Дополнительное прицеливание", 1), Choice("Простая стрельба", 0)] int isAim = 0)
        //{
        //    if (accuracy < 0)
        //    {
        //        await RespondAsync("Неправильное значение режима меткости", null, false, true);
        //        return;
        //    }

        //    var equippedweapon = DataAccessLayer.GetEquippedWeapon(Context.UserId.id, Context.guild.id);
        //    var armourname = DataAccessLayer.GetStatus(targetUser.id, Context.guild.id);
        //    var meleeCheck = new Melee(equippedweapon.WeaponName);

        //    string result;

        //    if (!meleeCheck.Error)
        //    {
        //        await RespondAsync("Невозможно на данный момент сделать это оружием ближнего боя", null, false, true);
        //        return;
        //    }

        //    if (equippedweapon.CurrentAmmo < 1)
        //    {
        //        await RespondAsync("Клик-клик... оружие не стреляет");
        //        return;
        //    }

        //    var gunFire = new FireWeapon(accuracy, 0, equippedweapon.WeaponName, armourname.Armour, targetUser.id, isAim == 1 ? true : false);
        //    result = await gunFire.CalledShot(DataAccessLayer, equippedweapon.WeaponId, armourname.id, aimpoint);

        //    await RespondAsync(result);
        //}

        /// <summary>
        /// Проверка характеристик.
        /// </summary>
        /// <param name="wType">Оружие для проверки.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("статы", "Статы выбранного оружия")]
        public async Task Stats(string wType)
        {
            var weapon = new Weapon(wType);

            if (weapon.Error)
            {
                await RespondAsync("Ошибка в названии оружия", null, false, true);
                return;
            }

            await RespondAsync(embed: weapon.GetWeaponInfo());
        }

        ///// <summary>
        ///// Бросок гранаты в обычном режиме.
        ///// </summary>
        ///// <param name="accuracy">Коэфициент меткости.</param>
        ///// <param name="wType">Гранта стреляющего.</param>
        ///// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[SlashCommand("бросить", "Бросает")]
        //public async Task GrenadeWeapon(int accuracy, string wType)
        //{
        //    if (accuracy < 0)
        //    {
        //        await RespondAsync("Неправильное значение меткости", null, false, true);
        //        return;
        //    }

        //    var grenadeThrow = new Grenade(accuracy, 1, wType, string.Empty, 1, false);
        //    if (grenadeThrow.Error)
        //    {
        //        await RespondAsync("Такой гранаты не существует", null, false, true);
        //        return;
        //    }

        //    var grenadesList = DataAccessLayer.GetInventoryGrenades(Context.UserId.id, Context.guild.id);
        //    var userGrenade = grenadesList.Find(x => x.GrenadeName == wType);
        //    if (userGrenade != null)
        //    {
        //        await DataAccessLayer.DestroyGrenade(userGrenade.GrenadeId);
        //        await RespondAsync(grenadeThrow.GrenadeThrow());
        //        return;
        //    }

        //    await RespondAsync("Такой гранаты в инвентаре нет", null, false, true);
        //}

        /// <summary>
        /// Информация об имеющихся вариантах оружия.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SlashCommand("инфо", "Как пользоваться ботом")]
        public async Task Info()
        {
            await OWInfo();
        }

        [SlashCommand("механики", "Информация о доступных механиках боя")]
        public async Task Mech()
        {
            await OWMech();
        }

        private async Task OWInfo()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Как использовать бота")
                .AddField("Стрельба", "`-стрелять {меткость} {режим} {оружие} {слап цели}` - стандартная стрельба\n`-стрелять {меткость} {оружие} {желаемое место попадания} {слап цели}` - прицельная стрельба\n`-стрелять {оружие}` - получить информацию об оружии\n\nДля увеличения меткости в конце команд для стрельбы можно выставить \"0\"\n")
                .AddField("Гранаты", "`-бросать {меткость} {граната}` - бросок гранаты\n`-бросать {граната}` - получить информацию о гранате\n")
                .AddField("Ближний бой", "`-резать {меткость} {оружие} {слап цели}` - атака оружием ближнего боя\n`-резать {оружие}` - получить информацию об оружии\n")
                .AddField("Взаимодействия с хит поинтами", "`-хп` - показывает ваши текущие хп\n`-установить {номер части тела} {значение}` - прибавляет к указанной части тела то количество хп, которое указано в значении\n")
                .AddField("Дополнительные команды", "`-инфо` - вызывает эту таблицу\n`-механ` - показывает дополнительные механики боя\n\n")
                .AddField("Стандартное оружие", "Пистолет\nАвтомат\nДробовик\nПулемёт\nВинтовка\nОгнемёт", true)
                .AddField("Гранаты", "Осколочная\nЗажигательная\nСветошумовая\nГазовая\nТактическая\nДымовая", true)
                .Build();

            await RespondAsync(embed: embed);
        }

        private async Task OWMech()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Дополнительные механики боя")
                .AddField("Огонь на подавление", "Персонаж должен использовать дистанционное оружие, способное вести огонь короткими или длинными очередями.Заявив огонь на " + "подавление, персонаж выбирает область, которую он будет подавлять огнѐм. Затем персонаж производит выстрел выбранной очередью." + "Дополнительное попадание в таком случае высчитывается за каждые 2 ступени успеха, а не за 1. Цели, попавшие под обстрел и не " + "прошедшие тест, получают -20 к меткости", true)
                .AddField("Осторожная атака", "Персонаж действует осторожно и продуманно, в любой миг готовясь уйти в оборону. В этом ходу он получает штраф -10 на тесты Ближнего или Дальнего боя, однако противники в следующем ходу так же будут получать -10 к своим тестам направленным против персонажа.", true)
                .Build();

            await RespondAsync(embed: embed);
        }
    }
}

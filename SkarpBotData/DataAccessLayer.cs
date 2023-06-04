using Newtonsoft.Json;
using SkarpBot.Data.Context;
using SkarpBot.Data.Models;
using System.Text;

namespace SkarpBot.Data
{
    public partial class DataAccessLayer
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static readonly string localurl = "http://127.0.0.1:8000/api/";

        private static readonly int[] _maxHp = new int[] { 12, 28, 17, 20 };

        public DataAccessLayer(SkarpBotDbContext dbContext)
        {
        }

        /// <summary>
        /// Ищет <see cref="Guild"/> по его айди
        /// </summary>
        /// <param name="serverId">Дискорд айди сервера</param>
        /// <returns>Найденный <see cref="Guild"/> или же null</returns>
        public static async Task<Guild> GetGuildAsync(ulong serverId)
        {
            var guilds = await GetGuildsAsync();

            return guilds.FirstOrDefault(guild => guild.ServerId == serverId);
        }

        /// <summary>
        /// Геттер серверов из БД
        /// </summary>
        /// <returns>Список серверов</returns>
        public static async Task<List<Guild>> GetGuildsAsync()
        {
            var response = await httpClient.GetAsync($"{localurl}guilds/");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetGuildsAsync Error: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var guilds = JsonConvert.DeserializeObject<List<Guild>>(responseBody);

            return guilds; 
        }

        public static async Task<User> GetUserAsync(ulong userId, ulong serverId)
        {
            var users = await GetUsersAsync(serverId);

            return users.FirstOrDefault(user => user.UserDiscordId == userId);
        }

        /// <summary>
        /// Ищет <see cref="User"/> с указанным <see cref="Status"/>
        /// </summary>
        /// <param name="status">Статус</param>
        /// <returns>Найденный статус или null</returns>
        public static async Task<User> GetUserAsync(Status status)
        {
            var users = await GetUsersAsync();

            return users.FirstOrDefault(user => user.Id == status.UserId);
        }

        /// <summary>
        /// Геттер пользователей на выбранном сервере
        /// </summary>
        /// <param name="serverId">Дискорд айди сервера</param>
        /// <returns>Отфильтрованный по серверу список пользователей</returns>
        public static async Task<List<User>> GetUsersAsync(ulong serverId)
        {
            var guild = await GetGuildAsync(serverId);

            if (guild is null)
            {
                return null;
            }

            var users = await GetUsersAsync();

            return users.Where(user => user.GuildId == guild.Id).ToList();
        }

        /// <summary>
        /// Геттер пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public static async Task<List<User>> GetUsersAsync()
        {
            var response = await httpClient.GetAsync($"{localurl}users/");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetUsersAsync Error: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(responseBody);

            return users;
        }

        /// <summary>
        /// Геттер статуса по его айди в БД
        /// </summary>
        /// <param name="statusId">Айди статуса</param>
        /// <returns>Найденный статус</returns>
        public static async Task<Status> GetStatusAsync(int statusId)
        {
            var statuses = await GetStatusesAsync();

            return statuses.FirstOrDefault(statuses => statuses.Id == statusId);
        }

        /// <summary>
        /// Геттер статуса пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Статус пользователя</returns>
        public static async Task<Status> GetStatusAsync(User user)
        {
            var statuses = await GetStatusesAsync();

            return statuses.FirstOrDefault(status => status.UserId == user.Id);
        }

        /// <summary>
        /// Геттер статусов
        /// </summary>
        /// <returns>Список статусов</returns>
        public static async Task<List<Status>> GetStatusesAsync()
        {
            var response = await httpClient.GetAsync($"{localurl}statuses/");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetStatusesAsync Error: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var status = JsonConvert.DeserializeObject<List<Status>>(responseBody);

            return status;
        }

        /// <summary>
        /// Геттер экипированного оружия пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Оружие в руках пользователя</returns>
        public static async Task<Weapon> GetEquippedWeaponAsync(User user)
        {
            var userWeapons = await GetWeaponsAsync(user);

            return userWeapons?.FirstOrDefault(weapon => weapon.InventoryName == "equipped");
        }

        /// <summary>
        /// Геттер оружия с определенным айди
        /// </summary>
        /// <param name="weaponId">Айди оружия в БД</param>
        /// <returns>Найденное оружие</returns>
        public static async Task<Weapon> GetWeaponAsync(int weaponId)
        {
            var weapons = await GetWeaponsAsync();

            return weapons.FirstOrDefault(weapon => weapon.WeaponId == weaponId);
        }

        /// <summary>
        /// Геттер оружия у выбранного пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Оружие пользователя</returns>
        public static async Task<List<Weapon>> GetWeaponsAsync(User user)
        {
            var weapons = await GetWeaponsAsync();

            return weapons?.Where(weapon => weapon.UserId == user.Id).ToList();
        }

        /// <summary>
        /// Геттер оружия
        /// </summary>
        /// <returns>Список оружия</returns>
        public static async Task<List<Weapon>> GetWeaponsAsync()
        {
            var response = await httpClient.GetAsync($"{localurl}weapons/");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetWeaponsAsync Error: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<Weapon>>(responseBody);

            return weapons;
        }

        /// <summary>
        /// Геттер гранат
        /// </summary>
        /// <returns>Список гранат</returns>
        public static async Task<List<Grenade>> GetGrenadesAsync()
        {
            var response = await httpClient.GetAsync($"{localurl}grenades/");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetGrenadesAsync Error: {response.StatusCode}");
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var grenades = JsonConvert.DeserializeObject<List<Grenade>>(responseBody);

            return grenades;
        }

        /// <summary>
        /// Отправляет запрос на создание новой строки <see cref="Guild"/>
        /// </summary>
        /// <param name="guildId">Дискорд айди сервера</param>
        /// <param name="guildName">Название сервера</param>
        /// <returns>Копия созданного <see cref="Guild"/></returns>
        public static async Task<Guild> CreateGuildAsync(ulong guildId, string guildName)
        {
            var guilds = await GetGuildsAsync();
            var guild = guilds.FirstOrDefault(guild => guild.ServerId ==  guildId);

            if (guild is not null)
            {
                return guild;
            }

            var guildData = new Guild
            {
                ServerId = guildId,
                ServerName = guildName,
                Prefix = "?"
            };

            var json = JsonConvert.SerializeObject(guildData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{localurl}guilds/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"CreateGuildAsync Error: {response.StatusCode}");
            }

            return guildData;
        }

        /// <summary>
        /// Отправляет запрос на создание новой строки <see cref="User"/>
        /// </summary>
        /// <param name="username">Ник в дискорде</param>
        /// <param name="id">Дискорд айди пользователя</param>
        /// <param name="guild"><see cref="Guild"/> сервера</param>
        /// <returns>Копия созданного <see cref="User"/></returns>
        public static async Task<User> CreateUserAsync(string username, ulong id, Guild guild)
        {
            var users = await GetUsersAsync();
            var user = users.FirstOrDefault(user => user.UserDiscordId == id && user.GuildId == guild.Id);

            if (user is not null)
            {
                return user;
            }

            var userData = new User
            {
                Name = username,
                UserDiscordId = id,
                GuildId = guild.Id
            };

            var json = JsonConvert.SerializeObject(userData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{localurl}users/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"CreateUserAsync {username} Error: {response.StatusCode}");
            }

            return userData;
        }

        /// <summary>
        /// Отправляет запрос на создание новой строки <see cref="Status"/>
        /// </summary>
        /// <param name="aType">Тип брони</param>
        /// <param name="user">Пользователь</param>
        /// <returns>Копия созданного <see cref="Status"/></returns>
        public static async Task<Status> CreateStatusAsync(string aType, User user)
        {
            var status = await GetStatusAsync(user);

            if (status is not null)
            {
                return status;
            }

            var statusData = new Status
            {
                Armour = aType,
                Head = _maxHp[0],
                Body = _maxHp[1],
                LHand = _maxHp[2],
                RHand = _maxHp[2],
                LFoot = _maxHp[3],
                RFoot = _maxHp[3],
                UserId = user.Id
            };

            var json = JsonConvert.SerializeObject(statusData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{localurl}statuses/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"CreateStatusAsync {user.Name} Error: {response.StatusCode}");
            }

            return statusData;
        }

        public static async Task<Weapon> CreateWeaponAsync(User user, string weaponName, int ammo)
        {
            var weaponData = new Weapon
            {
                WeaponName = weaponName,
                CurrentAmmo = ammo,
                InventoryName = "unequipped",
                UserId = user.Id
            };

            var json = JsonConvert.SerializeObject(weaponData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{localurl}weapons/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"CreateWeaponAsync {user.Name} Error: {response.StatusCode}");
            }

            return weaponData;
        }

        /// <summary>
        /// Изменяет запись в бд
        /// </summary>
        /// <param name="aType">Тип брони</param>
        /// <param name="user">Пользователь</param>
        /// <returns>Измененная версия <see cref="Status"/></returns>
        public static async Task<Status> UpdateStatusAsync(string aType, User user)
        {
            var status = await GetStatusAsync(user);

            if (status is null)
            {
                status = await CreateStatusAsync(aType, user);
                return status;
            }

            StatusRefiller(ref status, aType);

            var json = JsonConvert.SerializeObject(status);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"{localurl}statuses/{user.Id - 1}/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"UpdateStatusAsync {user.Name} Error: {response.StatusCode}");
            }

            return status;
        }

        /// <summary>
        /// Геттер префикса сервера
        /// </summary>
        /// <param name="id">Айди сервера</param>
        /// <returns>Префикс</returns>
        public static async Task<string> GetPrefixAsync(ulong id)
        {
            var guilds = await GetGuildsAsync();

            return guilds.Where(guild => guild.ServerId == id).First().Prefix;
        }

        /// <summary>
        /// Считает общий процент здоровости от количества хп каждой части тела
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Общий процент хп</returns>
        public static async Task<double> GetUserHealthAsync(User user)
        {
            var status = await GetStatusAsync(user);

            double sum = status.Head / (double)_maxHp[0] * 100.0
                       + status.Body / (double)_maxHp[1] * 100.0
                       + status.LHand / (double)_maxHp[2] * 100.0
                       + status.RHand / (double)_maxHp[2] * 100.0
                       + status.LFoot / (double)_maxHp[3] * 100.0
                       + status.RFoot / (double)_maxHp[3] * 100.0;

            return Math.Round(sum / 6);
        }

        /// <summary>
        /// Подробное количество хп пользователя
        /// </summary>
        /// <param name="status">Пользователь</param>
        /// <returns>Строка с хп</returns>
        public static async Task<string> CurrentHpAsync(Status status)
        {
            var user = await GetUserAsync(status);

            string result = $"Текущее состояние <@{user.UserDiscordId}>\n" +
                $"\n**Голова: **{status.Head / (double)_maxHp[0] * 100.0}%" +
                $"\n**Тело: **{status.Body / (double)_maxHp[1] * 100.0}%" +
                $"\n**Левая рука: **{status.LHand / (double)_maxHp[2] * 100.0}%" +
                $"\n**Правая рука: **{status.RHand / (double)_maxHp[2] * 100.0}%" +
                $"\n**Левая нога: **{status.LFoot / (double)_maxHp[3] * 100.0}%" +
                $"\n**Правая нога: **{status.RFoot / (double)_maxHp[3] * 100.0}%";

            return result;
        }


        public static async Task ChangeHpAsync(int statusId, int point, int value)
        {
            var status = await GetStatusAsync(statusId);

            switch (point)
            {
                case 0:
                    status.Head += value;
                    if (status.Head < 0)
                        {
                        status.Head = 0;
                    }
                    break;

                case 1:
                    status.Body += value;
                    if (status.Body < 0)
                        {
                        status.Head = 0;
                    }
                    break;

                case 2:
                    status.LHand += value;
                    if (status.LHand < 0)
                        {
                        status.Head = 0;
                    }
                    break;

                case 3:
                    status.RHand += value;
                    if (status.RHand < 0)
                        {
                        status.Head = 0;
                    }
                    break;

                case 4:
                    status.LFoot += value;
                    if (status.LFoot < 0)
                        {
                        status.Head = 0;
                    }
                    break;

                case 5:
                    status.RFoot += value;
                    if (status.RFoot < 0)
                        {
                        status.Head = 0;
                    }
                    break;
            }

            var json = JsonConvert.SerializeObject(status);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"{localurl}statuses/{status.Id}/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ChangeAmmoAsync UserId = {status.UserId} Error: {response.StatusCode}");
            }
        }

        public static async Task ChangeFullHpAsync(int statusId, int value)
        {
            var status = await GetStatusAsync(statusId);

            status.Head += (int)(value * 0.1);
            if (status.Head < 0)
            {
                status.Head = 0;
            }

            status.Body += value;
            if (status.Body < 0)
                {
                status.Head = 0;
            }

            status.LHand += (int)(value * 0.3);
            if (status.LHand < 0)
            {
                status.Head = 0;
            }

            status.RHand += (int)(value * 0.3);
            if (status.RHand < 0)
            {
                status.Head = 0;
            }

            status.LFoot += (int)(value * 0.3);
            if (status.LFoot < 0)
            {
                status.Head = 0;
            }

            status.RFoot += (int)(value * 0.3);
            if (status.RFoot < 0)
            {
                status.Head = 0;
            }

            var json = JsonConvert.SerializeObject(status);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"{localurl}statuses/{status.Id}/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ChangeAmmoAsync UserId = {status.UserId} Error: {response.StatusCode}");
            }
        }

        public static async Task ChangeAmmoAsync(int weaponId, int value)
        {
            var weapon = await GetWeaponAsync(weaponId);

            weapon.CurrentAmmo += value;

            if (weapon.CurrentAmmo < 0)
            {
                weapon.CurrentAmmo = 0;
            }

            var json = JsonConvert.SerializeObject(weapon);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"{localurl}weapons/{weaponId}/", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"ChangeAmmoAsync UserId = {weapon.UserId} Error: {response.StatusCode}");
            }
        }

        /// <summary>
        /// Заполняет <see cref="Status"/>
        /// </summary>
        /// <param name="status">Заполняемый статус</param>
        /// <param name="aType">Тип брони</param>
        private static void StatusRefiller(ref Status status, string aType)
        {
            status.Armour = aType;
            status.Head = _maxHp[0];
            status.Body = _maxHp[1];
            status.LHand = _maxHp[2];
            status.RHand = _maxHp[2];
            status.LFoot = _maxHp[3];
            status.RFoot = _maxHp[3];
        }
    }
}

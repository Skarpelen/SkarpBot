using Discord;

namespace SkarpBot.OnlyWar.Classes
{
    public class Equipment
    {
        private string name;
        private int penetration;
        private int[] damageStats = new int[2];
        private int[] rof = new int[3];
        private double weight;
        private int maxRange;
        private int maxAmmo;
        private bool[] qualities = new bool[11];
        private int weaponClass = 0;
        private bool error = false;

        public Equipment(string name)
        {
            this.name = name;
            switch (name)
            {
                case "пистолет":
                    penetration = 4;
                    rof = new int[] { 1, 2, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = -2;
                    weight = 1.5;
                    maxRange = 30;
                    maxAmmo = 9;
                    break;

                case "тазер":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 0;
                    damageStats[1] = 0;
                    weight = 0.2;
                    maxRange = 30;
                    maxAmmo = 1;
                    qualities[2] = true;
                    break;

                case "автомат":
                    penetration = 2;
                    rof = new int[] { 1, 3, 10 };
                    damageStats[0] = 1;
                    damageStats[1] = 2;
                    weight = 5;
                    maxRange = 100;
                    maxAmmo = 30;
                    break;

                case "дробовик":
                    penetration = 0;
                    rof = new int[] { 1, 3, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 8;
                    weight = 6.5;
                    maxRange = 30;
                    maxAmmo = 18;
                    qualities[1] = true;
                    break;

                case "винтовка":
                    penetration = 6;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 5;
                    weight = 5;
                    maxRange = 200;
                    maxAmmo = 20;
                    qualities[0] = true;
                    break;

                case "пулемет":
                    penetration = 3;
                    rof = new int[] { 0, 0, 8 };
                    damageStats[0] = 1;
                    damageStats[1] = 4;
                    weight = 30;
                    maxRange = 100;
                    maxAmmo = 75;
                    break;

                case "огнемет":
                    penetration = 4;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 5;
                    weight = 45;
                    maxRange = 30;
                    maxAmmo = 10;
                    qualities[5] = true;
                    qualities[6] = true;
                    weaponClass = 1;
                    break;

                case "бластер":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 10;
                    weight = 3;
                    maxRange = 10;
                    maxAmmo = 3;
                    qualities[8] = true;
                    qualities[9] = true;
                    break;

                case "ббпп":
                    penetration = 5;
                    rof = new int[] { 1, 3, 7 };
                    damageStats[0] = 1;
                    damageStats[1] = 1;
                    weight = 4;
                    maxRange = 50;
                    maxAmmo = 24;
                    break;

                case "игольный":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 0;
                    weight = 1.5;
                    maxRange = 30;
                    maxAmmo = 6;
                    qualities[0] = true;
                    qualities[10] = true;
                    break;

                case "эзопистолет":
                    penetration = 4;
                    rof = new int[] { 1, 2, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 2;
                    weight = 4;
                    maxRange = 30;
                    maxAmmo = 10;
                    qualities[7] = true;
                    break;

                //////////////////////////

                case "реактивнаяперчатка":
                    penetration = 9;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 2;
                    damageStats[1] = 0;
                    weight = 9;
                    maxRange = 5;
                    maxAmmo = 0;
                    break;

                case "рука":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = -2;
                    weight = 0;
                    maxRange = 5;
                    maxAmmo = 0;
                    break;

                case "лезвие":
                    penetration = 5;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 5;
                    weight = 3;
                    maxRange = 5;
                    maxAmmo = 0;
                    break;

                case "эзомеч":
                    penetration = 5;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 4;
                    weight = 5;
                    maxRange = 5;
                    maxAmmo = 0;
                    qualities[7] = true;
                    break;

                case "эзопосох":
                    penetration = 3;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 3;
                    weight = 2;
                    maxRange = 5;
                    maxAmmo = 0;
                    qualities[7] = true;
                    break;

                //////////////////////////

                case "осколочная":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 2;
                    damageStats[1] = 0;
                    weight = 0.5;
                    maxRange = 15;
                    maxAmmo = 0;
                    qualities[3] = true;
                    weaponClass = 2;
                    break;

                case "зажигательная":
                    penetration = 6;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 1;
                    damageStats[1] = 3;
                    weight = 0.5;
                    maxRange = 15;
                    maxAmmo = 0;
                    qualities[3] = true;
                    qualities[5] = true;
                    weaponClass = 2;
                    break;

                case "дымовая":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 0;
                    damageStats[1] = 0;
                    weight = 0.5;
                    maxRange = 25;
                    maxAmmo = 0;
                    qualities[4] = true;
                    weaponClass = 3;
                    break;

                case "светошумовая":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 0;
                    damageStats[1] = 0;
                    weight = 0.5;
                    maxRange = 15;
                    maxAmmo = 0;
                    qualities[4] = true;
                    weaponClass = 3;
                    break;

                case "тактическая":
                    penetration = 0;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 0;
                    damageStats[1] = 0;
                    weight = 0.5;
                    maxRange = 15;
                    maxAmmo = 0;
                    qualities[5] = true;
                    weaponClass = 3;
                    break;

                case "газовая":
                    penetration = 10;
                    rof = new int[] { 1, 0, 0 };
                    damageStats[0] = 2;
                    damageStats[1] = 10;
                    weight = 0.5;
                    maxRange = 15;
                    maxAmmo = 0;
                    qualities[3] = true;
                    weaponClass = 3;
                    break;

                default:
                    error = true;
                    break;
            }
        }

        public Embed FormEquipmentInfo()
        {
            var embed = new EmbedBuilder()
                .WithTitle(name)
                .AddField("Бронебойность", penetration, true)
                .AddField("Режимы стрельбы", $"{rof[0]}, {rof[1]}, {rof[2]}", true)
                .AddField("Урон", $"{damageStats[0]}к{damageStats[1]}", true)
                .AddField("Оптимальная дальность", maxRange, true)
                .AddField("Объем магазина", maxAmmo, true)
                .AddField("Вес", weight, true)
                .AddField("Особенности", GetQualities())
                .Build();

            return embed;
        }

        private string GetQualities()
        {
            string[] qualitiesList = QualitiesList();
            string result = "";
            for (int i = 0; i < qualities.Length; i++)
            {
                if (qualities[i])
                {
                    result += qualitiesList[i] + "\n";
                }
            }

            return result;
        }

        private string[] QualitiesList()
        {
            string[] qualities = new string[]
            {
                "```1.Точное.\nЭто оружие предназначено для точного поражения отдельных " +
                "целей и бесподобно в умелых руках. При прицеливании стрелок получает дополнительный бонус +10 к " +
                "меткости (этот бонус складывается с тем, что дает само прицеливание). Прицелившись и выстрелив " +
                "в одиночном режиме из основного оружия, имеющего эту особенность, стрелок наносит дополнительно " +
                "1к10 единиц урона за каждые две ступени успеха до максимума в 2к10.```",

                "```2.Разброс.\nСтандартные боеприпасы для этого оружия содержат дробь или иные подобные поражающие элементы. " +
                "При стрельбе в упор, такое оружие получает бонус +10 к тесту на попадание и наносит +3 единицы урона. На " +
                "короткой дистанции оно получает только +10 к тесту на попадание. При стрельбе на более дальнюю дистанцию, " +
                "урон оружия уменьшается на 3.```",

                "```3.Оглушающее.\nВыстрел из этого оружия порождает ударную волну или громкий звук. Цель, получившая попадание из оружия с такой " +
                "особенностью,оглушается на один раунд за каждую ступень провала. Если цель получает количество урона, большее, чем " +
                "еѐ бонус Силы, еѐ сбивает с ног.```",

                $"```4.Взрыв({5}).\nПри расчете попадания из оружия с этой особенностью, все, находящиеся в указанном в скобках количестве " +
                "метров от места, куда пришлось попадание, считаются задетыми взрывом. Все, задетые взрывом, получают одинаковое " +
                "количество урона```",

                $"```5.Дымовое({5}).\nЭто оружие не столько наносит урон, сколько заволакивает всѐ вокруг клубами дыма, позволяя прятаться" +
                " в нем от вражеских глаз. Попадание из оружия с особенностью «Дымовое» создает в месте попадания облако дыма, диаметр" +
                " которого равен числу в скобках.```",

                "```6.Огненное.\nНекоторое оружие при стрельбе изрыгает огромную струю пламени, поджигающую всѐ, во что попадет. Цель, " +
                "получившая попадание из огненного оружия (даже если она не получила урона), должна преуспеть в тесте Ловкости или " +
                "загореться.```",

                "```7.Распыляющее.\nНекоторые виды оружия при стрельбе выбрасывают перед собой конус пламени, жидкости или других поражающих" +
                " элементов. В отличие от других видов оружия, у распыляющего есть только одна дистанция стрельбы. Стрелку не требуется бросать" +
                " тест Дальнего боя – он просто жмет на гашетку. Зона поражения такого оружия – конус с углом в 30 градусов и длиной, равной " +
                "дистанции стрельбы. Все, находящиеся в этой зоне в момент выстрела, должны пройти средний (+0) тест Ловкости или получить обычный" +
                " для этого оружия урон. Укрытия не спасает персонажей от атак распыляющим оружием (если только оно не скрывает персонажа" +
                " полностью). Поскольку при стрельбе из распыляющего оружия не бросается тест Дальнего боя, считается, что любое попадание из " +
                "него приходится в торс цели.```",

                "```8.Эзотерическое.\nЭзотерическое оружие уникально тем, что работает в полную силу лишь в руках эзотерика, чья магия наполняет, " +
                "казалось бы, обычный клинок устрашающей мощью. В руках обычного человека оружие ощущается тяжелым и неэффективным в бою. В руках эзотерика " +
                "оружие получает + к урону и бронебойности, в зависимости от рейтинга силы владельца. Кроме того, всякий раз, когда эзотерик наносит урон противнику, " +
                "он может предпринять действие «Сотворение психосилы». В случае успеха этого теста, владелец эзотерического оружия наносит врагу дополнительно 1к10 единиц " +
                "энергетического урона```",

                "```9.Мельта.\nИспользующее высокотемпературные потоки газа, под давлением переходящего в нестабильное состояние, это оружие с близкого расстояния может расплавить даже " +
                "самые прочные материалы```",

                "```10.Перегревающееся.\nОружие с этой особенностью перегревается, если при броске на попадание выпадает 91 или больше. При этом стрелок получает количество энергетического урона, " +
                "равное обычному урону оружия с бронебойностью 0. Этот урон приходится в руку (в ту, которой персонаж держит оружие, если оно одноручное; в выбранную случайно, " +
                "если персонаж держал его двумя руками). Стрелок может избежать получения урона, бросив своѐ оружие в качестве свободного действия.На остывание оружие тратится " +
                "один раунд; перегревшееся оружие не может вести огонь до того как начнется второй раунд после перегрева.```",

                $"```11.Токсичное({5}).\nЛюбой, получивший урон от токсичного оружия (после вычета очков брони) обязан пройти тест Выносливости со штрафом, равным 10 * {5}, или получить 1к10 единиц урона того же типа, что был у оружия```",
            };

            return qualities;
        }

        public string Name
        {
            get { return name; }
        }

        public int Penetration
        {
            get { return penetration; }
        }

        public int[] DamageStats
        {
            get { return damageStats; }
        }

        public int[] RoF
        {
            get { return rof; }
        }

        public int MaxAmmo
        {
            get { return maxAmmo; }
        }

        public bool[] Qualities
        {
            get { return qualities; }
        }

        public int WeaponClass
        {
            get { return weaponClass; }
        }

        public bool Error
        {
            get { return error; }
        }
    }
}

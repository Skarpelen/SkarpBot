namespace SkarpBot.OnlyWar
{
    public class Weapon
    {
        protected int accuracy;
        protected int aim;
        protected int range;
        protected int rate;
        protected int mode;
        protected int value;
        protected int hitValue;
        protected int hitIndex;
        private string aType;
        protected int[,] buffs = new int[,] { { 10, 0, 20, 0 }, { 10, 0, -10, -20 }, { 20, 10, 0, -20 } };

        protected bool[] qualities = new bool[7];
        protected int pen;
        protected int dmgInd;
        protected int x;
        protected string[]? stats;
        public bool error = false;
        public double degree;

        protected string AType { get => aType; set => aType = value; }

        protected int GetValue()
        {
            Random rnd = new();
            value = rnd.Next(100);
            hitValue = (value / 10) + ((value % 10) * 10);
            int ballisticSkill = accuracy + buffs[0, aim] + buffs[1, range] + buffs[2, mode];
            degree = (ballisticSkill - value) / 10.0;
            return ballisticSkill;
        }

        protected static int RollDmg(int add)
        {
            Random rnd1 = new();
            return rnd1.Next(11) + add;
        }

        public string WriteQualitiesFire()
        {
            string[] qualitiesList = GetQualities();
            if (error)
            {
                return "Для проверки характеристик огнестрельного оружия используйте ?стрелять {название}, а для гранат ?бросать {название}";
            }

            string result = $"```diff\nКласс оружия: \r-{stats[0]}\nДистанция стрельбы: \r-{stats[1]} м\nПараметр урона: \r-{dmgInd}\nВместимость магазина: \r-{stats[2]}\nБронебойность: \r-{pen}\nВес: \r-{stats[3]} кг```";
            string buf = string.Empty;
            for (int i = 0; i < qualitiesList.Length; i++)
            {
                if (qualities[i])
                {
                    buf += qualitiesList[i];
                }
            }

            result += buf == string.Empty ? "Особенностей нет" : buf;
            return result;
        }

        public string WriteQualitiesGrenade()
        {
            string[] qualitiesList = GetQualities();
            if (error)
            {
                return "Для проверки характеристик огнестрельного оружия используйте ?стрелять {название}, а для гранат ?бросать {название}";
            }

            string result = $"```diff\nКласс оружия: \r-{stats[0]}\nДистанция броска: \r-{stats[1]} м\nФормула урона: \r-{stats[2]}\nБронебойность: \r-{pen}\nВес: \r-{stats[3]} кг```";
            string buf = string.Empty;
            for (int i = 0; i < qualitiesList.Length; i++)
            {
                if (qualities[i])
                {
                    buf += qualitiesList[i];
                }
            }

            result += buf == string.Empty ? "Особенностей нет" : buf;
            return result;
        }

        protected static int HitPointCalculate(int hitValue)
        {
            if (hitValue > 0 & hitValue < 11)
            {
                return 0;
            }

            if (hitValue > 10 & hitValue < 31)
            {
                if (hitValue > 20)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }

            if (hitValue > 30 & hitValue < 71)
            {
                return 1;
            }

            if (hitValue > 70 & hitValue < 100 || hitValue == 0)
            {
                if (hitValue == 0 || hitValue > 85)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }

            return 6;
        }

        private string[] GetQualities()
        {
            string[] qualitiesList = new string[]
            {
                "```Точное.\nЭто оружие предназначено для точного поражения отдельных " +
                "целей и бесподобно в умелых руках. При прицеливании стрелок получает дополнительный бонус +10 к " +
                "меткости (этот бонус складывается с тем, что дает само прицеливание). Прицелившись и выстрелив " +
                "в одиночном режиме из основного оружия, имеющего эту особенность, стрелок наносит дополнительно " +
                "1к10 единиц урона за каждые две ступени успеха до максимума в 2к10.```",

                "```Разброс.\nСтандартные боеприпасы для этого оружия содержат дробь или иные подобные поражающие элементы. " +
                "При стрельбе в упор, такое оружие получает бонус +10 к тесту на попадание и наносит +3 единицы урона. На " +
                "короткой дистанции оно получает только +10 к тесту на попадание. При стрельбе на более дальнюю дистанцию, " +
                "урон оружия уменьшается на 3.```",

                "```Оглушающее.\nВыстрел из этого оружия порождает ударную волну или громкий звук. Цель, получившая попадание из оружия с такой " +
                "особенностью,оглушается на один раунд за каждую ступень провала. Если цель получает количество урона, большее, чем " +
                "еѐ бонус Силы, еѐ сбивает с ног.```",

                $"```Взрыв({x}).\nПри расчете попадания из оружия с этой особенностью, все, находящиеся в указанном в скобках количестве " +
                "метров от места, куда пришлось попадание, считаются задетыми взрывом. Все, задетые взрывом, получают одинаковое " +
                "количество урона```",

                $"```Дымовое({x}).\nЭто оружие не столько наносит урон, сколько заволакивает всѐ вокруг клубами дыма, позволяя прятаться" +
                " в нем от вражеских глаз. Попадание из оружия с особенностью «Дымовое» создает в месте попадания облако дыма, диаметр" +
                " которого равен числу в скобках.```",

                "```Огненное.\nНекоторое оружие при стрельбе изрыгает огромную струю пламени, поджигающую всѐ, во что попадет. Цель, " +
                "получившая попадание из огненного оружия (даже если она не получила урона), должна преуспеть в тесте Ловкости или " +
                "загореться.```",

                "```Распыляющее.\nНекоторые виды оружия при стрельбе выбрасывают перед собой конус пламени, жидкости или других поражающих" +
                " элементов. В отличие от других видов оружия, у распыляющего есть только одна дистанция стрельбы. Стрелку не требуется бросать" +
                " тест Дальнего боя – он просто жмет на гашетку. Зона поражения такого оружия – конус с углом в 30 градусов и длиной, равной " +
                "дистанции стрельбы. Все, находящиеся в этой зоне в момент выстрела, должны пройти средний (+0) тест Ловкости или получить обычный" +
                " для этого оружия урон. Укрытия не спасает персонажей от атак распыляющим оружием (если только оно не скрывает персонажа" +
                " полностью). Поскольку при стрельбе из распыляющего оружия не бросается тест Дальнего боя, считается, что любое попадание из " +
                "него приходится в торс цели.```",
            };

            return qualitiesList;
        }
    }
}

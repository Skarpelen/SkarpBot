using Microsoft.EntityFrameworkCore;
using SkarpBot.Data.Context;
using SkarpBot.Data.Models;

namespace SkarpBot.Data
{
    public partial class DataAccessLayer
    {
        public async Task Register(ulong id, string name)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(new PersonHP { Id = id, Head = 12, Body = 28, LHand = 17, RHand = 17, LFoot = 20, RFoot = 20 });
            context.Add(new PersonArmour { Id = id, Name = name });
            await context.SaveChangesAsync();
        }

        public async Task Nuller(ulong id, string armourName)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.Persons
                .FindAsync(id);
            var armour = await context.Armour
                .FindAsync(id);
            if (context.Persons.Any(x => x.Id == id))
            {
                person.Head = 12;
                person.Body = 28;
                person.LHand = 17;
                person.RHand = 17;
                person.RFoot = 20;
                person.LFoot = 20;
                armour.Name = armourName;

                await context.SaveChangesAsync();
                return;
            }
            await Register(id, armourName);
        }

        public async Task ChangeHp(ulong id, int point, int coeff)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.Persons
                .FindAsync(id);
            switch (point)
            {
                case 0:
                    person.Head -= coeff;
                    if (person.Head < 0)
                        person.Head = 0;
                    break;

                case 1:
                    person.Body -= coeff;
                    if (person.Body < 0)
                        person.Body = 0;
                    break;

                case 2:
                    person.LHand -= coeff;
                    if (person.LHand < 0)
                        person.LHand = 0;
                    break;

                case 3:
                    person.RHand -= coeff;
                    if (person.RHand < 0)
                        person.RHand = 0;
                    break;

                case 4:
                    person.LFoot -= coeff;
                    if (person.LFoot < 0)
                        person.LFoot = 0;
                    break;

                case 5:
                    person.RFoot -= coeff;
                    if (person.RFoot < 0)
                        person.RFoot = 0;
                    break;
            }

            await context.SaveChangesAsync();
        }

        public async Task CangeFullHp(ulong id, int coeff)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.Persons
                .FindAsync(id);

            person.Head -= coeff;
            if (person.Head < 0)
                person.Head = 0;

            person.Body -= coeff;
            if (person.Body < 0)
                person.Body = 0;

            person.LHand -= coeff;
            if (person.LHand < 0)
                person.LHand = 0;

            person.RHand -= coeff;
            if (person.RHand < 0)
                person.RHand = 0;

            person.LFoot -= coeff;
            if (person.LFoot < 0)
                person.LFoot = 0;

            person.RFoot -= coeff;
            if (person.RFoot < 0)
                person.RFoot = 0;
        }

        public string GetHp(ulong id)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = context.Persons
                .Find(id);

            string result = $"Текущее состояние <@{id}>\n" +
                $"\n**Голова: **{person.Head / 12.0 * 100.0}%" +
                $"\n**Тело: **{person.Body / 28.0 * 100.0}%" +
                $"\n**Левая рука: **{person.LHand / 17.0 * 100.0}%" +
                $"\n**Правая рука: **{person.RHand / 17.0 * 100.0}%" +
                $"\n**Левая нога: **{person.LFoot / 20.0 * 100.0}%" +
                $"\n**Правая нога: **{person.RFoot / 20.0 * 100.0}%";

            return result;
        }

        public string GetArmour(ulong id)
        {
            using var context = _contextFactory.CreateDbContext();
            var armour = context.Armour
                .Find(id);

            return armour.Name;
        }
    }
}

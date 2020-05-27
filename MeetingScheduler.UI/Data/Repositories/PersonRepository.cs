using MeetingScheduler.DataAccess;
using MeetingScheduler.Model;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MeetingScheduler.UI.Data.Repositories
{
    // Teljes Person-nal kapcsolatos adat műveletek függvényeit tartalmazó file
    public class PersonRepository : IPersonRepository
    {
        private MeetingSchedulerDbContext _context;

        public PersonRepository(MeetingSchedulerDbContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.People.Add(person);
        }

        // Egyelőre visszaad pár Person-t, később adatbázisból húzza le az adatokat
        public async Task<Person> GetByIdAsync(int personId)
        {
            return await _context.People.SingleAsync(f => f.Id == personId);



            // Entity framework és adatbázis implementálása előtt ez biztosította az adatot
            /*
            yield return new Person { FirstName = "Valaki", LastName = "Kitalalt" };
            yield return new Person { FirstName = "Mas", LastName = "Valaki" };
            yield return new Person { FirstName = "Horvath", LastName = "Barnabas" };
            yield return new Person { FirstName = "Nem", LastName = "Letezo" };
            */
        }

        // megnézi a dbcontext-ből, hogy van-e változás
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Person model)
        {
            _context.People.Remove(model);
        }

        // Mentésért felelős
        public async Task SaveAsync()
        {

            // Meghívjuk a SaveChangesAsync-ot ami tényleg elmenti a változást
            await _context.SaveChangesAsync();
        }
    }
}

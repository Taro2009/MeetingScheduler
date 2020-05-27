using MeetingScheduler.Model;
using System.Data.Entity.Migrations;

namespace MeetingScheduler.DataAccess.Migrations
{
    // Code first megközelítés konfigurációja, itt állíthatom be többek között azt is, hogy mi kerüljön az adatbázisba inicializáláskor
    internal sealed class Configuration : DbMigrationsConfiguration<MeetingScheduler.DataAccess.MeetingSchedulerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MeetingScheduler.DataAccess.MeetingSchedulerDbContext context)
        {
            // FirstName alapján azonosítunk
            context.People.AddOrUpdate(
                f => f.FirstName,
                new Person { FirstName = "Valaki", LastName = "Kitalalt" },
                new Person { FirstName = "Mas", LastName = "Valaki" },
                new Person { FirstName = "Horvath", LastName = "Barnabas" },
                new Person { FirstName = "Nem", LastName = "Letezo" }
                );
        }
    }
}

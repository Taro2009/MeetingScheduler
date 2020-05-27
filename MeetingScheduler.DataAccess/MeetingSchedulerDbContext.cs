using MeetingScheduler.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MeetingScheduler.DataAccess
{
    public class MeetingSchedulerDbContext:DbContext
    {
        // A DbContext az, aminek segítségével az Entity Framework-ön keresztül zajlik az adatbázis kommunikáció, illetve adatok változásának jelzésére is használatos
        public MeetingSchedulerDbContext():base("MeetingSchedulerDb")
        {

        }
        public DbSet<Person> People { get; set; }

        // Beállítjuk, hogy ne tegye többesszámba a tábla neveket egy override-val
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}

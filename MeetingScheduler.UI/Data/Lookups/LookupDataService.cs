using MeetingScheduler.DataAccess;
using MeetingScheduler.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingScheduler.UI.Data.Lookups
{
    // Kiszedi az adatbázisból a navigációs elemeket
    public class LookupDataService : IPersonLookupDataService
    {
        private Func<MeetingSchedulerDbContext> _contextCreator;

        public LookupDataService(Func<MeetingSchedulerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetPersonLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.People.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.FirstName + " " + f.LastName
                    })
                    .ToListAsync();
            }
        }


    }
}

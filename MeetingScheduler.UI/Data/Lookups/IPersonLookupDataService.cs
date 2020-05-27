using MeetingScheduler.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingScheduler.UI.Data.Lookups
{
    public interface IPersonLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetPersonLookupAsync();
    }
}
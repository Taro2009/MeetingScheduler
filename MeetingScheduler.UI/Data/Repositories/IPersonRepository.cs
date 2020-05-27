using MeetingScheduler.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingScheduler.UI.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int personId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Person person);
        void Remove(Person model);
    }
}
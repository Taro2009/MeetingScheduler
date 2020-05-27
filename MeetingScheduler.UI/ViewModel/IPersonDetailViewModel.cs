using System.Threading.Tasks;

namespace MeetingScheduler.UI.ViewModel
{
    public interface IPersonDetailViewModel
    {
        Task LoadAsync(int? personId);
        bool HasChanges { get; }
    }
}
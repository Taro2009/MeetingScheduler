using MeetingScheduler.Model;
using MeetingScheduler.UI.Data;
using MeetingScheduler.UI.Data.Lookups;
using MeetingScheduler.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingScheduler.UI.ViewModel
{
    // A UI "bal oldali" navigációs része mögötti ViewModel, logika
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IPersonLookupDataService _personLookupService;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> People { get; }

        public NavigationViewModel(IPersonLookupDataService personLookupService, IEventAggregator eventAggregator)
        {
            _personLookupService = personLookupService;
            _eventAggregator = eventAggregator;
            People = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterPersonSavedEvent>().Subscribe(AfterPersonSaved);
            _eventAggregator.GetEvent<AfterPersonDeletedEvent>().Subscribe(AfterPersonDeleted);
        }

        private void AfterPersonDeleted(int personId)
        {
            var person = People.SingleOrDefault(f => f.Id == personId);
            if (person!=null)
            {
                People.Remove(person);
            }
        }

        private void AfterPersonSaved(AfterPersonSavedEventArgs obj)
        {
            // Kiveszem azt a Person-t, amelyik el lett mentve a másik view modelben
            var lookupItem = People.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                People.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                // Frissítem a DisplayMember mezőt, a megjelenítés miatt
                lookupItem.DisplayMember = obj.DisplayMember;
            }
        }

        // Feltölti a People nevű ObservableCollection-t 
        public async Task LoadAsync()
        {
            var lookup = await _personLookupService.GetPersonLookupAsync();
            People.Clear();
            foreach (var item in lookup)
            {
                People.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

    }
}

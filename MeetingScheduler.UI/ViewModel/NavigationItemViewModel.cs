using MeetingScheduler.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeetingScheduler.UI.ViewModel
{
    // A "bal oldali" navigációs menü elemeinek ViewModel-je
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenPersonDetailViewCommand = new DelegateCommand(OnOpenPersonDetailView);
            _eventAggregator = eventAggregator;
        }

        private void OnOpenPersonDetailView()
        {
            _eventAggregator.GetEvent<OpenPersonDetailViewEvent>()
                .Publish(Id);
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenPersonDetailViewCommand { get; }

        private IEventAggregator _eventAggregator;
    }
}

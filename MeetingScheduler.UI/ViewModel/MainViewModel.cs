using MeetingScheduler.UI.Event;
using MeetingScheduler.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeetingScheduler.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IPersonDetailViewModel> personDetailViewModelCreator, IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService)
        {
            NavigationViewModel = navigationViewModel;
            _personDetailViewModelCreator = personDetailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<AfterPersonDeletedEvent>().Subscribe(AfterPersonDeleted);

            // Ha a View-ban az új ember hozzáadásra kattintunk
            CreateNewPersonCommand = new DelegateCommand(OnCreateNewPersonExecute);

            // Ez felelős a navigációért, "subscribe"-olunk a navigációban történő emberre kattintásra
            _eventAggregator.GetEvent<OpenPersonDetailViewEvent>()
    .Subscribe(OnOpenPersonDetailView);
        }

        private void AfterPersonDeleted(int personId)
        {
            PersonDetailViewModel = null;
        }

        private void OnCreateNewPersonExecute()
        {
            // A már meglévő OnOpenPersonDetailView-t újra lehet használni null értékkel
            OnOpenPersonDetailView(null);
        }

        public INavigationViewModel NavigationViewModel { get; }

        private Func<IPersonDetailViewModel> _personDetailViewModelCreator;

        // Betölti a "bal oldali" lista nézetbe az embereket
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        // Ha rákattintunk egy Person-ra, ez fogja betölteni az adatait
        private async void OnOpenPersonDetailView(int? personId)
        {
            // Megnézzük, hogy van-e már betöltve egy viewmodel, és hogy van-e változás, mert ha ezek mind igazak, akkor jelezni kell a felhasználónak, hogy elkattintáskor
            // El fogja veszíteni a változtatásokat
            if(PersonDetailViewModel!=null && PersonDetailViewModel.HasChanges)
            {
                // Ha a user elkattint, feldobunk egy ablakot ahol megerősítést kérünk tőle
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Click away still?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    // Ha a user Cancel-re nyomott, nem megyünk át másik ViewModel-ra
                    return;
                }
            }
            PersonDetailViewModel = _personDetailViewModelCreator();
            await PersonDetailViewModel.LoadAsync(personId);
        }



        public ICommand CreateNewPersonCommand { get; }

        private IPersonDetailViewModel _personDetailViewModel;

        // Ahhoz kell, hogy új PersonDetailViewModel-eket tudjunk csinálni az OnOpenPersonDetailView függvényben
        public IPersonDetailViewModel PersonDetailViewModel
        {
            get { return _personDetailViewModel; }
            private set { _personDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
    }

}

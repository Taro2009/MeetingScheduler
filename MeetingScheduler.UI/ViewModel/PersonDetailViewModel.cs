using MeetingScheduler.Model;
using MeetingScheduler.UI.Data;
using MeetingScheduler.UI.Data.Repositories;
using MeetingScheduler.UI.Event;
using MeetingScheduler.UI.View.Services;
using MeetingScheduler.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeetingScheduler.UI.ViewModel
{
    public class PersonDetailViewModel : ViewModelBase, IPersonDetailViewModel
    {
        private IPersonRepository _personRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private PersonWrapper _person;

        public PersonDetailViewModel(IPersonRepository personRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _personRepository = personRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            // Prism-t használjuk a mentéshez
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        // Törlési logika
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete {Person.FirstName} {Person.LastName}", "Question");
            if (result == MessageDialogResult.OK)
            { 
            _personRepository.Remove(Person.Model);
            await _personRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterPersonDeletedEvent>().Publish(Person.Id);
            }
        }

        // Id alapján kap egy Person-t
        public async Task LoadAsync(int? personId)
        {
            // Ha az id-ban van elem, akkor lekérjük a detail nézetbe az embert, ha nincs, akkor tudjuk hogy új ember lett hozzáadva
            var person = personId.HasValue
                ? await _personRepository.GetByIdAsync(personId.Value)
                : CreateNewPerson();

            Person = new PersonWrapper(person);
            Person.PropertyChanged += (s, e) =>
              {
                  if (!HasChanges)
                  {
                      // Ha a property változik és a HasChanges még nincs beállítva, akkor állítsuk igazra
                      HasChanges = _personRepository.HasChanges();
                  }
                  if (e.PropertyName==nameof(Person.HasErrors))
                  {
                      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                  }
              };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if(Person.Id == 0)
            {
                // Ennek hatására ha hozzáadunk egy új embert, akkor is bekapcsol a validáció, enélkül lehetne üres mezővel hozzáadni és az entity framework hibát dobna
                Person.FirstName = "";
            }
        }

        private Person CreateNewPerson()
        {
            var person = new Person();
            _personRepository.Add(person);
            return person;
        }

        private bool _hasChanges;

        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                if (_hasChanges != value) 
                { 
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        public PersonWrapper Person
        {
            get { return _person; }
            private set
            {
                _person = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }
        
        private bool OnSaveCanExecute()
        {
            return Person!=null && !Person.HasErrors && HasChanges;
        }
        
        private async void OnSaveExecute()
        {
            await _personRepository.SaveAsync();
            HasChanges = _personRepository.HasChanges();
            // Eventet hoz létre, amire a NavigationViewModel feliratkozik, hogy tudja, hogy frissíteni kell a nézetet.
            _eventAggregator.GetEvent<AfterPersonSavedEvent>().Publish(
                new AfterPersonSavedEventArgs
                {
                    Id = Person.Id,
                    DisplayMember = $"{Person.FirstName} {Person.LastName}" 
                });
        }
       
    }
}

using MeetingScheduler.UI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MeetingScheduler.UI.Wrapper
{
    // Hibakezeléssel kapcsolatos dolgokat tartalmaz
    public class NotifyDataErrorInfoBase: ViewModelBase, INotifyDataErrorInfo
    {
        // INotifyDataError -hoz tartozó dolgok, én az error-okat egy property névhez dictionary-ben tárolom.

        private Dictionary<string, List<string>> _errorsByPropertyName
            = new Dictionary<string, List<string>>();

        // HasErrors true-t ad vissza, ha a dictionary-ben van valami
        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            // Ha benne van a dictionary-ben az átvett propertyName kulcsként, akkor adjuk vissza a hozzá tartozott értéket, egyébként adjunk vissza null-t
            return _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : null;
        }

        // Hiba változását nézi
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }

        // metódus, hogy könnyen tudjunk errort hozzáadni
        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        // Hibák törlése
        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}

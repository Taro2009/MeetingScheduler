using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MeetingScheduler.UI.Wrapper
{
    // A PersonWrapper számára biztosítja a logikát az adatok eléréséhez a Model-ből. Emellett a Model-ben található szabályokat validálja
    public class ModelWrapper<T>: NotifyDataErrorInfoBase
    {
        public ModelWrapper(T model)
        {
            Model = model;
        }

        public T Model { get; }
        // Általános fv, ami lekéri a modelből azt, amit propertyName-ként megkap. PersonWrapper-ből a [CallerMemberName]-el tudjuk, hogy mi hívta
        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(); 
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);

            ValidateDataAnnotations(propertyName, currentValue);
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            // Modell-ben található adat "szabályok" validálása

            // Ez fogja tárolni a validáció eredméneyit
            var results = new List<ValidationResult>();

            // Ez nézi meg, hogy a model-ünkben mik a szabályok
            var context = new ValidationContext(Model) { MemberName = propertyName };

            // Ez végzi el a validálást, az eredményt beleteszi a végén a results-ba
            Validator.TryValidateProperty(currentValue, context, results);

            // Végigmegyünk a results-on, minden validálási hibára meghívjuk az AddError()-t és beletesszük a dictionary-nkbe a hibát
            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

    }
}

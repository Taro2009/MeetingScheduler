using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetingScheduler.UI.ViewModel
{
    // Csinálunk egy baseclass-t, mert ezt többször is kell majd használni
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Készítünk egy metódust, mert több property-nk is lesz később. Ez a metódus fogja hívni a property changed eventet
        // Ez a CallerMemberName azért kell, hogy paraméter nélkül hívhassuk meg a függvényt, a compiler ennek határásra automatikusan átadja a nevet
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            // Megnézi, hogy a property changed 0-e, majd invoke-al meghívja az eventet, ennek 2 paramétere van. 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}

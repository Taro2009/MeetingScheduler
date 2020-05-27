using Prism.Events;

namespace MeetingScheduler.UI.Event
{
    // A NavigationViewModel-nek fogja jelezni, hogy mentés történt és frissítse a nézetet
    public class AfterPersonSavedEvent: PubSubEvent<AfterPersonSavedEventArgs>
    {
    }

    public class AfterPersonSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}

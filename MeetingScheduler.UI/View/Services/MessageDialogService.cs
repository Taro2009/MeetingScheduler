using System.Windows;

namespace MeetingScheduler.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        // Ez a class feldob egy custom message boxot, emellett pedig visszaadja a kattintás eredményét
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }
    }
    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}

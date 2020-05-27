using MeetingScheduler.UI.ViewModel;
using System.Windows;

namespace MeetingScheduler.UI
{

    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        // Mivel ahhoz, hogy minden meglegyen és a viewmodellünk megfelelően működjön meg kell hívni a Load metódust rá, ezért kell ez a kódrészlet, ami lényegében pont ezt csinálja.
        // Először eltárolja a viewModel-ünket egy field-ben, majd meghívja a Load metódust. 
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}

using Autofac;
using MeetingScheduler.UI.Data;
using MeetingScheduler.UI.Startup;
using MeetingScheduler.UI.ViewModel;
using System;
using System.Windows;

namespace MeetingScheduler.UI
{
    public partial class App : Application
    {

        // Saját main window létrehozása, hogy ne kapjunk exception-t
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Ezek kellenek az Autofac containerhez
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        // Kezeli a nem várt hibákat
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured." + Environment.NewLine + e.Exception.Message, "Unexpected Error");

            e.Handled = true;
        }
    }
}

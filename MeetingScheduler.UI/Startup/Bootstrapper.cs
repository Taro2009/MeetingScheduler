using Autofac;
using MeetingScheduler.DataAccess;
using MeetingScheduler.UI.Data.Lookups;
using MeetingScheduler.UI.Data.Repositories;
using MeetingScheduler.UI.View.Services;
using MeetingScheduler.UI.ViewModel;
using Prism.Events;

namespace MeetingScheduler.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MeetingSchedulerDbContext>().AsSelf();


            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();

            // Ezzel állítjuk be, hogy ha IPersonDataService kell, akkor PersonDataService-t használjuk, ettől a container tudni fogja, hogy ha kell egy IPerson... akkor csináljon egy PersonD...
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<PersonDetailViewModel>().As<IPersonDetailViewModel>();

            // Ezzel létrehozzuk a containert és visszaadjuk
            return builder.Build();
        }
    }
}

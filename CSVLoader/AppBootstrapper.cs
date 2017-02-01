using Caliburn.Micro;
using CSVLoader.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace CSVLoader
{
    public class AppBootstrapper : BootstrapperBase
    {
        SimpleContainer container = new SimpleContainer();
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();

            string connString = ConfigurationManager.ConnectionStrings["CSVLoader"].ConnectionString;

            container.Instance<IDbWriterService>(new DbWriterService(connString));
            container.Singleton<IOpenFileService, OpenFileService>();
            container.Singleton<IValidator, Validator>();

            container.PerRequest<ILoaderService, LoaderService>();
            container.PerRequest<LoaderViewModel, LoaderViewModel>();
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoaderViewModel>();
        }
    }
}

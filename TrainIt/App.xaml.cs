using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel;

namespace TrainIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TrainItService _trainItService;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var mainView = new MainWindow();
                mainView.Show();

                var _dialogCoordinator = DialogCoordinator.Instance;

                _trainItService = new TrainItService(_dialogCoordinator);
                mainView.DataContext = new MainWindowViewModel(_trainItService , _dialogCoordinator);

                RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _trainItService.Dispose();
            base.OnExit(e);
        }
    }
}

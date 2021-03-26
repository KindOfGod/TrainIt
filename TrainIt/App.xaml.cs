using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TrainIt.Model;
using TrainIt.ViewModel;

namespace TrainIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            TrainItService trainItService = new TrainItService(); ;

            try
            {
                var mainView = new MainWindow();
                mainView.Show();
                mainView.DataContext = new MainWindowViewModel(trainItService);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

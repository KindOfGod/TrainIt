using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainIt.Model;

namespace TrainIt.View.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            Loaded += ChildLoaded;
        }

        private void ChildLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if(window != null)
                window.Closing += ParentClosing;
        }

        private static void ParentClosing(object sender, CancelEventArgs e)
        {
            SettingsService.SaveSetting();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace TrainIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool _stateClosed = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (_stateClosed)
            {
                var sb = FindResource("OpenMenu") as Storyboard;
                sb?.Begin();
            }
            else
            {
                var sb = FindResource("CloseMenu") as Storyboard;
                sb?.Begin();
            }

            _stateClosed = !_stateClosed;
        }
    }
}

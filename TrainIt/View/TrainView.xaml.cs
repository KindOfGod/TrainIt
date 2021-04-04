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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel;

namespace TrainIt.View
{
    /// <summary>
    /// Interaction logic for TrainViewModel.xaml
    /// </summary>
    public partial class TrainView : UserControl
    {
        private readonly TrainViewModel _trainViewModel;

        public TrainView(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            InitializeComponent();

            _trainViewModel = new TrainViewModel(trainItService, dialogCoordinator);
            DataContext = _trainViewModel;
        }
    }
}

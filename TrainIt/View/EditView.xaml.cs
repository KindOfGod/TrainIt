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
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        private readonly EditViewModel _editViewModel;

        public EditView(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            InitializeComponent();
            _editViewModel = new EditViewModel(trainItService, dialogCoordinator);
            DataContext = _editViewModel;
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _editViewModel.SelectedItem = e.NewValue;
        }
    }
}

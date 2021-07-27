using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel.DialogViewModel;

namespace TrainIt.Dialogs.Edit
{
    /// <summary>
    /// Interaction logic for CreateLanguageDialog.xaml
    /// </summary>
    public partial class CreateLanguageDialog : MetroWindow
    {
        public readonly CreationDialogViewModel _createLanguageDialogViewModel;

        public CreateLanguageDialog(TrainItService trainItService, IDialogCoordinator dialogCoordinator, int mode)
        {
            InitializeComponent();
            _createLanguageDialogViewModel = new CreationDialogViewModel(trainItService, dialogCoordinator, mode);
            DataContext = _createLanguageDialogViewModel;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_createLanguageDialogViewModel.IsInputValid())
                DialogResult = true;
        }
    }
}

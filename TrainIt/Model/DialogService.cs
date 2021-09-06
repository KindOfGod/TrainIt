using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using TrainIt.Annotations;
using TrainIt.Dialogs.DialogViewModels;
using TrainIt.Interfaces;

namespace TrainIt.Model
{
    public static class DialogService
    {
        private static string _currentIdentifier;

        [ItemCanBeNull]
        public static async Task<object> ShowDialog(IDialog dialog, string identifier = "RootHost")
        {
            _currentIdentifier = identifier;
            return await DialogHost.Show(dialog, identifier);
        }

        public static async void ShowErrorDialog(string errorHeader, string errorMessage, string identifier = "RootHost")
        {
            var dialog = new ErrorDialogViewModel(errorHeader, errorMessage);

            _currentIdentifier = identifier;
            await DialogHost.Show(dialog, identifier);
        }

        public static void CloseAllDialogs()
        {
            if(_currentIdentifier != null)
                DialogHost.GetDialogSession(_currentIdentifier)?.Close();

            _currentIdentifier = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using TrainIt.Annotations;
using TrainIt.Interfaces;
using TrainIt.ViewModel.DialogViewModels;

namespace TrainIt.Model
{
    public static class DialogService
    {
        private static string _currentIdentifier;

        [ItemCanBeNull]
        public static async Task<object> OpenDialog(IDialog dialog, string identifier = "RootHost")
        {
            _currentIdentifier = identifier;
            return await DialogHost.Show(dialog, identifier);
        }

        public static void CloseAllDialogs()
        {
            if(_currentIdentifier != null)
                DialogHost.GetDialogSession(_currentIdentifier)?.Close();

            _currentIdentifier = null;
        }
    }
}

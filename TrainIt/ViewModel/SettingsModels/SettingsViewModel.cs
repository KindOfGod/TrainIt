using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrainIt.ViewModel.SettingsModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Fields

        private readonly GeneralSettingsViewModel _generalSettingsViewModel;

        private BaseViewModel _selectedViewModel;
        private ListBoxItem _selectedItem;

        #endregion

        #region Properties

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                if (_selectedViewModel == value)
                    return;

                _selectedViewModel = value;
                OnPropertyChange();
            }
        }

        public ListBoxItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;

                _selectedItem = value;
                OnSelectionChanged();
                OnPropertyChange();
            }
        }

        #endregion

        #region Constructors

        public SettingsViewModel()
        {
            _generalSettingsViewModel = new GeneralSettingsViewModel();

            SelectedViewModel = _generalSettingsViewModel;
        }

        #endregion

        #region private Methods

        private void OnSelectionChanged()
        {
            switch (SelectedItem.Name)
            {
                case "GeneralSettings":
                    SelectedViewModel = _generalSettingsViewModel;
                    break;
            }
        }
        #endregion
    }
}

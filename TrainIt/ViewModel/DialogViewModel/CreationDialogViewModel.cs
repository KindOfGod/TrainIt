using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Helper;
using TrainIt.Model;

namespace TrainIt.ViewModel.DialogViewModel
{
    public class CreationDialogViewModel : BaseViewModel
    {
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private ObservableCollection<string> _paths;
        private Visibility _hasIcon = Visibility.Hidden;
        private string _selectedIcon;
        private string _name;
        private readonly int _mode;

        #region Properties

        public ObservableCollection<string> Paths
        {
            get => _paths;
            private set
            {
                if (_paths == value) 
                    return;

                _paths = value;
                OnPropertyChange();
            }
        }

        public string SelectedIcon
        {
            get => _selectedIcon;
            set
            {
                if (_selectedIcon == value)
                    return;

                _selectedIcon = value;
                OnPropertyChange();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChange();
            }
        }

        public Visibility HasIcon
        {
            get => _hasIcon;
            set
            {
                if (_hasIcon == value)
                    return;

                _hasIcon = value;
                OnPropertyChange();
            }
        }
        
        #endregion

        #region Constructors
        public CreationDialogViewModel(){}

        public CreationDialogViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator, int mode)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
            _mode = mode;

            SetDialogSettings();
        }
        #endregion

        #region Public Methods
        public bool IsInputValid()
        {
            return true;
        }
        #endregion

        #region Private Methods

        private void SetDialogSettings()
        {
            if (_mode != 0)
                return;

            LoadImages();
            HasIcon = Visibility.Visible;
        }

        private void LoadImages()
        {
            var paths = new ObservableCollection<string>();

            foreach (var path in Directory.GetFiles("../../../Resources/Flags", "*.png").ToList())
                paths.Add(path);

            Paths = paths;
        }

        #endregion
    }
}

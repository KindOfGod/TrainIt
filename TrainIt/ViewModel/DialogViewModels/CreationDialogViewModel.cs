using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using TrainIt.Interfaces;
using TrainIt.Model;

namespace TrainIt.ViewModel.DialogViewModels
{
    public class CreationDialogViewModel : BaseViewModel, IDialog
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

        public CreationDialogViewModel() { }

        public CreationDialogViewModel(int mode)
        {
            SetDialogSettings();
            _mode = mode;
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

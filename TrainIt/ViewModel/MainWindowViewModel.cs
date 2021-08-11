using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlzEx.Standard;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Helper;
using TrainIt.Model;
using TrainIt.View;
using TrainIt.ViewModel.SettingsModels;

namespace TrainIt.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly EditViewModel _editViewModel;
        private readonly TrainViewModel _trainViewModel;
        private readonly StatisticsViewModel _statisticsViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        private readonly IDialogCoordinator _dialogCoordinator;

        private TrainItService _trainItService;
        private BaseViewModel _selectedView;
        private BaseViewModel _settingsView;
        private ListBoxItem _selectedItem;

        private bool _isMenuClosed;
        private Visibility _settingsVisible;

        #endregion

        #region Constructors
        public MainWindowViewModel(){}

        public MainWindowViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;

            _dashboardViewModel = new DashboardViewModel(trainItService, dialogCoordinator);
            _editViewModel = new EditViewModel(trainItService, dialogCoordinator);
            _trainViewModel = new TrainViewModel(trainItService, dialogCoordinator);
            _statisticsViewModel = new StatisticsViewModel(trainItService, dialogCoordinator);
            _settingsViewModel = new SettingsViewModel();

            IsMenuClosed = true;
            SettingsVisible = Visibility.Collapsed;
        }
        #endregion

        #region Properties

        public BaseViewModel SelectedViewModel
        {
            get => _selectedView;
            set
            {
                if (_selectedView == value) 
                    return;

                _selectedView = value;
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
                OnSelectedItemChange(value);
                OnPropertyChange();
            }
        }

        public bool IsMenuClosed
        {
            get => _isMenuClosed;
            set
            {
                if (_isMenuClosed == value)
                    return;

                _isMenuClosed = value;
                OnPropertyChange();
            }
        }

        public BaseViewModel SettingsViewModel
        {
            get => _settingsView;
            set
            {
                if (_settingsView == value)
                    return;

                _settingsView = value;
                OnPropertyChange();
            }
        }

        public Visibility SettingsVisible
        {
            get => _settingsVisible;
            set
            {
                if (_settingsVisible == value)
                    return;

                _settingsVisible = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Commands

        public ICommand OpenButtonCommand => new RelayCommand(p => OnOpenMenu());
        public ICommand CloseButtonCommand => new RelayCommand(p => OnCloseMenu());
        public ICommand SettingsButtonCommand => new RelayCommand(p => OnSettingsCommand());

        #endregion

        #region Private Methods

        private void OnOpenMenu()
        {
            IsMenuClosed = false;
        }

        private void OnCloseMenu()
        {
            IsMenuClosed = true;
        }

        private void OnSettingsCommand()
        {
            if (SettingsVisible == Visibility.Collapsed)
            {
                SettingsVisible = Visibility.Visible;
                SettingsViewModel = _settingsViewModel;
            }
            else
            {
                SettingsVisible = Visibility.Collapsed;
                SettingsViewModel = null;
            }
        }
        #endregion

        #region Navigation Methods

        private void OnSelectedItemChange(ListBoxItem item)
        {
            switch (item.Name)
            {
                case "DashboardItem":
                    SelectedViewModel = _dashboardViewModel;
                    break;
                case "EditItem":
                    SelectedViewModel = _editViewModel;
                    break;
                case "TrainItem":
                    SelectedViewModel = _trainViewModel;
                    break;
                case "StatisticsItem":
                    SelectedViewModel = _statisticsViewModel;
                    break;
            }
        }
        #endregion
    }
}

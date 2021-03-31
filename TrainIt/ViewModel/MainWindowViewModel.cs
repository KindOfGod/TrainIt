using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ControlzEx.Standard;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;

namespace TrainIt.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly DashboardViewModel _dashboardViewModel;
        private readonly EditViewModel _editViewModel;
        private readonly TrainViewModel _trainViewModel;
        private readonly StatisticsViewModel _statisticsViewModel;

        private readonly IDialogCoordinator _dialogCoordinator;

        private TrainItService _trainItService;
        private ListBoxItem _selectedItem;
        private BaseViewModel _selectedView;
        #endregion

        #region Constructors
        public MainWindowViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;

            _dashboardViewModel = new DashboardViewModel();
            _editViewModel = new EditViewModel();
            _trainViewModel = new TrainViewModel();
            _statisticsViewModel = new StatisticsViewModel();
        }
        #endregion

        #region Properties

        public BaseViewModel SelectedView
        {
            get { return _selectedView; }
            set
            {
                if (_selectedView != value)
                {
                    _selectedView = value;
                    OnPropertyChange();
                }
            }
        }

        public ListBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnSelectedItemChange(value);
                    OnPropertyChange();
                }
            }
        }

        #endregion

        #region Navigation Methods

        private void OnSelectedItemChange(ListBoxItem item)
        {
            switch (item.Name)
            {
                case "DashboardItem":
                    SelectedView = _dashboardViewModel;
                    break;
                case "EditItem":
                    SelectedView = _editViewModel;
                    break;
                case "TrainItem":
                    SelectedView = _trainViewModel;
                    break;
                case "StatisticsItem":
                    SelectedView = _statisticsViewModel;
                    break;
            }
        }
        #endregion

    }
}

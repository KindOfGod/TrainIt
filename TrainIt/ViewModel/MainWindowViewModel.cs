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
using TrainIt.View;

namespace TrainIt.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private readonly DashboardView _dashboardView;
        private readonly EditView _editView;
        private readonly TrainView _trainView;
        private readonly StatisticsView _statisticsView;

        private readonly IDialogCoordinator _dialogCoordinator;

        private TrainItService _trainItService;
        private UserControl _selectedView;
        private ListBoxItem _selectedItem;
        #endregion

        #region Constructors
        public MainWindowViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;

            _dashboardView = new DashboardView(trainItService, dialogCoordinator);
            _editView = new EditView(trainItService, dialogCoordinator);
            _trainView = new TrainView(trainItService, dialogCoordinator);
            _statisticsView = new StatisticsView(trainItService, dialogCoordinator);
        }
        #endregion

        #region Properties

        public UserControl SelectedView
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
                    SelectedView = _dashboardView;
                    break;
                case "EditItem":
                    SelectedView = _editView;
                    break;
                case "TrainItem":
                    SelectedView = _trainView;
                    break;
                case "StatisticsItem":
                    SelectedView = _statisticsView;
                    break;
            }
        }
        #endregion

    }
}

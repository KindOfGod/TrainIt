using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Classes;
using TrainIt.Model;

namespace TrainIt.ViewModel.EditModels
{
    public class UnitInfoViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private Unit _selectedUnit;
        #endregion

        #region Properties

        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (_selectedUnit != value)
                {
                    _selectedUnit = value;
                    OnPropertyChange();
                }
            }
        }

        #endregion

        #region Constructors
        public UnitInfoViewModel() { }

        public UnitInfoViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
        }
        #endregion
    }
}

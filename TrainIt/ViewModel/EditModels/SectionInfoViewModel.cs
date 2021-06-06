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
    public class SectionInfoViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private Section _selectedSection;
        #endregion

        #region Properties

        public Section SelectedSection
        {
            get => _selectedSection;
            set
            {
                if (_selectedSection != value)
                {
                    _selectedSection = value;
                    OnPropertyChange();
                }
            }
        }

        #endregion

        #region Constructors
        public SectionInfoViewModel() { }

        public SectionInfoViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
        }
        #endregion
    }
}

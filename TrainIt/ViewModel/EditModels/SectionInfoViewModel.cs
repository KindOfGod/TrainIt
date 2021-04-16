using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;

namespace TrainIt.ViewModel.EditModels
{
    public class SectionInfoViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        #endregion


        #region Constructors
        public SectionInfoViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
        }
        #endregion
    }
}

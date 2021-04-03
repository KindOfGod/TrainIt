using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;
using TrainIt.Model;

namespace TrainIt.ViewModel
{
    class EditViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        #endregion

        #region Constructors
        public EditViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
        }
        #endregion
    }
}

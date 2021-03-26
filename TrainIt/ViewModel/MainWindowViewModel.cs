using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Model;

namespace TrainIt.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private TrainItService _trainItService;
        #endregion

        public MainWindowViewModel(TrainItService trainItService)
        {
            _trainItService = trainItService;
        }
    }
}

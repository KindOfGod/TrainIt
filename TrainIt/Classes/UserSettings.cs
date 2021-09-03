using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Annotations;
using TrainIt.Model;
using TrainIt.ViewModel;

namespace TrainIt.Classes
{
    public sealed class UserSettings : BaseViewModel
    {
        #region Fields

        private double _wordStartGrade;

        #endregion

        #region Properties

        public double WordStartGrade
        {
            get => _wordStartGrade;
            set
            {
                if (Equals(_wordStartGrade, value))
                    return;

                _wordStartGrade = value;
                OnPropertyChange();
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.SeriesAlgorithms;
using TrainIt.Classes;
using TrainIt.Model;

namespace TrainIt.ViewModel.SettingsModels
{
    public class GeneralSettingsViewModel : BaseViewModel
    {
        #region Properties

        public double WordStartGrade
        {
            get => SettingsService._settings.WordStartGrade;
            set
            {
                if(Math.Abs(SettingsService._settings.WordStartGrade - value) < 0.05)
                    return;

                SettingsService._settings.WordStartGrade = Math.Round(value, 1);
                OnPropertyChange();
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TrainIt.Classes;
using TrainIt.ViewModel;

namespace TrainIt.Model
{
    public static class SettingsService
    {
        #region Fields

        public static UserSettings UserSettings;
        public static ApplicationSettings ApplicationSettings;

        #endregion

        #region Constructors

        static SettingsService()
        {
            ApplicationSettings = new ApplicationSettings();
            UserSettings = new UserSettings();
        }

        #endregion

        #region Public Methods

        public static void SaveSetting()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(UserSettings);
            File.WriteAllText(ApplicationSettings.UserSettingsPath,json);
        }

        public static void ReadSettings()
        {
            if (File.Exists(ApplicationSettings.UserSettingsPath))
            {
                var json = File.ReadAllText(ApplicationSettings.UserSettingsPath);
                UserSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<UserSettings>(json);
                return;
            }

            DefaultSettings();
        }
        #endregion

        #region Private Methods

        private static void DefaultSettings()
        {
            UserSettings = new UserSettings
            {
                WordStartGrade = 3
            };
        }

        #endregion
    }
}

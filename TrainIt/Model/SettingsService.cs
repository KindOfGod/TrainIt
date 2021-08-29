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

        public static Settings _settings;
        private const string _settingsPath = "./settings.json";

        #endregion

        #region Constructors

        static SettingsService()
        {
            _settings = new Settings();
        }

        #endregion

        #region Public Methods

        public static void SaveSetting()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_settings);
            File.WriteAllText(_settingsPath,json);
        }

        public static void ReadSettings()
        {
            if (File.Exists(_settingsPath))
            {
                var json = File.ReadAllText(_settingsPath);
                _settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(json);
                return;
            }

            DefaultSettings();
        }
        #endregion

        #region Private Methods

        private static void DefaultSettings()
        {
            _settings = new Settings
            {
                WordStartGrade = 3
            };
        }

        #endregion
    }
}

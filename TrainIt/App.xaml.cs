using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel;

namespace TrainIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public enum Skin{ Dark, Light}

    public partial class App : Application
    {
        private TrainItService _trainItService;
        public static Skin Skin { get; set; } = Skin.Dark;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var mainView = new MainWindow();
                mainView.Show();

                var dialogCoordinator = DialogCoordinator.Instance;

                _trainItService = new TrainItService(dialogCoordinator);
                mainView.DataContext = new MainWindowViewModel(_trainItService , dialogCoordinator);
                RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;

                LoadSkin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #region Skin

        private void LoadSkin()
        {
            Resources.Clear();
            Resources.MergedDictionaries.Clear();

            if (SettingsService.UserSettings.Skin == Skin.Light)
                ApplyLightSkin();
            else if (SettingsService.UserSettings.Skin == Skin.Dark)
                ApplyDarkSkin();

            ApplySharedResources();
        }

        public void ChangeSkin()
        {
            Resources.Clear();
            Resources.MergedDictionaries.Clear();

            if (SettingsService.UserSettings.Skin == Skin.Light)
                ApplyDarkSkin();
            else if (SettingsService.UserSettings.Skin == Skin.Dark)
                ApplyLightSkin();

            ApplySharedResources();
            SettingsService.SaveSetting();
        }

        private void ApplyLightSkin()
        {
            AddResourceDictionarys("Resources/Themes/LightTheme.xaml");
            SettingsService.UserSettings.Skin = Skin.Light;
        }
 
        private void ApplyDarkSkin()
        {
            AddResourceDictionarys("Resources/Themes/DarkTheme.xaml");
            SettingsService.UserSettings.Skin = Skin.Dark;
        }

        private void ApplySharedResources()
        {
            AddResourceDictionarys("Resources/SharedResources.xaml");
        }

        private void AddResourceDictionarys(string src)
        {
            var dict = new ResourceDictionary() { Source = new Uri(src, UriKind.Relative) };
            foreach (var mergeDict in dict.MergedDictionaries)
            {
                Resources.MergedDictionaries.Add(mergeDict);
            }
 
            foreach (var key in dict.Keys)
            {
                Resources[key] = dict[key];
            }
        } 

        #endregion

        protected override void OnExit(ExitEventArgs e)
        {
            _trainItService.Dispose();
            base.OnExit(e);
        }
    }
}

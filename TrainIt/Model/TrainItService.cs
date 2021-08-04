using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Classes;

namespace TrainIt.Model
{
    public class TrainItService : IDisposable
    {
        private readonly DatabaseService _databaseService;

        #region Constructors
        public TrainItService()
        {
            _databaseService = new DatabaseService();
        }
        #endregion

        #region Help Methods

        public async void ShowErrorMessage(string errorName, string message)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;

            await metroWindow.ShowMessageAsync("Title", "message");
        }

        #endregion

        #region Database Methods

        public async Task<List<Language>> GetLanguages()
        {
            try
            {
                return await _databaseService.GetLanguages();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
                return null;
            }
        }

        public async Task<List<Section>> GetSections(Language language)
        {
            try
            {
                return await _databaseService.GetSections(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
                return null;
            }
        }

        public async Task<List<Unit>> GetUnits(Section section)
        {
            try
            {
                return await _databaseService.GetUnits(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
                return null;
            }
        }

        public async Task<List<Word>> GetWords(Unit unit)
        {
            try
            {
                return await _databaseService.GetWords(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
                return null;
            }
        }

        public async Task SetLanguage(Language language)
        {
            try
            {
                await _databaseService.SetLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task SetSection(Section section)
        {
            try
            {
                await _databaseService.SetSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task SetUnit(Unit unit)
        {
            try
            {
                await _databaseService.SetUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task SetWord(Word word)
        {
            try
            {
                await _databaseService.SetWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task DeleteLanguages(IEnumerable<Language> languages)
        {
            try
            {
                await _databaseService.DeleteLanguage(languages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task DeleteSections(IEnumerable<Section> sections)
        {
            try
            {
                await _databaseService.DeleteSection(sections);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task DeleteUnits(IEnumerable<Unit> units)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (units == null) throw new ArgumentNullException(nameof(units));
            try
            {
                await _databaseService.DeleteUnit(units);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }

        public async Task DeleteWords(IEnumerable<Word> words)
        {
            try
            {
                await _databaseService.DeleteWord(words);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ShowErrorMessage("Database Error", e.Message);
            }
        }
        #endregion

        #region Public Methods
        public void Dispose()
        {
            _databaseService.Dispose();
        }

        #endregion

        #region Private Methods
        
        #endregion
    }
}

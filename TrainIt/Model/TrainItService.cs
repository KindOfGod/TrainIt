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

        #region Fields

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly DatabaseService _databaseService;

        #endregion

        #region Constructors
        public TrainItService(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            _databaseService = new DatabaseService();

            //var tsk = TestObjects();
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

        public static async void ShowErrorMessage(string title, string message)
        {
            await (Application.Current.MainWindow as MetroWindow).ShowMessageAsync(title, message);
        }

        public void Dispose()
        {
            _databaseService.Dispose();
        }

        #endregion

        #region Private Methods

        #endregion

        #region Methods for Tests

        private async Task TestObjects()
        {
            double grade = 1;

            for (var i = 0; i < 2; i++)
            {
                var language = new Language(Guid.NewGuid(), 1, "Language " + i, @"..\Resources\Flags\DE@3x.png", DateTime.Now, DateTime.Now, true);
                await SetLanguage(language);

                for (var j = 0; j < 30; j++)
                {
                    var section = new Section(Guid.NewGuid(), language.Id, 1, "Section " + j, DateTime.Now, DateTime.Now, DateTime.Now, true);
                    await SetSection(section);

                    for (var k = 0; k < 10; k++)
                    {
                        var unit = new Unit(Guid.NewGuid(), section.Id, grade, "unit " + k, DateTime.Now, DateTime.Now, DateTime.Now, true);
                        await SetUnit(unit);

                        var previousWord = Guid.Empty;

                        for (var u = 0; u < 10; u++)
                        {
                            var word = new Word(Guid.NewGuid(), grade, unit.Id, "word" + u, "word" + u,
                                "word" + u, "word" + u, DateTime.Now, DateTime.Now, DateTime.Now, true);

                            word.PreviousWordId = previousWord;
                            await SetWord(word);

                            previousWord = word.Id;
                        }

                        grade += 0.05;
                    }

                    grade = 1;
                }
            }
        }

        #endregion
    } 
}

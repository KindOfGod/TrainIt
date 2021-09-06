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
        #endregion

        #region Constructors
        public TrainItService(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            //var tsk = TestObjects();
        }
        #endregion

        #region Public Methods

        public static async void ShowErrorMessage(string title, string message)
        {
            await (Application.Current.MainWindow as MetroWindow).ShowMessageAsync(title, message);
        }

        public void Dispose()
        {
            DatabaseService.Dispose();
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
                await DatabaseService.SetLanguage(language);

                for (var j = 0; j < 30; j++)
                {
                    var section = new Section(Guid.NewGuid(), language.Id, 1, "Section " + j, DateTime.Now, DateTime.Now, DateTime.Now, true);
                    await DatabaseService.SetSection(section);

                    for (var k = 0; k < 10; k++)
                    {
                        var unit = new Unit(Guid.NewGuid(), section.Id, grade, "unit " + k, DateTime.Now, DateTime.Now, DateTime.Now, true);
                        await DatabaseService.SetUnit(unit);

                        var previousWord = Guid.Empty;

                        for (var u = 0; u < 10; u++)
                        {
                            var word = new Word(Guid.NewGuid(), grade, unit.Id, "word" + u, "word" + u,
                                "word" + u, "word" + u, DateTime.Now, DateTime.Now, DateTime.Now, true);

                            word.PreviousWordId = previousWord;
                            await DatabaseService.SetWord(word);

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

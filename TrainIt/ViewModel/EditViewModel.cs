using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using TrainIt.Classes;
using TrainIt.Model;

namespace TrainIt.ViewModel
{
    public class EditViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private List<Language> _languageList;
        private object _selectedItem;
        #endregion

        #region Properties

        public List<Language> LanguageList
        {
            get { return _languageList;}
            set
            {
                if (_languageList != value)
                {
                    _languageList = value;
                    OnPropertyChange();
                }
            }
        }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChange();
                    OnSelectionChanged(_selectedItem);
                }
            }
        }
        #endregion

        #region Constructors
        public EditViewModel(){}

        public EditViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;

            //TestObjects();
            OnLoad();
        }
        #endregion

        #region Methods
        private void OnLoad()
        {
            LanguageList = _trainItService.GetLanguages();

            foreach (var language in LanguageList)
            {
                language.Sections = _trainItService.GetSections(language);

                foreach (var section in language.Sections)
                {
                    section.Units = _trainItService.GetUnits(section);
                }
            }
        }

        private void OnSelectionChanged(object o)
        {

        }

        private void TestObjects()
        {

            for (int i = 0; i < 5; i++)
            {
                var language = new Language(Guid.NewGuid(), 1, "Unit", "path", DateTime.Now, DateTime.Now, DateTime.Now, true);
                _trainItService.SetLanguage(language);

                var section = new Section(Guid.NewGuid(), language.Id, 1, "Section", DateTime.Now, DateTime.Now, DateTime.Now, true);
                _trainItService.SetSection(section);

                for (int k = 0; k < 100; k++)
                {
                    _trainItService.SetUnit(new Unit(Guid.NewGuid(), "unit " + k, 1, section.Id, DateTime.Now, DateTime.Now,
                        DateTime.Now, true));
                }
            }
        }
        #endregion
    }
}

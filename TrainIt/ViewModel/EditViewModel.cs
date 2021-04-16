using MahApps.Metro.Controls.Dialogs;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using TrainIt.Classes;
using TrainIt.Model;
using TrainIt.View.Edit;

namespace TrainIt.ViewModel
{
    public class EditViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;

        private readonly LanguageInfoView _languageInfoView;
        private readonly SectionInfoView _sectionInfoView;
        private readonly UnitInfoView _unitInfoView;

        private UserControl _selectedView;
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

        public UserControl SelectedView
        {
            get { return _selectedView; }
            set
            {
                if (_selectedView != value)
                {
                    _selectedView = value;
                    OnPropertyChange();
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

            _languageInfoView = new LanguageInfoView(trainItService, dialogCoordinator);
            _sectionInfoView = new SectionInfoView(trainItService, dialogCoordinator);
            _unitInfoView = new UnitInfoView(trainItService, dialogCoordinator);

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
            if (o.GetType() == typeof(Language))
            {
                SelectedView = _languageInfoView;
                _languageInfoView._languageInfoViewModel.SelectedLanguage = (Language)o;
            }
            else if (o.GetType() == typeof(Section))
            {
                SelectedView = _sectionInfoView;
            }
            else
            {
                SelectedView = _unitInfoView;
            }
        }

        private void TestObjects()
        {
            double grade = 1;

            for (int i = 0; i < 5; i++)
            {
                var language = new Language(Guid.NewGuid(), 1, "Language " + i, "path", DateTime.Now, DateTime.Now, DateTime.Now, true);
                _trainItService.SetLanguage(language);

                var section = new Section(Guid.NewGuid(), language.Id, 1, "Section", DateTime.Now, DateTime.Now, DateTime.Now, true);
                _trainItService.SetSection(section);

                for (int k = 0; k < 80; k++)
                {
                    _trainItService.SetUnit(new Unit(Guid.NewGuid(), "unit " + k, grade, section.Id, DateTime.Now, DateTime.Now,
                        DateTime.Now, true));

                    grade += 0.05;
                }

                grade = 1;
            }
        }
        #endregion
    }
}

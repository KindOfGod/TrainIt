using MahApps.Metro.Controls.Dialogs;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Xml.Linq;
using TrainIt.Classes;
using TrainIt.Helper;
using TrainIt.Model;
using TrainIt.View.Edit;
using TrainIt.ViewModel.EditModels;

namespace TrainIt.ViewModel
{
    public class EditViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;

        private readonly LanguageInfoViewModel _languageInfoViewModel;
        private readonly SectionInfoViewModel _sectionInfoViewModel;
        private readonly UnitInfoViewModel _unitInfoViewModel;

        private BaseViewModel _selectedView;
        private ObservableCollection<Language> _languageList;
        private object _selectedItem;
        #endregion

        #region Properties

        public ObservableCollection<Language> LanguageList
        {
            get => _languageList;
            private set
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
            get => _selectedItem;
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

        public BaseViewModel SelectedView
        {
            get => _selectedView;
            private set
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

            _languageInfoViewModel = new LanguageInfoViewModel(trainItService, dialogCoordinator);
            _sectionInfoViewModel = new SectionInfoViewModel(trainItService, dialogCoordinator);
            _unitInfoViewModel = new UnitInfoViewModel(trainItService, dialogCoordinator);

            //var tsk = TestObjects();
            var task = OnLoad();
        }
        #endregion

        #region Commands

        public ICommand DeleteLanguageCommand => new RelayCommand(p => OnDeleteLanguageCommand());

        public ICommand DeleteSectionCommand => new RelayCommand(p => OnDeleteSectionCommand());

        public ICommand DeleteUnitCommand => new RelayCommand(p => OnDeleteUnitCommand());

        #endregion

        #region General Methods
        private async Task OnLoad()
        {
            LanguageList = new ObservableCollection<Language>(await _trainItService.GetLanguages());

            foreach (var language in LanguageList)
            {
                language.Sections = new ObservableCollection<Section>(await _trainItService.GetSections(language));

                foreach (var section in language.Sections)
                {
                    section.Units = new ObservableCollection<Unit>(await _trainItService.GetUnits(section));
                }
            }
        }

        private void OnSelectionChanged(object o)
        {
            if (o == null)
                return;

            if (o.GetType() == typeof(Language))
            {
                SelectedView = _languageInfoViewModel;
                _languageInfoViewModel.SelectedLanguage = (Language)o;
            }
            else if (o.GetType() == typeof(Section))
            {
                SelectedView = _sectionInfoViewModel;
                _sectionInfoViewModel.SelectedSection = (Section)o;
            }
            else
            {
                SelectedView = _unitInfoViewModel;
                _unitInfoViewModel.SelectedUnit = (Unit)o;
            }
        }

        public T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
        {
            var returnVal = source;

            while (returnVal != null && !(returnVal is T))
            {
                DependencyObject tempReturnVal = null;
                if (returnVal is Visual or Visual3D)
                {
                    tempReturnVal = VisualTreeHelper.GetParent(returnVal);
                }
                returnVal = tempReturnVal ?? LogicalTreeHelper.GetParent(returnVal);
            }

            return returnVal as T;
        }
        #endregion

        #region Creation Methods

        #endregion

        #region Delete Methods

        private async void OnDeleteLanguageCommand()
        {
            var items = new ObservableCollection<Language>();
            var item = (Language) _selectedItem;

            items.Add((Language)_selectedItem);

            await _trainItService.DeleteLanguages(items);
            await _trainItService.DeleteSections(item.Sections);

            foreach (var section in item.Sections)
            {
                await _trainItService.DeleteUnits(section.Units);

                foreach (var unit in section.Units) 
                    await _trainItService.DeleteWords(unit.Words);
            }

            LanguageList.Remove(item);
        }

        private async void OnDeleteSectionCommand()
        {
            var items = new ObservableCollection<Section>();
            var item = (Section)_selectedItem;

            items.Add((Section)_selectedItem);

            await _trainItService.DeleteSections(items);
            await _trainItService.DeleteUnits(item.Units);

            foreach (var unit in item.Units)
            {
                await _trainItService.DeleteWords(unit.Words);
            }

            LanguageList.FirstOrDefault(x => x.Sections.Contains(item))?.Sections.Remove(item);
        }

        private async void OnDeleteUnitCommand()
        {
            var items = new ObservableCollection<Unit>();
            var item = (Unit)_selectedItem;

            items.Add((Unit)_selectedItem);

            await _trainItService.DeleteUnits(items);
            await _trainItService.DeleteWords(item.Words);

            LanguageList.FirstOrDefault(x => x.Sections.ToList().Exists(y => y.Units.Contains(item)))
                ?.Sections.FirstOrDefault(x => x.Units.Contains(item))
                ?.Units.Remove(item);
        }

        #endregion

        // Todo: Delete Temporary Test
        #region Test Methods

        private async Task TestObjects()
        {
            double grade = 1;

            for (var i = 0; i < 5; i++)
            {
                var language = new Language(Guid.NewGuid(), 1, "Language " + i, @"..\Resources\Flags\DE@3x.png", DateTime.Now, DateTime.Now, DateTime.Now, true);
                await _trainItService.SetLanguage(language);

                var section = new Section(Guid.NewGuid(), language.Id, 1, "Section", DateTime.Now, DateTime.Now, DateTime.Now, true);
                await _trainItService.SetSection(section);

                for (var k = 0; k < 80; k++)
                {
                    await _trainItService.SetUnit(new Unit(Guid.NewGuid(), section.Id, grade, "unit " + k, DateTime.Now, DateTime.Now,
                        DateTime.Now, true));

                    grade += 0.05;
                }

                grade = 1;
            }
        }

        #endregion
    }
}

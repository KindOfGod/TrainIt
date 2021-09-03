using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using MaterialDesignThemes.Wpf;
using TrainIt.Classes;
using TrainIt.Helper;
using TrainIt.Model;
using TrainIt.ViewModel.DialogViewModels;
using TrainIt.ViewModel.EditModels;
using TrainIt.ViewModel.EditModels.UnitEditorModels;
using Section = TrainIt.Classes.Section;

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

        private BaseViewModel _selectedInfoView;
        private BaseViewModel _selectedView;
        private ObservableCollection<Language> _languageList;
        private Visibility _isAnythingSelected = Visibility.Collapsed;
        private Visibility _isUnitSelected = Visibility.Collapsed;
        private object _selectedItem;
        private string _searchText;

        #endregion

        #region Properties

        public ObservableCollection<Language> LanguageList
        {
            get => _languageList;
            set
            {
                if (_languageList == value) 
                    return;

                _languageList = value;
                OnPropertyChange();
            }
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) 
                    return;

                _selectedItem = value;
                OnPropertyChange();
                OnSelectionChanged(_selectedItem);
            }
        }

        public Visibility IsAnythingSelected
        {
            get => _isAnythingSelected;
            set
            {
                if (_isAnythingSelected == value) 
                    return;

                _isAnythingSelected = value;
                OnPropertyChange();
            }
        }

        public Visibility IsUnitSelected
        {
            get => _isUnitSelected;
            set
            {
                if (_isUnitSelected == value)
                    return;

                _isUnitSelected = value;
                OnPropertyChange();
            }
        }

        public BaseViewModel SelectedInfoView
        {
            get => _selectedInfoView;
            private set
            {
                if (_selectedInfoView == value) 
                    return;

                _selectedInfoView = value;
                OnPropertyChange();
            }
        }

        public BaseViewModel SelectedView
        {
            get => _selectedView;
            set
            {
                if (_selectedView == value)
                    return;

                _selectedView = value;
                OnPropertyChange();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;
                OnSearchTextChanged();
                OnPropertyChange();
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
            
            OnLoad();
            SelectedView = null;
        }
        #endregion

        #region Commands

        public ICommand DeleteButtonCommand => new RelayCommand(p => OnDelete());
        public ICommand CreateButtonCommand => new RelayCommand(p => OnCreate());
        public ICommand CreateLanguageCommand => new RelayCommand(p => OnCreateLanguage());
        public ICommand EditUnitCommand => new RelayCommand(p => OnEditUnitCommand());

        #endregion

        #region Public Methods

        public void OnEditUnitCommand()
        {
            SelectedView = new UnitEditorViewModel(_trainItService, _dialogCoordinator, (Unit)SelectedItem, this);
        }

        #endregion

        #region Private Methods
        private async void OnLoad()
        {
            LanguageList = new ObservableCollection<Language>(await DatabaseService.GetLanguages());

            foreach (var language in LanguageList)
            {
                language.Sections = new ObservableCollection<Section>(await DatabaseService.GetSections(language));

                foreach (var section in language.Sections)
                {
                    section.Units = new ObservableCollection<Unit>(await DatabaseService.GetUnits(section));
                }
            }

            if (LanguageList.Count != 0)
                LanguageList[0].IsExpanded = true;
        }

        private void OnSelectionChanged(object o)
        {
            if (o == null)
                return;

            IsAnythingSelected = Visibility.Visible;
            IsUnitSelected = Visibility.Collapsed;

            if (o.GetType() == typeof(Language))
            {
                SelectedInfoView = _languageInfoViewModel;
                _languageInfoViewModel.SelectedLanguage = (Language)o;
            }
            else if (o.GetType() == typeof(Section))
            {
                SelectedInfoView = _sectionInfoViewModel;
                _sectionInfoViewModel.SelectedSection = (Section)o;
            }
            else
            {
                SelectedInfoView = _unitInfoViewModel;
                _unitInfoViewModel.SelectedUnit = (Unit)o;
                IsUnitSelected = Visibility.Visible;
            }
        }

        private void OnSearchTextChanged()
        {
            foreach (var l in LanguageList)
            {
                foreach (var s in l.Sections)
                {
                    foreach (var u in s.Units) 
                        u.IsVisible = u.Name.Contains(SearchText) ? Visibility.Visible : Visibility.Collapsed;

                    s.IsVisible = (s.Name.ToLower().Contains(SearchText.ToLower()) || s.Units.Any(u => u.IsVisible == Visibility.Visible)) ? Visibility.Visible : Visibility.Collapsed;
                }
                l.IsVisible = (l.Name.ToLower().Contains(SearchText.ToLower()) || l.Sections.Any(u => u.IsVisible == Visibility.Visible)) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public static T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
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
        private async void OnCreate()
        {
            if (SelectedItem.GetType() == typeof(Section))
                await CreateUnit((Section)SelectedItem);
            else if (SelectedItem.GetType() == typeof(Language))
                await CreateSection((Language)SelectedItem);
        }

        private static async Task CreateUnit(Section section)
        {
            var vm = new CreationDialogViewModel(2);
            var result = await DialogService.OpenDialog(vm);

            if(result == null || result.Equals(false))
                return;

            var unit = new Unit(
                Guid.NewGuid(),
                section.Id,
                3,
                vm.Name,
                DateTime.Now,
                DateTime.Now,
                true
                );
            await DatabaseService.SetUnit(unit);
            section.Units.Add(unit);
        }

        private static async Task CreateSection(Language language)
        {
            var vm = new CreationDialogViewModel(1);
            var result = await DialogService.OpenDialog(vm);

            if(result == null || result.Equals(false))
                return;

            var sec = new Section(
                Guid.NewGuid(),
                language.Id,
                3,
                vm.Name,
                DateTime.Now,
                DateTime.Now,
                true
                );
            await DatabaseService.SetSection(sec);
            language.Sections.Add(sec);
        }

        private async void OnCreateLanguage()
        {
            var vm = new CreationDialogViewModel(0);
            var result = await DialogService.OpenDialog(vm);

            if(result == null || result.Equals(false))
                return;

            var lan = new Language(
                Guid.NewGuid(),
                3,
                vm.Name,
                vm.SelectedIcon,
                DateTime.Now, 
                DateTime.Now,
                true
            );

            await DatabaseService.SetLanguage(lan);
            _languageList.Add(lan);
        }

        #endregion

        #region Delete Methods

        private async void OnDelete()
        {
            if (SelectedItem == null) 
                return;

            if (SelectedItem.GetType() == typeof(Language))
            {
                await OnDeleteLanguageCommand();
            }
            else if (SelectedItem.GetType() == typeof(Section))
            {
                await OnDeleteSectionCommand();
            }
            else
            {
                await OnDeleteUnitCommand();
            }
        }

        private async Task OnDeleteLanguageCommand()
        {
            var items = new ObservableCollection<Language>();
            var item = (Language) _selectedItem;

            items.Add((Language)_selectedItem);

            await DatabaseService.DeleteLanguages(items);
            
            if (item.Sections != null)
            {
                await DatabaseService.DeleteSections(item.Sections);

                foreach (var section in item.Sections)
                {
                    await DatabaseService.DeleteUnits(section.Units);

                    foreach (var unit in section.Units) 
                        await DatabaseService.DeleteWords(unit.Words);
                }
            }

            LanguageList.Remove(item);
        }

        private async Task OnDeleteSectionCommand()
        {
            var items = new ObservableCollection<Section>();
            var item = (Section)_selectedItem;

            items.Add((Section)_selectedItem);

            await DatabaseService.DeleteSections(items);
            
            if (item.Units != null)
            {
                await DatabaseService.DeleteUnits(item.Units);

                foreach (var unit in item.Units)
                {
                    await DatabaseService.DeleteWords(unit.Words);
                }
            }

            LanguageList.FirstOrDefault(x => x.Sections.Contains(item))?.Sections.Remove(item);
        }

        private async Task OnDeleteUnitCommand()
        {
            var items = new ObservableCollection<Unit>();
            var item = (Unit)_selectedItem;

            items.Add((Unit)_selectedItem);

            await DatabaseService.DeleteUnits(items);

            if(item.Words != null)
                await DatabaseService.DeleteWords(item.Words);

            LanguageList.FirstOrDefault(x => x.Sections.ToList().Exists(y => y.Units.Contains(item)))
                ?.Sections.FirstOrDefault(x => x.Units.Contains(item))
                ?.Units.Remove(item);
        }

        #endregion
    }
}

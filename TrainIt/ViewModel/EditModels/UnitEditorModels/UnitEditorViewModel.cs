using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Classes;
using TrainIt.Helper;
using TrainIt.Model;

namespace TrainIt.ViewModel.EditModels.UnitEditorModels
{
    public class UnitEditorViewModel : BaseViewModel
    {
        #region Fields
        private readonly TrainItService _trainItService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly Unit _selectedUnit;
        private readonly EditViewModel _parentModel;
        private Word _selectedWord;
        private ObservableCollection<Word> _words;
        #endregion

        #region Properties
        public Word SelectedWord
        {
            get => _selectedWord;
            set
            {
                if (_selectedWord == value)
                    return;

                _selectedWord = value;
                OnFocusedItemChanged();

                OnPropertyChange();
            }
        }

        public ObservableCollection<Word> Words
        {
            get => _words;
            set
            {
                if (_words == value)
                    return;

                _words = value;
                OnPropertyChange();
            }
        }
        #endregion

        #region Constructors
        public UnitEditorViewModel() {}

        public UnitEditorViewModel(TrainItService trainItService, IDialogCoordinator dialogCoordinator, Unit selectedUnit, EditViewModel parentModel)
        {
            _trainItService = trainItService;
            _dialogCoordinator = dialogCoordinator;
            _selectedUnit = selectedUnit;
            _parentModel = parentModel;

            OnLoad(); 
        }
        #endregion

        #region Commands

        public ICommand ReturnButtonCommand => new RelayCommand(p => OnReturnButtonCommand());

        #endregion

        #region Public Methods

        public async void SaveChanges()
        {
            UpdateOrder();

            foreach (var word in Words)
            {
                if (word.IsEdited)
                {
                    await _trainItService.SetWord(word);
                    word.IsEdited = false;
                }
            }
        }
        #endregion

        #region Privat Methods
        private async void OnLoad()
        {
            var words = new ObservableCollection<Word>(await _trainItService.GetWords(_selectedUnit));
            UnitHelper.SortUnit(ref words);

            Words = words;

            if (Words.Count > 0)
                SelectedWord = Words[0];
        }

        private void OnFocusedItemChanged()
        {
            if (SelectedWord == null)
                return;

            SelectedWord.IsEdited = true;

            SelectedWord.PrimaryLanguage = SelectedWord.PrimaryLanguage.Trim();
            SelectedWord.SecondaryLanguage = SelectedWord.SecondaryLanguage.Trim();
            SelectedWord.Synonym = SelectedWord.Synonym.Trim();
            SelectedWord.Comment = SelectedWord.Comment.Trim();

            SaveChanges();
        }

        private void OnReturnButtonCommand()
        {
            OnFocusedItemChanged();
            _parentModel.SelectedView = null;
        }

        private void UpdateOrder()
        {
            var currentId = Guid.Empty;

            foreach (var word in Words)
            {
                if (word.PreviousWordId != currentId)
                {
                    word.PreviousWordId = currentId;
                    word.IsEdited = true;
                }

                currentId = word.Id;
            }
        }
        #endregion
    }
}

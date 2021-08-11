using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrainIt.Classes
{
    public class Word : VocabularyBaseClass
    {
        #region Fields
        private string _primaryLanguage;
        private string _secondaryLanguage;
        #endregion

        #region Properties

        public Guid UnitId { get; set; }
        public Guid PreviousWordId { get; set; }

        public string PrimaryLanguage { 
            get => _primaryLanguage;
            set
            {
                if (_primaryLanguage == value)
                    return;

                _primaryLanguage = value;
                OnPropertyChange();
            } }
        public string SecondaryLanguage 
        {
            get => _secondaryLanguage;
            set
            {
                if (_secondaryLanguage == value)
                    return;

                _secondaryLanguage = value;
                OnPropertyChange();
            }
        }
    
        public string Comment { get; set; }
        public string Synonym { get; set; }

        public bool IsEdited { get; set; }
        #endregion

        #region Constructors

        public Word(Guid id, double grade, Guid unitId, string primaryLanguage, string secondaryLanguage, string comment, string synonym, DateTime created, DateTime edited, DateTime lastLearned, bool isNew)
        {
            Id = id;
            Grade = grade;
            UnitId = unitId;

            PrimaryLanguage = primaryLanguage;
            SecondaryLanguage = secondaryLanguage;
            Comment = comment;
            Synonym = synonym;

            PreviousWordId = Guid.Empty;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
            IsNew = isNew;
            IsEdited = false;
            IsVisible = Visibility.Visible;
        }

        #endregion
    }
}

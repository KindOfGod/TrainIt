using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrainIt.Classes
{ 
    public class Section : VocabularyBaseClass
    {
        #region Properties
        public Guid LanguageId { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Unit>Units { get; set; }
        #endregion

        #region Constructors
        public Section(Guid id, Guid languageId, double grade, string name, DateTime created, DateTime edited, bool isNew)
        {
            Id = id;
            LanguageId = languageId;
            Grade = grade;
            Name = name;

            Created = created;
            Edited = edited;

            IsNew = isNew;
            IsExpanded = false;
            IsVisible = Visibility.Visible;
        }

        public Section(Guid id, Guid languageId, double grade, string name, DateTime created, DateTime edited, DateTime lastLearned, bool isNew)
        {
            Id = id;
            LanguageId = languageId;
            Grade = grade;
            Name = name;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;

            IsNew = isNew;
            IsExpanded = false;
            IsVisible = Visibility.Visible;
        }
        #endregion
    }
}

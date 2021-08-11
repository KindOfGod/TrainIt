using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrainIt.Classes
{
    public class Unit : VocabularyBaseClass
    {
        #region Properties
        public Guid SectionId { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Word> Words { get; set; }
        #endregion

        #region Constructors

        public Unit(Guid id, Guid sectionId, double grade, string name, DateTime created, DateTime edited, bool isNew)
        {
            Id = id;
            Name = name;
            Grade = grade;
            SectionId = sectionId;

            Created = created;
            Edited = edited;

            IsNew = isNew;
            IsExpanded = false;
            IsVisible = Visibility.Visible;
        }

        public Unit(Guid id, Guid sectionId, double grade, string name, DateTime created, DateTime edited, DateTime lastLearned, bool isNew)
        {
            Id = id;
            Name = name;
            Grade = grade;
            SectionId = sectionId;

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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrainIt.Annotations;
using TrainIt.Classes;

namespace TrainIt.Classes
{
    public class Language : VocabularyBaseClass
    {
        #region Properties

        public string Name { get; set; }
        public string FlagIconPath { get; set; }

        public ObservableCollection<Section> Sections { get; set; }

        #endregion

        #region Constructors
        public Language(Guid id, double grade, string name, string flagIconPath, DateTime created, DateTime edited, bool isNew)
        {
            Id = id;
            Grade = grade;
            Name = name;
            FlagIconPath = flagIconPath;

            Created = created;
            Edited = edited;

            IsNew = isNew;
            IsExpanded = false;
            IsVisible = Visibility.Visible;
        }

        public Language(Guid id, double grade, string name, string flagIconPath, DateTime created, DateTime edited, DateTime lastLearned, bool isNew)
        {
            Id = id;
            Grade = grade;
            Name = name;
            FlagIconPath = flagIconPath;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;

            IsNew = isNew;
            IsVisible = Visibility.Visible;
        }
        #endregion
    }
}

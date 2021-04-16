using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;

namespace TrainIt.Classes
{
    public class Language : VocabularyBaseClass
    {
        #region Properties

        public string Name { get; set; }
        public string FlagIconPath { get; set; }

        public List<Section> Sections { get; set; }

        #endregion

        #region Constructors
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
            IsExpanded = true;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{ 
    public class Section : VocabularyBaseClass
    {
        #region Properties
        public Guid LanguageId { get; set; }
        public string Name { get; set; }

        public List<Unit>Units { get; set; }
        #endregion

        #region Constructors
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
        }
        #endregion
    }
}

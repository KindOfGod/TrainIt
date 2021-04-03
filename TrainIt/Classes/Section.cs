using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{ 
    public class Section
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }

        public double Grade { get; set; }
        
        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public List<Unit>Units { get; set; }

        public bool IsNew { get; set; }

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
        }
        #endregion
    }
}

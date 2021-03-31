using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    class Section
    {
        #region Properties

        public int Id { get; set; }
        public int LanguageId { get; set; }

        public float Grade { get; set; }
        
        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public List<Unit>Units { get; set; }

        #endregion

        #region Constructors
        public Section(int id, int languageId, float grade, string name, DateTime created, DateTime edited, DateTime lastLearned)
        {
            Id = id;
            LanguageId = languageId;
            Grade = grade;
            Name = name;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
        }
        #endregion
    }
}

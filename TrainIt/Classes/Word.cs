using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    class Word
    {
        #region Properties

        public int Id { get; set; }
        public int Grade { get; set; }
        public int UnitId { get; set; }

        public string PrimaryLanguage { get; set; }
        public string SecondaryLanguage { get; set; }
        public string Comment { get; set; }
        public string Synonym { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }
        #endregion

        #region Constructors

        public Word(int id, int grade, int unitId, string primaryLanguage, string secondaryLanguage, string comment, string synonym, DateTime created, DateTime edited, DateTime lastLearned)
        {
            Id = id;
            Grade = grade;
            UnitId = unitId;

            PrimaryLanguage = primaryLanguage;
            SecondaryLanguage = secondaryLanguage;
            Comment = comment;
            Synonym = synonym;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
        }

        #endregion
    }
}

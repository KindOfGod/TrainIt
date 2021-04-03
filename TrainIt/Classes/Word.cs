using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    public class Word
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid UnitId { get; set; }

        public double Grade { get; set; }

        public string PrimaryLanguage { get; set; }
        public string SecondaryLanguage { get; set; }
        public string Comment { get; set; }
        public string Synonym { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public bool IsNew { get; set; }
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

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
            IsNew = isNew;
        }

        #endregion
    }
}

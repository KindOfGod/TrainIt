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

        public int Id { get; set; }
        public int UnitId { get; set; }

        public double Grade { get; set; }

        public string PrimaryLanguage { get; set; }
        public string SecondaryLanguage { get; set; }
        public string Comment { get; set; }
        public string Synonym { get; set; }

        public string Created { get; set; }
        public string Edited { get; set; }
        public string LastLearned { get; set; }
        #endregion

        #region Constructors

        public Word(int id, double grade, int unitId, string primaryLanguage, string secondaryLanguage, string comment, string synonym, string created, string edited, string lastLearned)
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

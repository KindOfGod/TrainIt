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

        public int Id { get; set; }
        public int LanguageId { get; set; }

        public double Grade { get; set; }
        
        public string Name { get; set; }

        public string Created { get; set; }
        public string Edited { get; set; }
        public string LastLearned { get; set; }

        public List<Unit>Units { get; set; }

        #endregion

        #region Constructors
        public Section(int id, int languageId, double grade, string name, string created, string edited, string lastLearned)
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

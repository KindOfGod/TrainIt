using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    public class Unit
    {
        #region Properties
        public int Id { get; set; }
        public int SectionId { get; set; }

        public double Grade { get; set; }

        public string Name { get; set; }

        public string Created { get; set; }
        public string Edited { get; set; }
        public string LastLearned { get; set; }

        public List<Word> Words { get; set; }
        #endregion

        #region Constructors

        public Unit(int id, string name, double grade, int sectionId, string created, string edited, string lastLearned)
        {
            Id = id;
            Name = name;
            Grade = grade;
            SectionId = sectionId;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
        }

        #endregion
    }
}

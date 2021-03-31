using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    class Unit
    {
        #region Properties
        public int Id { get; set; }
        public int SectionId { get; set; }

        public float Grade { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public List<Word> Words { get; set; }
        #endregion

        #region Constructors

        public Unit(int id, string name, float grade, int sectionId, DateTime created, DateTime edited, DateTime lastLearned)
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

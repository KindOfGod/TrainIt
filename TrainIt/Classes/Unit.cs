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
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }

        public double Grade { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public List<Word> Words { get; set; }

        public bool IsNew { get; set; }
        #endregion

        #region Constructors

        public Unit(Guid id, string name, double grade, Guid sectionId, DateTime created, DateTime edited, DateTime lastLearned, bool isNew)
        {
            Id = id;
            Name = name;
            Grade = grade;
            SectionId = sectionId;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;

            IsNew = isNew;
        }

        #endregion
    }
}

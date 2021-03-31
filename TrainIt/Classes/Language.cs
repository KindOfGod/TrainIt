using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;

namespace TrainIt.Classes
{
    public class Language
    {
        #region Properties

        public int Id { get; set; }
        public double Grade { get; set; }

        public string Name { get; set; }
        public string FlagIconPath { get; set; }

        public string Created { get; set; }
        public string Edited { get; set; }
        public string LastLearned { get; set; }

        public List<Section> Sections { get; set; }

        #endregion

        #region Constructors
        public Language(int id, double grade, string name, string flagIconPath, string created, string edited, string lastLearned)
        {
            Id = id;
            Grade = grade;
            Name = name;
            FlagIconPath = flagIconPath;

            Created = created;
            Edited = edited;
            LastLearned = lastLearned;
        }
        #endregion
    }
}

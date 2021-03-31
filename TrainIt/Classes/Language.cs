﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;

namespace TrainIt.Classes
{
    class Language
    {
        #region Properties

        public int Id { get; set; }
        public float Grade { get; set; }

        public string Name { get; set; }
        public string FlagIconPath { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public List<Section> Sections { get; set; }

        #endregion

        #region Constructors
        public Language(int id,  float grade, string name, string flagIconPath, DateTime created, DateTime edited, DateTime lastLearned)
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
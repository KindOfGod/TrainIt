using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Classes
{
    public class VocabularyBaseClass
    {
        public Guid Id { get; set; }

        public double Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public bool IsNew { get; set; }
        public bool IsExpanded { get; set; }
    }
}

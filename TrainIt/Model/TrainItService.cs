using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainIt.Model
{
    public class TrainItService
    {
        public DatabaseService _databaseService;

        #region Constructors
        public TrainItService()
        {
            _databaseService = new DatabaseService();
        }
        #endregion
    }
}

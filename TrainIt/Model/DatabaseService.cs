using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using TrainIt.Classes;

namespace TrainIt.Model
{
    public class DatabaseService
    {
        private Database myDatabase;

        #region Constructors
        public DatabaseService()
        {
            myDatabase = new Database();
        }
        #endregion

        #region Methods
        #endregion
    }
}

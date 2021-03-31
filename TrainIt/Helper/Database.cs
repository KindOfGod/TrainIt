using System.Data.SQLite;
using System.IO;
using Accessibility;

namespace TrainIt.Helper
{
    internal class Database
    {
        #region Fields

        public SQLiteConnection _myConnection;

        #endregion

        #region Constructors

        public Database()
        {
            var ex = CheckForDatabase();

            _myConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3;");

            if (!ex)
                CreateDatabase();
        }

        #endregion

        private bool CheckForDatabase()
        {
            if (File.Exists("./database.sqlite"))
                return true;

            SQLiteConnection.CreateFile("database.sqlite");
            return false;
        }

        private void CreateDatabase()
        {
            using (var cmd = new SQLiteCommand(_myConnection))
            {
                _myConnection.Open();

                cmd.CommandText = @"CREATE TABLE languages(Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Name TEXT NOT NULL, Grade REAL NOT NULL, "
                                  + "FlagIconPath TEXT NOT NULL, LastLearned TEXT NOT NULL, Edited TEXT NOT NULL, Created TEXT NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE sections(Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Name TEXT NOT NULL, LanguageId INTEGER NOT NULL, " 
                                  + "Grade REAL NOT NULL, LastLearned TEXT NOT NULL, Edited TEXT NOT NULL, Created TEXT NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE units(Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, Name TEXT NOT NULL, SectionId INTEGER NOT NULL, " 
                                  +"Grade REAL NOT NULL, LastLearned TEXT NOT NULL, Edited TEXT NOT NULL, Created TEXT NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE words(Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL, pLanguage TEXT NOT NULL, sLanguage TEXT NOT NULL, " 
                                  +"Comment TEXT NOT NULL, Synonym TEXT NOT NULL, UnitId INTEGER NOT NULL, Grade REAL NOT NULL, LastLearned TEXT NOT NULL, " 
                                  +"Edited TEXT NOT NULL, Created TEXT NOT NULL)";
                cmd.ExecuteNonQuery();

                _myConnection.Close();
            }
        }

        public void OpenConnection()
        {
            if (_myConnection.State != System.Data.ConnectionState.Open)
            {
                _myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if(_myConnection.State != System.Data.ConnectionState.Closed)
            {
                _myConnection.Close();
            }
        }
    }
}
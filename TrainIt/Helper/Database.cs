using System;
using System.Data.SQLite;
using System.IO;
using Accessibility;

namespace TrainIt.Helper
{
    internal class Database : IDisposable
    {
        #region Fields

        public SQLiteConnection _myConnection;

        #endregion

        #region Constructors
        public Database()
        {
            var ex = CheckForDatabase();

            _myConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3;");

            OpenConnection();

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
                cmd.CommandText = @"CREATE TABLE languages(Id GUID PRIMARY KEY, Name TEXT NOT NULL, Grade REAL NOT NULL, "
                                  + "FlagIconPath TEXT NOT NULL, LastLearned DATETIME NOT NULL, Edited DATETIME NOT NULL, Created DATETIME NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE sections(Id GUID PRIMARY KEY, Name TEXT NOT NULL, LanguageId GUID NOT NULL, "
                                  + "Grade REAL NOT NULL, LastLearned DATETIME NOT NULL, Edited DATETIME NOT NULL, Created DATETIME NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE units(Id GUID PRIMARY KEY, Name TEXT NOT NULL, SectionId GUID NOT NULL, "
                                  + "Grade REAL NOT NULL, LastLearned DATETIME NOT NULL, Edited DATETIME NOT NULL, Created DATETIME NOT NULL)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE words(Id GUID PRIMARY KEY, pLanguage TEXT NOT NULL, sLanguage TEXT NOT NULL, "
                                  + "Comment TEXT NOT NULL, Synonym TEXT NOT NULL, UnitId GUID NOT NULL, Grade REAL NOT NULL, LastLearned DATETIME NOT NULL, "
                                  + "Edited DATETIME NOT NULL, Created DATETIME NOT NULL)";
                cmd.ExecuteNonQuery();
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

        public void Dispose()
        {
            _myConnection.Dispose();
        }
    }
}
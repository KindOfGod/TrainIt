using System.Data.SQLite;
using System.IO;

namespace TrainIt.Helper
{
    internal class Database
    {
        #region Fields

        private readonly SQLiteConnection _myConnection;

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

                cmd.CommandText = @"CREATE TABLE languages(Id INTEGER PRIMARY KEY, Name TEXT, Grade REAL, FlagIconPATH TEXT, LastLearned TEXT, Edited TEXT, Created TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE sections(Id INTEGER PRIMARY KEY, Name TEXT, LanguageId INTEGER, Grade REAL, LastLearned TEXT, Edited TEXT, Created TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE units(Id INTEGER PRIMARY KEY, Name TEXT, SectionId INTEGER, Grade REAL, LastLearned TEXT, Edited TEXT, Created TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE words(Id INTEGER PRIMARY KEY, pLanguage TEXT, sLanguage TEXT, Comment TEXT, Synonym TEXT, UnitId INTEGER, Grade REAL, LastLearned TEXT, Edited TEXT, Created TEXT)";
                cmd.ExecuteNonQuery();

                _myConnection.Close();
            }
        }
    }
}
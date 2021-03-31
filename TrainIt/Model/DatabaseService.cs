using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Accessibility;
using TrainIt.Classes;
using TrainIt.Helper;

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

        #region Read Methods
        public List<Language> GetLanguages()
        {
            var languages = new List<Language>();
            myDatabase.OpenConnection();

            using (var cmd = new SQLiteCommand(myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Grade, Name, FlagIconPath, Created, Edited, LastLearned FROM languages";
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var language = new Language(
                        Convert.ToInt32(r["Id"]),
                        Convert.ToDouble(r["Grade"]),
                        Convert.ToString(r["Name"]),
                        Convert.ToString(r["FlagIconPath"]),
                        Convert.ToString(r["Created"]),
                        Convert.ToString(r["Edited"]),
                        Convert.ToString(r["LastLearned"])
                    );

                    languages.Add(language);
                }
            }

            myDatabase.CloseConnection();

            return languages;
        }

        public List<Section> GetSectionsFromLanguage(Language language)
        {
            var sections = new List<Section>();
            myDatabase.OpenConnection();

            using (var cmd = new SQLiteCommand(myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, LanguageID, Grade, Name, Created, Edited, LastLearned FROM sections WHERE LanguageID like @id";
                cmd.Parameters.AddWithValue("@id", language.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var section = new Section(
                        Convert.ToInt32(r["Id"]),
                        Convert.ToInt32(r["LanguageID"]),
                        Convert.ToDouble(r["Grade"]),
                        Convert.ToString(r["Name"]),
                        Convert.ToString(r["Created"]),
                        Convert.ToString(r["Edited"]),
                        Convert.ToString(r["LastLearned"])
                    );

                    sections.Add(section);
                }
            }

            myDatabase.CloseConnection();

            return sections;
        }

        public List<Unit> GetUnitsFromSection(Section section)
        {
            var units = new List<Unit>();
            myDatabase.OpenConnection();

            using (var cmd = new SQLiteCommand(myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Name, Grade, SectionID, Created, Edited, LastLearned FROM units WHERE SectionID like @id";
                cmd.Parameters.AddWithValue("@id", section.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var unit = new Unit(
                        Convert.ToInt32(r["Id"]),
                        Convert.ToString(r["Name"]),
                        Convert.ToDouble(r["Grade"]),
                        Convert.ToInt32(r["SectionID"]),
                        Convert.ToString(r["Created"]),
                        Convert.ToString(r["Edited"]),
                        Convert.ToString(r["LastLearned"])
                    );

                    units.Add(unit);
                }
            }

            myDatabase.CloseConnection();

            return units;
        }

        public List<Word> GetWordsFromUnit(Unit unit)
        {
            var words = new List<Word>();
            
            myDatabase.OpenConnection();

            using (var cmd = new SQLiteCommand(myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Grade, UnitId, pLanguage, sLanguage, Comment, Synonym, Created, Edited, LastLearned FROM words WHERE UnitId like @id";
                cmd.Parameters.AddWithValue("@id", unit.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var word = new Word(
                        Convert.ToInt32(r["Id"]),
                        Convert.ToDouble(r["Grade"]),
                        Convert.ToInt32(r["UnitId"]),
                        Convert.ToString(r["pLanguage"]),
                        Convert.ToString(r["sLanguage"]),
                        Convert.ToString(r["Comment"]),
                        Convert.ToString(r["Synonym"]),
                        Convert.ToString(r["Created"]),
                        Convert.ToString(r["Edited"]),
                        Convert.ToString(r["LastLearned"])
                        );

                    words.Add(word);
                }
            }

            myDatabase.CloseConnection();

            return words;
        }
        #endregion
    }
}

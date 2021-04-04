using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Input;
using TrainIt.Classes;
using TrainIt.Helper;

namespace TrainIt.Model
{
    public class DatabaseService
    {
        private readonly Database _myDatabase;

        #region Constructors
        public DatabaseService()
        {
            _myDatabase = new Database();
        }
        #endregion

        #region Read Methods
        public List<Language> GetLanguages()
        {
            var languages = new List<Language>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Grade, Name, FlagIconPath, Created, Edited, LastLearned FROM languages";
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var language = new Language(
                        (Guid)r["Id"],
                        (double)r["Grade"],
                        (string)r["Name"],
                        (string)r["FlagIconPath"],
                        (DateTime)r["Created"],
                        (DateTime)r["Edited"],
                        (DateTime)r["LastLearned"],
                        false
                    );

                    languages.Add(language);
                }
            }

            return languages;
        }

        public List<Section> GetSections(Language language)
        {
            var sections = new List<Section>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, LanguageID, Grade, Name, Created, Edited, LastLearned FROM sections WHERE LanguageID like @id";
                cmd.Parameters.AddWithValue("@id", language.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var section = new Section(
                        (Guid)r["Id"],
                        (Guid)r["LanguageID"],
                        (double)r["Grade"],
                        (string)r["Name"],
                        (DateTime)r["Created"],
                        (DateTime)r["Edited"],
                        (DateTime)r["LastLearned"],
                        false
                    );

                    sections.Add(section);
                }
            }

            return sections;
        }

        public List<Unit> GetUnits(Section section)
        {
            var units = new List<Unit>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Name, Grade, SectionID, Created, Edited, LastLearned FROM units WHERE SectionID like @id";
                cmd.Parameters.AddWithValue("@id", section.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var unit = new Unit(
                        (Guid)r["Id"],
                        (string)r["Name"],
                        (double)r["Grade"],
                        (Guid)r["SectionID"],
                        (DateTime)r["Created"],
                        (DateTime)r["Edited"],
                        (DateTime)r["LastLearned"],
                        false
                    );

                    units.Add(unit);
                }
            }

            return units;
        }

        public List<Word> GetWords(Unit unit)
        {
            var words = new List<Word>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Grade, UnitId, pLanguage, sLanguage, Comment, Synonym, Created, Edited, LastLearned FROM words WHERE UnitId like @id";
                cmd.Parameters.AddWithValue("@id", unit.Id);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    var word = new Word(
                        (Guid)r["Id"],
                        (double)r["Grade"],
                        (Guid)r["UnitId"],
                        (string)r["pLanguage"],
                        (string)r["sLanguage"],
                        (string)r["Comment"],
                        (string)r["Synonym"],
                        (DateTime)r["Created"],
                        (DateTime)r["Edited"],
                        (DateTime)r["LastLearned"],
                        false
                        );

                    words.Add(word);
                }
            }

            return words;
        }
        #endregion

        #region Write Methods

        public void SetLanguage(Language language)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                if (language.IsNew)
                    cmd.CommandText = @"INSERT INTO languages(Id, Grade, Name, FlagIconPath, Created, Edited, LastLearned) VALUES(@Id, @Grade, @Name, @FlagIconPath, @Created, @Edited, @LastLearned)";
                else
                    cmd.CommandText = @"UPDATE languages SET Grade = @Grade, Name = @Name, FlagIconPath = @FlagIconPath, Created = @Created, Edited = @Edited, LastLearned = @LastLearned WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", language.Id);
                cmd.Parameters.AddWithValue("@Grade", language.Grade);
                cmd.Parameters.AddWithValue("@Name", language.Name);
                cmd.Parameters.AddWithValue("@FlagIconPath", language.FlagIconPath);
                cmd.Parameters.AddWithValue("@Created", language.Created);
                cmd.Parameters.AddWithValue("@Edited", language.Edited);
                cmd.Parameters.AddWithValue("@LastLearned", language.LastLearned);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }

        public void SetSection(Section section)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                if (section.IsNew)
                    cmd.CommandText = @"INSERT INTO sections(Id, LanguageId, Grade, Name, Created, Edited, LastLearned) VALUES(@Id, @LanguageId, @Grade, @Name, @Created, @Edited, @LastLearned)";
                else
                    cmd.CommandText = @"UPDATE sections SET LanguageId = @LanguageId, Grade = @Grade, Name = @Name, Created = @Created, Edited = @Edited, LastLearned = @LastLearned WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", section.Id);
                cmd.Parameters.AddWithValue("@LanguageId", section.LanguageId);
                cmd.Parameters.AddWithValue("@Grade", section.Grade);
                cmd.Parameters.AddWithValue("@Name", section.Name);
                cmd.Parameters.AddWithValue("@Created", section.Created);
                cmd.Parameters.AddWithValue("@Edited", section.Edited);
                cmd.Parameters.AddWithValue("@LastLearned", section.LastLearned);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }

        public void SetUnit(Unit unit)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                if (unit.IsNew)
                    cmd.CommandText = @"INSERT INTO units(Id, SectionId, Grade, Name, Created, Edited, LastLearned) VALUES(@Id, @SectionId, @Grade, @Name, @Created, @Edited, @LastLearned)";
                else
                    cmd.CommandText = @"UPDATE units SET SectionId = @SectionId, Grade = @Grade, Name = @Name, Created = @Created, Edited = @Edited, LastLearned = @LastLearned WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", unit.Id);
                cmd.Parameters.AddWithValue("@SectionId", unit.SectionId);
                cmd.Parameters.AddWithValue("@Grade", unit.Grade);
                cmd.Parameters.AddWithValue("@Name", unit.Name);
                cmd.Parameters.AddWithValue("@Created", unit.Created);
                cmd.Parameters.AddWithValue("@Edited", unit.Edited);
                cmd.Parameters.AddWithValue("@LastLearned", unit.LastLearned);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }

        public void SetWord(Word word)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                if (word.IsNew)
                    cmd.CommandText = @"INSERT INTO words(Id, UnitId, Grade, pLanguage, sLanguage, Comment, Synonym, Created, Edited, LastLearned) " +
                                      "VALUES(@Id, @UnitId, @Grade, @pLanguage, @sLanguage, @Comment, @Synonym, @Created, @Edited, @LastLearned)";
                else
                    cmd.CommandText = @"UPDATE words SET UnitId = @UnitId, Grade = @Grade, pLanguage = @pLanguage, sLanguage = @sLanguage " +
                                      "Comment = @Comment, Synonym = @Synonym, Created = @Created, Edited = @Edited, LastLearned = @LastLearned WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", word.Id);
                cmd.Parameters.AddWithValue("@SectionId", word.UnitId);
                cmd.Parameters.AddWithValue("@Grade", word.Grade);
                cmd.Parameters.AddWithValue("@pLanguage", word.PrimaryLanguage);
                cmd.Parameters.AddWithValue("@sLanguage", word.SecondaryLanguage);
                cmd.Parameters.AddWithValue("@Comment", word.Comment);
                cmd.Parameters.AddWithValue("@Synonym", word.Synonym);
                cmd.Parameters.AddWithValue("@Created", word.Created);
                cmd.Parameters.AddWithValue("@Edited", word.Edited);
                cmd.Parameters.AddWithValue("@LastLearned", word.LastLearned);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Delete Methods

        public void DeleteLanguage(Language language)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"DELETE FROM languages WHERE Id = @Id";
                
                cmd.Parameters.AddWithValue("@Id", language.Id);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSection(Section section)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"DELETE FROM sections WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", section.Id);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUnit(Unit unit)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"DELETE FROM units WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", unit.Id);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteWord(Word word)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"DELETE FROM words WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", word.Id);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}

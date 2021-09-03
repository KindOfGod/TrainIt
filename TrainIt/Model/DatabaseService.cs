using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainIt.Classes;
using TrainIt.Helper;

namespace TrainIt.Model
{
    public static class DatabaseService
    {
        #region Fields
        private static readonly Database _myDatabase;
        #endregion
        
        #region Constructors
        static DatabaseService()
        {
            _myDatabase = new Database();
        }
        #endregion

        #region Public Methods

        public static async Task<List<Language>> GetLanguages()
        {
            try
            {
                return await DbGetLanguages();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public static async Task<List<Section>> GetSections(Language language)
        {
            try
            {
                return await DbGetSections(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<List<Unit>> GetUnits(Section section)
        {
            try
            {
                return await DbGetUnits(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<List<Word>> GetWords(Unit unit)
        {
            try
            {
                return await DbGetWords(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task SetLanguage(Language language)
        {
            try
            {
                await DbSetLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task SetSection(Section section)
        {
            try
            {
                await DbSetSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task SetUnit(Unit unit)
        {
            try
            {
                await DbSetUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task SetWord(Word word)
        {
            try
            {
                await DbSetWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task DeleteLanguages(IEnumerable<Language> languages)
        {
            try
            {
                await DbDeleteLanguage(languages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task DeleteSections(IEnumerable<Section> sections)
        {
            try
            {
                await DbDeleteSection(sections);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task DeleteUnits(IEnumerable<Unit> units)
        {
            try
            {
                await DbDeleteUnit(units);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task DeleteWords(IEnumerable<Word> words)
        {
            try
            {
                await DbDeleteWord(words);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region Private Methods

        private static async Task<List<Language>> DbGetLanguages()
        {
            var languages = new List<Language>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Grade, Name, FlagIconPath, Created, Edited, LastLearned FROM languages";
                var r = cmd.ExecuteReader();

                while (await r.ReadAsync())
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

        private static async Task<List<Section>> DbGetSections(Language language)
        {
            var sections = new List<Section>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, LanguageID, Grade, Name, Created, Edited, LastLearned FROM sections WHERE LanguageID like @id";
                cmd.Parameters.AddWithValue("@id", language.Id);
                var r = cmd.ExecuteReader();

                while (await r.ReadAsync())
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

        private static async Task<List<Unit>> DbGetUnits(Section section)
        {
            var units = new List<Unit>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, Name, Grade, SectionID, Created, Edited, LastLearned FROM units WHERE SectionID like @id";
                cmd.Parameters.AddWithValue("@id", section.Id);
                var r = cmd.ExecuteReader();

                while (await r.ReadAsync())
                {
                    var unit = new Unit(
                        (Guid)r["Id"],
                        (Guid)r["SectionID"],
                        (double)r["Grade"],
                        (string)r["Name"],
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

        private static async Task<List<Word>> DbGetWords(Unit unit)
        {
            var words = new List<Word>();

            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                cmd.CommandText = @"SELECT Id, PreviousWordId, Grade, UnitId, pLanguage, sLanguage, Comment, Synonym, Created, Edited, LastLearned FROM words WHERE UnitId like @id";
                cmd.Parameters.AddWithValue("@id", unit.Id);
                var r = cmd.ExecuteReader();

                while (await r.ReadAsync())
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
                        )
                    {
                        PreviousWordId = (Guid)r["PreviousWordId"]
                    };

                    words.Add(word);
                }
            }

            return words;
        }

        private static async Task DbSetLanguage(Language language)
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

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static async Task DbSetSection(Section section)
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

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static async Task DbSetUnit(Unit unit)
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

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static async Task DbSetWord(Word word)
        {
            using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
            {
                if (word.IsNew)
                    cmd.CommandText = @"INSERT INTO words(Id, PreviousWordId, UnitId, Grade, pLanguage, sLanguage, Comment, Synonym, Created, Edited, LastLearned) " +
                                      "VALUES(@Id, @PreviousWordId, @UnitId, @Grade, @pLanguage, @sLanguage, @Comment, @Synonym, @Created, @Edited, @LastLearned)";
                else
                    cmd.CommandText = @"UPDATE words SET UnitId = @UnitId, PreviousWordId = @PreviousWordId, Grade = @Grade, pLanguage = @pLanguage, sLanguage = @sLanguage, " +
                                      "Comment = @Comment, Synonym = @Synonym, Created = @Created, Edited = @Edited, LastLearned = @LastLearned WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", word.Id);
                cmd.Parameters.AddWithValue("@PreviousWordId", word.PreviousWordId);
                cmd.Parameters.AddWithValue("@UnitId", word.UnitId);
                cmd.Parameters.AddWithValue("@Grade", word.Grade);
                cmd.Parameters.AddWithValue("@pLanguage", word.PrimaryLanguage);
                cmd.Parameters.AddWithValue("@sLanguage", word.SecondaryLanguage);
                cmd.Parameters.AddWithValue("@Comment", word.Comment);
                cmd.Parameters.AddWithValue("@Synonym", word.Synonym);
                cmd.Parameters.AddWithValue("@Created", word.Created);
                cmd.Parameters.AddWithValue("@Edited", word.Edited);
                cmd.Parameters.AddWithValue("@LastLearned", word.LastLearned);
                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static async Task DbDeleteLanguage(IEnumerable<Language> languages)
        {
            if (languages == null) 
                return;

            foreach (var language in languages)
            {
                using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
                {
                    cmd.CommandText = @"DELETE FROM languages WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", language.Id);

                    cmd.Prepare();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private static async Task DbDeleteSection(IEnumerable<Section> sections)
        {
            if (sections == null)
                return;

            foreach (var section in sections)
            {
                using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
                {
                    cmd.CommandText = @"DELETE FROM sections WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", section.Id);

                    cmd.Prepare();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private static async Task DbDeleteUnit(IEnumerable<Unit> units)
        {
            if (units == null)
                return;

            foreach (var unit in units)
            {
                using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
                {
                    cmd.CommandText = @"DELETE FROM units WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", unit.Id);

                    cmd.Prepare();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private static async Task DbDeleteWord(IEnumerable<Word> words)
        {
            if (words == null)
                return;

            foreach (var word in words)
            {
                using (var cmd = new SQLiteCommand(_myDatabase._myConnection))
                {
                    cmd.CommandText = @"DELETE FROM words WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", word.Id);

                    cmd.Prepare();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion

        public static void Dispose()
        {
            _myDatabase.Dispose();
        }
    }
}

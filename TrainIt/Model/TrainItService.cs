using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;

namespace TrainIt.Model
{
    public class TrainItService
    {
        private DatabaseService _databaseService;

        #region Constructors
        public TrainItService()
        {
            _databaseService = new DatabaseService();
        }
        #endregion

        #region Database Methods
        public List<Language> GetLanguages()
        {
            try
            {
                return _databaseService.GetLanguages();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public List<Section> GetSections(Language language)
        {
            try
            {
                return _databaseService.GetSections(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Unit> GetUnits(Section section)
        {
            try
            {
                return _databaseService.GetUnits(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Word> GetWords(Unit unit)
        {
            try
            {
                return _databaseService.GetWords(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SetLanguage(Language language)
        {
            try
            {
                _databaseService.SetLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetSection(Section section)
        {
            try
            {
                _databaseService.SetSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUnit(Unit unit)
        {
            try
            {
                _databaseService.SetUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetWord(Word word)
        {
            try
            {
                _databaseService.SetWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteLanguage(Language language)
        {
            try
            {
                _databaseService.DeleteLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteSection(Section section)
        {
            try
            {
                _databaseService.DeleteSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteUnit(Unit unit)
        {
            try
            {
                _databaseService.DeleteUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteWord(Word word)
        {
            try
            {
                _databaseService.DeleteWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}

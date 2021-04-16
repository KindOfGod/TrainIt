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
    public class TrainItService : IDisposable
    {
        private readonly DatabaseService _databaseService;

        #region Constructors
        public TrainItService()
        {
            _databaseService = new DatabaseService();
        }
        #endregion

        #region Database Methods
        public async Task<List<Language>> GetLanguages()
        {
            try
            {
                return await _databaseService.GetLanguages();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Section>> GetSections(Language language)
        {
            try
            {
                return await _databaseService.GetSections(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Unit>> GetUnits(Section section)
        {
            try
            {
                return await _databaseService.GetUnits(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Word>> GetWords(Unit unit)
        {
            try
            {
                return await _databaseService.GetWords(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task SetLanguage(Language language)
        {
            try
            {
                await _databaseService.SetLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task SetSection(Section section)
        {
            try
            {
                await _databaseService.SetSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task SetUnit(Unit unit)
        {
            try
            {
                await _databaseService.SetUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task SetWord(Word word)
        {
            try
            {
                await _databaseService.SetWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteLanguage(Language language)
        {
            try
            {
                await _databaseService.DeleteLanguage(language);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteSection(Section section)
        {
            try
            {
                await _databaseService.DeleteSection(section);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteUnit(Unit unit)
        {
            try
            {
                await _databaseService.DeleteUnit(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteWord(Word word)
        {
            try
            {
                await _databaseService.DeleteWord(word);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion

        public void Dispose()
        {
            _databaseService.Dispose();
        }
    }
}

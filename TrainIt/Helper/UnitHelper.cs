using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TrainIt.Classes;

namespace TrainIt.Helper
{
    public static class UnitHelper
    {
        //bug: topologisches Sortieren (bruch in der Liste)
        public static void SortUnit(ref ObservableCollection<Word> collection)
        {
            if (collection.Count == 0)
                return;

            var map = collection.ToDictionary(w => w.PreviousWordId);
            var words = new ObservableCollection<Word>();

            var currentWord = map[Guid.Empty];
            words.Add(currentWord);

            while (map.ContainsKey(currentWord.Id))
            {
                words.Add(map[currentWord.Id]);
                currentWord = map[currentWord.Id];
            }

            collection = words;
        }
    }
}

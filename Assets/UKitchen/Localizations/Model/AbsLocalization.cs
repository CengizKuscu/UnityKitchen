using System.Collections.Generic;

namespace UKitchen.Localizations.Model
{
    public class AbsLocalization<T> where T : AbsWord
    {
        public List<T> wordList;
    }
}
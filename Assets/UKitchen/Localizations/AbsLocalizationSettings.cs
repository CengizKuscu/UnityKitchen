using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UKitchen.Localizations.Model;
using UnityEngine;

namespace UKitchen.Localizations
{
    public abstract class AbsLocalizationSettings<TWord> where TWord : AbsWord
    {
        public SystemLanguage appLanguage;
        public TextAsset languageSource;
        public bool useSystemLanguage;
        public List<TWord> wordList;

        protected abstract string GetWordByLanguage(TWord word, SystemLanguage lang);

        public string GetText(string key)
        {
            if (wordList.Any())
            {
                TWord word = wordList.FirstOrDefault(s => s.key == key);

                if (word != null)
                {
                    return GetWordByLanguage(word, appLanguage);
                }
            }

            return "[" + key + "]";
        }
        
        public string GetText(string key, params object[] args)
        {

            TWord word = wordList?.FirstOrDefault(s => s.key == key);
            
            string result = "[" + key + "]";
            
            if (word != null)
            {
                MatchCollection matchCollection = null;
                var regExObj = new Regex(@"{(\d+)}");
                result = GetWordByLanguage(word, appLanguage);
                matchCollection = regExObj.Matches(result);

                for (int i = 0; i < matchCollection.Count; i++)
                {
                    string str = "{" + i.ToString() + "}";
                    //result = result;
                    result = result.Replace(str, args[i].ToString());
                }

                return result;
            }

            return "[" + key + "]";
        }

        public abstract string GetTextUpper(string key);
    }
}
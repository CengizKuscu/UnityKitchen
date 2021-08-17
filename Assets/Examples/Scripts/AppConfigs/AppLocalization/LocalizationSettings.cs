using System;
using System.Globalization;
using UKitchen.Localizations;
using UnityEngine;

namespace Localizations
{
    [Serializable]
    public class LocalizationSettings : AbsLocalizationSettings<Word>
    {
        protected override string GetWordByLanguage(Word word, SystemLanguage lang)
        {
            switch (lang)
            {
                case SystemLanguage.English:
                    return word.English;
                case SystemLanguage.Turkish:
                    return word.Turkish;
                default:
                    return word.Turkish;
            }
        }

        public override string GetTextUpper(string key)
        {
            string cultureInfo;
            switch (appLanguage)
            {
                case SystemLanguage.English:
                    cultureInfo = "en";
                    break;
                case SystemLanguage.Turkish:
                    cultureInfo = "tr";
                    break;
                default:
                    cultureInfo = "tr";
                    break;
            }

            return GetText(key).ToUpper(new CultureInfo(cultureInfo));
        }
    }
}
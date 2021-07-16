using System;
using UKitchen.Localizations;
using UKitchen.Localizations.Model;
using UnityEngine;

namespace UKitchen.Examples.LocalizationExample
{
    [Serializable]
    public class LocalizationSettings : AbsLocalizationSettings
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
    }
}
using UKitchen.Localizations.Model;
using UnityEngine;
using Zenject;

namespace UKitchen.Localizations
{
    public abstract class AbsLocalizationManager<TWord, TSettings> : IInitializable where TWord : AbsWord where TSettings : AbsLocalizationSettings<TWord>
    {
        [Inject] private readonly TSettings _settings;
        
        private const string CURRENT_LANGUAGE_SAVE_KEY = "CURRENT_LANGUAGE";
        
        private SystemLanguage _currentLanguage;
        
        public void Initialize()
        {
            if (_settings.useSystemLanguage)
            {
                SetAppLanguage = Application.systemLanguage;
                _settings.appLanguage = SetAppLanguage;
            }
            else
            {
                SetAppLanguage = _settings.appLanguage;
            }
        }

        private SystemLanguage SetAppLanguage
        {
            get
            {
                _currentLanguage =
                    (SystemLanguage) PlayerPrefs.GetInt(CURRENT_LANGUAGE_SAVE_KEY, (int) Application.systemLanguage);
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                PlayerPrefs.SetInt(CURRENT_LANGUAGE_SAVE_KEY, (int)_currentLanguage);
            }
        }
    }
}
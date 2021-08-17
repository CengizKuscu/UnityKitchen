using System;
using UKitchen.Localizations.Model;
using UKitchen.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Localizations
{
    public abstract class
        AbsLocalizationMonoStateHelper : AbsStateMonoHelper<AbsLocalizationMonoStateHelper.StateItem>
    {
        [Serializable]
        public class StateItem : AbsStateItem
        {
            public bool useLocalization;
            public bool toUpper;
            public string key;
            public string text;
        }
    }

    public abstract class AbsLocalizationMonoStateHelper<TWord, TSettings, TInstaller> : AbsLocalizationMonoStateHelper
        where TWord : AbsWord
        where TSettings : AbsLocalizationSettings<TWord>
        where TInstaller : AbsLocalizationInstaller<TWord, TSettings>
    {
        [Inject] protected readonly TSettings _settings;
#if UNITY_EDITOR
        [SerializeField] private TInstaller m_Installer;
#endif
        [SerializeField] private Text m_Text;
        [SerializeField] private bool m_useState;

        private int _state;

        private void OnEnable()
        {
            StateValue = _defaultStateValue;
        }

        public override int StateValue
        {
            get => _state;
            set
            {
                _state = value;
                if (m_Text != null && m_useState)
                {
                    base.StateValue = value;
                    var item = base.SelectedState;
                    if (item != null)
                    {
                        if (item.useLocalization)
#if UNITY_EDITOR
                            if (Application.IsPlaying(m_Text))
                            {
                                if (item.toUpper)
                                    m_Text.text = _settings.GetTextUpper(item.key);
                                else
                                    m_Text.text = _settings.GetText(item.key);
                            }
                            else
                            {
                                if (item.toUpper)
                                    m_Text.text = m_Installer.settings.GetTextUpper(item.key);
                                else
                                    m_Text.text = m_Installer.settings.GetText(item.key);
                            }
#else
                            if (item.toUpper)
                                m_Text.text = _settings.GetTextUpper(item.key);
                            else
                                m_Text.text = _settings.GetText(item.key);
#endif
                        else
                            m_Text.text = item.text;
                    }
                }
            }
        }
    }
}
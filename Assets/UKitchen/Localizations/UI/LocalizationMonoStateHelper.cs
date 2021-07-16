using System;
using UKitchen.Examples.LocalizationExample;
using UKitchen.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Localizations.UI
{
    public class LocalizationMonoStateHelper : AbsStateMonoHelper<LocalizationMonoStateHelper.StateItem>
    {
        [Inject] private LocalizationSettings _settings;
        [SerializeField] private LocalizationInstaller m_Installer;
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
                            m_Text.text = Application.IsPlaying(gameObject) && _settings != null
                                ? _settings.GetText(item.key)
                                : m_Installer.settings.GetText(item.key);
                        else
                            m_Text.text = item.text;
                    }
                }
            }
        }

        [Serializable]
        public class StateItem : AbsStateItem
        {
            public bool useLocalization;
            public string key;
            public string text;
        }
    }
}
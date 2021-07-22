using System;
using UKitchen.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Localizations.UI
{
    [Serializable]
    public class LocalizationStateHelper : AbsStateHelper<LocalizationMonoStateHelper.StateItem>
    {
        [SerializeField] private LocalizationInstaller m_Installer;
        [SerializeField] private Text m_Text;
        [SerializeField] private bool m_useState;

        private int _state;

        public LocalizationStateHelper()
        {
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
                        if (item.useLocalization && m_Installer != null)
                            m_Text.text = m_Installer.settings.GetText(item.key);
                        else
                            m_Text.text = item.text;
                    }
                }
            }
        }
    }
}
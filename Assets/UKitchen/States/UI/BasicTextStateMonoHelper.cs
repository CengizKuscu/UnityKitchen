using System;
using UnityEngine;
using UnityEngine.UI;

namespace UKitchen.States.UI
{
    public class BasicTextStateMonoHelper : AbsStateMonoHelper<BasicTextStateMonoHelper.TextStateItem>
    {
        [SerializeField] private Text m_Text;
        [SerializeField] private bool m_useState;

        private int _state;

        private void Awake()
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
                        if (item.useState2)
                            m_Text.color = item.state2;
                        if (item.useState1)
                            m_Text.text = item.state1;
                    }
                }
            }
        }

        [Serializable]
        public class TextStateItem : AbsStateItem<String, Color>
        {
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UKitchen.States.UI
{
    public class BasicImageMonoState : AbsStateMonoHelper<BasicImageMonoState.ImageStateItem>
    {
        [SerializeField] private Image m_image;
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
                if (m_image != null && m_useState)
                {
                    base.StateValue = value;
                    var item = base.SelectedState;
                    if (item != null)
                    {
                        if (item.useColor)
                            m_image.color = item.color;
                        if (item.useSprite)
                            m_image.sprite = item.sprite;
                    }
                }
            }
        }
        
        [Serializable]
        public class ImageStateItem : AbsStateItem
        {
            public bool useSprite;
            public Sprite sprite;
            public bool useColor;
            public Color color = Color.red;
        }
    }
}
using System;
using UKitchen.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Themes.UI
{
    public class ThemeMonoStateHelper : AbsStateMonoHelper<ThemeMonoStateHelper.StateItem>
    {
        [Inject] private readonly AppThemes themesSettings;
        [Inject] private readonly PaletteName selectedPaletteName;

        [SerializeField] private ThemeInstaller m_Installer;
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_Image;
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
                if (m_useState && (m_Image != null || m_Text != null))
                {
                    base.StateValue = value;
                    var item = base.SelectedState;

                    if (item != null)
                    {
                        if (item.useTheme)
                        {
                            AppPaletteColor paletteColor = Application.IsPlaying(gameObject) && themesSettings != null
                                ? themesSettings.GetColor(selectedPaletteName, item.colorName)
                                : m_Installer.themes.GetColor(m_Installer.selectedPalette, item.colorName);
                            if(m_Text != null)
                                m_Text.color = paletteColor.color;
                            if (m_Image != null)
                                m_Image.color = paletteColor.color;
                        }
                        else
                        {
                            if(m_Text != null)
                                m_Text.color = item.color;
                            if (m_Image != null)
                                m_Image.color = item.color;
                        }

                        if (item.useSprite)
                        {
                            if (m_Image != null)
                                m_Image.sprite = item.sprite;
                        }
                    }
                }
            }
        }

        [Serializable]
        public class StateItem : AbsStateItem
        {
            public bool useTheme;
            public ColorName colorName;
            public Color color;
            public bool useSprite;
            public Sprite sprite;
        }
    }
}
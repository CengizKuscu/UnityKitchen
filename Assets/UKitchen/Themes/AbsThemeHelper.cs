using System;
using UKitchen.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Themes
{
    public abstract class AbsThemeHelper<TInstaller, TTheme, TThemeName, TThemeColor, TColorName> :
        AbsStateMonoHelper<
            AbsThemeHelper<TInstaller, TTheme, TThemeName, TThemeColor, TColorName>.StateItem<TColorName>>
        where TInstaller : AbsThemeInstaller<TThemeName, TTheme>
        where TTheme : AbsTheme<TThemeName, TColorName, TThemeColor>
        where TThemeColor : AbsThemeColor<TColorName>
        where TThemeName : Enum
        where TColorName : Enum
    {
        [Inject] private readonly TTheme theme;
#if UNITY_EDITOR
        [SerializeField] private TInstaller m_Installer;
#endif

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
#if UNITY_EDITOR
                            Color color = Application.IsPlaying(gameObject) && theme != null
                                ? theme.GetColor(item.colorName)
                                : m_Installer.themeSettings.GetColor(item.colorName);
#else
                            Color color = Application.IsPlaying(gameObject) && theme != null
                                ? theme.GetColor(item.colorName)
                                : Color.magenta;
#endif
                            if (m_Text != null)
                                m_Text.color = color;
                            if (m_Image != null)
                                m_Image.color = color;
                        }
                        else
                        {
                            if (m_Text != null)
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
        public class StateItem<TColorName> : AbsStateItem where TColorName : Enum
        {
            public bool useTheme;
            public TColorName colorName;
            public Color color;
            public bool useSprite;
            public Sprite sprite;
        }
    }
}
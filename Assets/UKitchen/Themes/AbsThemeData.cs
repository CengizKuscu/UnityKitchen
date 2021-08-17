using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.Themes
{
    //[CreateAssetMenu(fileName = "AppThemes", menuName = "UnityKitchen/Create/App Theme")]
    public abstract class AbsThemeData<TTheme, TThemeName, TColorName, TColor> : ScriptableObject
        where TThemeName : Enum
        where TColorName : Enum
        where TColor : AbsThemeColor<TColorName>
        where TTheme : AbsTheme<TThemeName, TColorName, TColor>
    {
        public List<TTheme> themes;

        public TTheme CurrentPalette<TThemeName>(TThemeName paletteName) where TThemeName : Enum
        {
            return themes.FirstOrDefault(s => s.themeName.Equals(paletteName));
        }

        public TColor GetColor<TThemeName, TColorName>(TThemeName paletteName, TColorName colorName) where TThemeName : Enum
        {
            return CurrentPalette(paletteName)?.colors.FirstOrDefault(s => s.colorName.Equals(colorName));
        }
    }
}
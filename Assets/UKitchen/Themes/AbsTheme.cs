using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.Themes
{
    
    public abstract class AbsTheme
    {
    }

    
    
    public abstract class AbsTheme<TThemeName, TColorName, TColor> : AbsTheme
        where TThemeName : Enum where TColorName : Enum where TColor : AbsThemeColor<TColorName>

    {
        [SerializeField] private string _name;
        public TThemeName themeName;
        public List<TColor> colors;
        
        public Color GetColor(TColorName colorName)
        {
            var clr = Color.red;

            var tmp = colors.FirstOrDefault(s => s.colorName.Equals(colorName));
            if (tmp != null)
                clr = tmp.color;

            return clr;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.Themes
{
    //[CreateAssetMenu(fileName = "AppThemes", menuName = "UnityKitchen/Create/App Theme")]
    public abstract class AbsThemes<TPalette, TPaletteName, TColorName, TColor> : ScriptableObject
        where TPaletteName : Enum
        where TColorName : Enum
        where TColor : AbsPaletteColor<TColorName>
        where TPalette : AbsPalette<TPaletteName, TColorName, TColor>
    {
        public List<TPalette> themes;

        public TPalette CurrentPalette<TPaletteName>(TPaletteName paletteName)
        {
            return themes.FirstOrDefault(s => s.paletteName.Equals(paletteName));
        }

        public TColor GetColor<TPaletteName, TColorName>(TPaletteName paletteName, TColorName colorName)
        {
            return CurrentPalette(paletteName)?.colors.FirstOrDefault(s => s.colorName.Equals(colorName));
        }
    }
}
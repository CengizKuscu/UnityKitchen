using System;
using System.Collections.Generic;
using UnityEngine;

namespace UKitchen.Themes
{
    
    public abstract class AbsPalette
    {
    }

    
    
    public abstract class AbsPalette<TPaletteName, TColorName, TColor> : AbsPalette
        where TPaletteName : Enum where TColorName : Enum where TColor : AbsPaletteColor<TColorName>

    {
        [SerializeField] private string _name;
        public TPaletteName paletteName;
        public List<TColor> colors;
    }
}
using System;
using UnityEngine;

namespace UKitchen.Themes
{

    public abstract class AbsThemeColor<TColorName> where TColorName : Enum
    {
        [SerializeField] private string _name;
        public TColorName colorName;
        public Color color;
    }
}
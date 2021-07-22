using System;
using UKitchen.Themes;

namespace UKitchen.Themes
{
    [Serializable]
    public class AppPalette : AbsPalette<PaletteName, ColorName, AppPaletteColor>
    {
        
    }

    public class PaletteTest
    {
        public PaletteTest()
        {
            AppPalette tmp = new AppPalette();
            
        }
    }
}
using AppThemes.Installer;
using UnityEditor;
using UKitchen.Themes;

namespace AppThemes.UI
{
    [CustomEditor(typeof(ThemeHelper))]
    public class ThemeHelperEditor : AbsThemeHelperEditor<ThemeInstaller, ThemeName, Theme, ThemeColor, ColorName,
        ThemeHelper>
    {
    }
}
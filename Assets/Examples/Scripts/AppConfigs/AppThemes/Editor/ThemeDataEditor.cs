using System;
using UnityEditor;
using UKitchen.Themes;

namespace AppThemes.Installer
{
	public class ThemeDataEditor : AbsThemeDataEditor<ThemesData, Theme, ThemeName, ColorName, ThemeColor, ThemeDataEditor, ThemeInstaller>
	{
		[MenuItem("UnityKitchen/Theme Color Editor", false, 1)]
		protected static void Init()
		{
			ShowWindow();
		}
	}
}

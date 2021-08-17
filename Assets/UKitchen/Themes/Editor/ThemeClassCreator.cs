using System;
using System.IO;
using AppThemes.Installer;
using UKitchen.Logger;
using UKitchen.Utils;
using UnityEditor;
using UnityEngine;
using Zenject.Internal;

namespace UKitchen.Themes
{
    public class ThemeClassCreator : EditorWindow
    {
        private static ThemeClassCreator _window;

        private const string _colorClass = "ThemeColor";
        private const string _themeNameEnum = "ThemeName";
        private const string _colorNameEnum = "ColorName";
        private const string _themeClassName = "Theme";
        private const string _themeInstallerClassName = "ThemeInstaller";
        private const string _themeDataClass = "ThemesData";
        private const string _themeDataEditorClass = "ThemeDataEditor";
        private const string _themeHelperClass = "ThemeHelper";
        private const string _themeHelperEditorClass = "ThemeHelperEditor";

        private const string _themeDirectory = "AppThemes";

        private string _path;

        [MenuItem("UnityKitchen/Theme Setup", false, 0)]
        public static void Init()
        {
            _window = GetWindow<ThemeClassCreator>("Theme Setup", true, typeof(SceneView));
            _window.Show();
        }

        private void OnGUI()
        {
            if (_window == null)
                Init();

            EditorGUILayout.LabelField(ZenUnityEditorUtil.GetCurrentDirectoryAssetPathFromSelection());
            GUILayout.Space(20);

            EditorGUILayout.BeginVertical(GUILayout.Width(300));

            ThemeCreatorView();

            EditorGUILayout.EndVertical();
        }

        private void ThemeCreatorView()
        {
            if (GUILayout.Button("Theme Class Creator"))
            {
                _path = string.Empty;
                _path = EditorActions.AddCSharpClassTemplate(_colorNameEnum,
                    $"namespace {_themeDirectory}\n" +
                    "{\n" +
                    $"\tpublic enum {_colorNameEnum}\n" +
                    "\t{\n" +
                    "\t\tTestColor1 = 0,\n" +
                    "\t\tTestColor2 = 1,\n" +
                    "\t\tTestColor3 = 2\n" +
                    "\t}\n" +
                    "}\n"
                    , _themeDirectory);

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_colorClass,
                        "using System;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}\n" +
                        "{\n" +
                        "\t[Serializable]\n" +
                        $"\tpublic class {_colorClass} : AbsThemeColor<{_colorNameEnum}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory);

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeNameEnum,
                        $"namespace {_themeDirectory}\n" +
                        "{\n" +
                        $"\tpublic enum {_themeNameEnum}\n" +
                        "\t{\n" +
                        "\t\tDefault = 0\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory);

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeClassName,
                        "using System;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}\n" +
                        "{\n" +
                        "\t[Serializable]\n" +
                        $"\tpublic class {_themeClassName} : AbsTheme<{_themeNameEnum}, {_colorNameEnum}, {_colorClass}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory);

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeDataClass,
                        "using System;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}\n" +
                        "{\n" +
                        $"\tpublic class {_themeDataClass} : AbsThemeData<{_themeClassName}, {_themeNameEnum}, {_colorNameEnum}, {_colorClass}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory);

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeInstallerClassName,
                        "using System;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}.Installer\n" +
                        "{\n" +
                        $"\tpublic class {_themeInstallerClassName} : AbsThemeInstaller<{_themeNameEnum}, {_themeClassName}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory + "/Installer");

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeDataEditorClass,
                        "using System;\n" +
                        "using UnityEditor;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}.Installer\n" +
                        "{\n" +
                        $"\tpublic class {_themeDataEditorClass} : AbsThemeDataEditor<{_themeDataClass}, {_themeClassName}, {_themeNameEnum}, {_colorNameEnum}, {_colorClass}, {_themeDataEditorClass}, {_themeInstallerClassName}>\n" +
                        "\t{\n" +
                        "\t\t[MenuItem(\"UnityKitchen/Theme Color Editor\", false, 1)]\n" +
                        "\t\tprotected static void Init()\n" +
                        "\t\t{\n" +
                        "\t\t\tShowWindow();\n" +
                        "\t\t}\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory + "/Editor");

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeHelperClass,
                        "using AppThemes.Installer;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}.UI\n" +
                        "{\n" +
                        $"\tpublic class {_themeHelperClass} : AbsThemeHelper<{_themeInstallerClassName}, {_themeClassName}, {_themeNameEnum}, {_colorClass}, {_colorNameEnum}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory + "/UI");

                if (!string.IsNullOrEmpty(_path))
                    _path = EditorActions.AddCSharpClassTemplate(_themeHelperEditorClass,
                        "using AppThemes.Installer;\n" +
                        "using UnityEditor;\n" +
                        "using UKitchen.Themes;\n\n" +
                        $"namespace {_themeDirectory}.UI\n" +
                        "{\n" +
                        $"\t[CustomEditor(typeof({_themeHelperClass}))]\n" +
                        $"\tpublic class {_themeHelperEditorClass} : AbsThemeHelperEditor<{_themeInstallerClassName}, {_themeNameEnum}, {_themeClassName}, {_colorClass}, {_colorNameEnum}, {_themeHelperClass}>\n" +
                        "\t{\n" +
                        "\t}\n" +
                        "}\n"
                        , _themeDirectory + "/UI/Editor");
            }
            if(!string.IsNullOrEmpty(_path))
                EditorGUILayout.HelpBox("Open \'Theme Color Editor\'\n UnityKitchen > Theme Color Editor", MessageType.Warning);
        }
    }
}
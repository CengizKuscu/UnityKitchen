using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UKitchen.Themes
{
    public abstract class AbsThemeDataEditor : EditorWindow
    {
        protected const string _themeAssetPath = "/Resources/UnityKitchen/ThemesData.asset";
        protected const string _installerAssetPath = "/Resources/UnityKitchen/ThemeInstaller.asset";

        protected Object obj = null;

        protected SerializedObject _configsObject = null;
        protected SerializedProperty _configThemes;

        protected SerializedObject _installerSerializedObject = null;
        protected SerializedProperty _selectedThemeName;
    }

    public abstract class AbsThemeDataEditor<T> : AbsThemeDataEditor where T : EditorWindow
    {
        protected static T _window;
    }

    public abstract class
        AbsThemeDataEditor<TConfigs, TTheme, TThemeName, TColorName, TColor, T,
            TInstaller> : AbsThemeDataEditor<T>
        where TConfigs : AbsThemeData<TTheme, TThemeName, TColorName, TColor>
        where TTheme : AbsTheme<TThemeName, TColorName, TColor>
        where TColor : AbsThemeColor<TColorName>
        where TThemeName : Enum
        where TColorName : Enum
        where T : AbsThemeDataEditor<T>
        where TInstaller : AbsThemeInstaller<TThemeName, TTheme>
    {
        protected bool _isContinue;

        private TInstaller _installer;
        private TConfigs _themes = null;

        protected static void ShowWindow()
        {
            _window = GetWindow<T>(
                "Color Settings", true, typeof(SceneView));
            _window.Show();
        }

        protected virtual void OnGUI()
        {
            _isContinue = false;
            if (_window == null)
                ShowWindow();

            var themeDataPath = Application.dataPath + _themeAssetPath;
            var installerPath = Application.dataPath + _installerAssetPath;


            if (!File.Exists(themeDataPath) || !File.Exists(installerPath))
            {
                if (!File.Exists(themeDataPath) && GUILayout.Button("Create Color Settings"))
                {
                    var asset = ScriptableObject.CreateInstance<TConfigs>();
                    AssetDatabase.CreateAsset(asset, $"Assets" + _themeAssetPath);
                    AssetDatabase.SaveAssets();
                }

                if (!File.Exists(installerPath) && GUILayout.Button("Create Theme Installer"))
                {
                    var asset = ScriptableObject.CreateInstance<TInstaller>();
                    AssetDatabase.CreateAsset(asset, $"Assets" + _installerAssetPath);
                    AssetDatabase.SaveAssets();
                }

                _isContinue = false;
                return;
            }
            else
            {
                //_isContinue = true;
                _themes = AssetDatabase.LoadAssetAtPath<TConfigs>("Assets" + _themeAssetPath);
                _configsObject = new SerializedObject(_themes);

                _installer = AssetDatabase.LoadAssetAtPath<TInstaller>("Assets" + _installerAssetPath);
                _installerSerializedObject = new SerializedObject(_installer);

                _configThemes = _configsObject.FindProperty("themes");

                _selectedThemeName = _installerSerializedObject.FindProperty("selectedTheme");
            }


            if (_configsObject != null && _installerSerializedObject != null)
            {
                _isContinue = true;


                EditorGUILayout.PropertyField(_selectedThemeName);

                _installerSerializedObject.Update();


                _configsObject.Update();

                ShowList(_configThemes);


                _configsObject.ApplyModifiedProperties();


                if (GUILayout.Button("Update to Installer") && _themes != null)
                {
                    var tmpTheme =
                        _themes.themes.First(s =>
                            s.themeName.ToString()
                                .Equals(_selectedThemeName.enumNames[_selectedThemeName.enumValueIndex]));

                    _installer.themeSettings = tmpTheme;
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }


                _installerSerializedObject.ApplyModifiedProperties();
            }
        }

        private void ShowList(SerializedProperty themes)
        {
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(_configThemes, false);

            if (themes.arraySize == 0)
            {
                themes.arraySize++;
            }

            SerializedProperty tmpTheme = themes.GetArrayElementAtIndex(0);


            if (_configThemes.isExpanded)
            {
                _configThemes.arraySize = EditorGUILayout.IntField("Array Size", _configThemes.arraySize);
                for (int i = 0; i < _configThemes.arraySize; i++)
                {
                    EditorGUILayout.Separator();
                    SerializedProperty theme = _configThemes.GetArrayElementAtIndex(i);

                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(theme, false);

                    SerializedProperty _name = theme.FindPropertyRelative("_name");
                    SerializedProperty paletteName = theme.FindPropertyRelative("themeName");
                    paletteName.enumValueIndex =
                        i < paletteName.enumNames.Length ? i : paletteName.enumNames.Length - 1;
                    SerializedProperty colors = theme.FindPropertyRelative("colors");

                    _name.stringValue = paletteName.enumNames[paletteName.enumValueIndex];

                    if (theme.isExpanded)
                    {
                        EditorGUI.indentLevel++;

                        EditorGUILayout.PropertyField(colors, false);

                        if (colors.isExpanded)
                        {
                            EditorGUI.indentLevel++;
                            /*colors.arraySize = EditorGUILayout.IntField("Color Size", colors.arraySize,
                                GUILayout.ExpandWidth(true));*/
                            if (colors.arraySize == 0)
                            {
                                colors.arraySize++;
                            }

                            SerializedProperty tmpColor = colors.GetArrayElementAtIndex(0);
                            SerializedProperty tmpColorName = tmpColor.FindPropertyRelative("colorName");

                            if (colors.arraySize < tmpColorName.enumNames.Length)
                            {
                                colors.arraySize = tmpColorName.enumNames.Length;
                            }

                            for (int j = 0; j < colors.arraySize; j++)
                            {
                                SerializedProperty paletteColor = colors.GetArrayElementAtIndex(j);
                                SerializedProperty _pname = paletteColor.FindPropertyRelative("_name");
                                SerializedProperty colorName = paletteColor.FindPropertyRelative("colorName");
                                colorName.enumValueIndex = j;
                                SerializedProperty color = paletteColor.FindPropertyRelative("color");

                                _pname.stringValue = colorName.enumNames[colorName.enumValueIndex];
                                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                                //EditorGUILayout.LabelField(_pname.stringValue);
                                EditorGUILayout.LabelField(colorName.enumNames[colorName.enumValueIndex],
                                    GUILayout.ExpandWidth(true));
                                color.colorValue = EditorGUILayout.ColorField(color.colorValue);
                                //EditorGUILayout.PropertyField(color, GUILayout.ExpandWidth(true));
                                EditorGUILayout.EndHorizontal();
                            }

                            EditorGUI.indentLevel--;
                        }

                        EditorGUI.indentLevel--;
                    }

                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UKitchen.Themes
{
    public class ThemeEditor : EditorWindow
    {
        private static ThemeEditor _window;

        private const string themeAssetPath = "/Resources/UnityKitchen/Themes.asset";

        private Object obj = null;

        private SerializedObject serializedObject = null;
        private SerializedProperty _themes;


        [MenuItem("UnityKitchen/Theme Editor")]
        private static void Init()
        {
            ShowWindow();
        }

        private static void ShowWindow()
        {
            _window = GetWindow<ThemeEditor>("Theme Editor", true, typeof(SceneView));
            _window.Show();
        }

        private void OnGUI()
        {
            if (_window == null)
                ShowWindow();

            var themeDataPath = Application.dataPath + themeAssetPath;

            if (!File.Exists(themeDataPath))
            {
                obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);

                if (GUILayout.Button("Create Asset"))
                {
                    var asset = ScriptableObject.CreateInstance(obj.name);
                    AssetDatabase.CreateAsset(asset, $"Assets/Resources/UnityKitchen/Themes.asset");
                    AssetDatabase.SaveAssets();
                    //EditorUtility.FocusProjectWindow();
                    Selection.activeObject = asset;
                }

                return;
            }
            else
            {
                serializedObject = new SerializedObject(AssetDatabase.LoadAssetAtPath(
                    "Assets/Resources/UnityKitchen/Themes.asset",
                    typeof(ScriptableObject)));
            }

            if (serializedObject != null)
            {
                serializedObject.Update();
                _themes = serializedObject.FindProperty("themes");
                ShowList(_themes);

                serializedObject.ApplyModifiedProperties();
            }
        }

        private void ShowList(SerializedProperty themes)
        {
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(_themes, false);

            if (themes.arraySize == 0)
            {
                themes.arraySize++;
            }

            SerializedProperty tmpTheme = themes.GetArrayElementAtIndex(0);
            SerializedProperty tmpPaletteName = tmpTheme.FindPropertyRelative("paletteName");


            if (_themes.isExpanded)
            {
                _themes.arraySize = EditorGUILayout.IntField("Array Size", _themes.arraySize);
                for (int i = 0; i < _themes.arraySize; i++)
                {
                    EditorGUILayout.Separator();
                    SerializedProperty palette = _themes.GetArrayElementAtIndex(i);

                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(palette, false);
                    
                    SerializedProperty _name = palette.FindPropertyRelative("_name");
                    SerializedProperty paletteName = palette.FindPropertyRelative("paletteName");
                    paletteName.enumValueIndex =
                        i < paletteName.enumNames.Length ? i : paletteName.enumNames.Length - 1;
                    SerializedProperty colors = palette.FindPropertyRelative("colors");

                    _name.stringValue = paletteName.enumNames[paletteName.enumValueIndex];

                    if (palette.isExpanded)
                    {
                        EditorGUI.indentLevel++;

                        EditorGUILayout.PropertyField(colors, false);

                        if (colors.isExpanded)
                        {
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
                                EditorGUILayout.LabelField(colorName.enumNames[colorName.enumValueIndex],GUILayout.ExpandWidth(true));
                                color.colorValue = EditorGUILayout.ColorField(color.colorValue);
                                //EditorGUILayout.PropertyField(color, GUILayout.ExpandWidth(true));
                                EditorGUILayout.EndHorizontal();
                            }
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
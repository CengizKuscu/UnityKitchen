using System;
using UnityEditor;
using UnityEngine;

namespace UKitchen.Themes.UI
{
    [CustomEditor(typeof(ThemeMonoStateHelper))]
    public class ThemeMonoStateHelperEditor : Editor
    {
        private SerializedProperty m_Installer;
        private SerializedProperty m_Text;
        private SerializedProperty m_Image;
        private SerializedProperty m_useState;
        
        private SerializedProperty _stateItems;
        private SerializedProperty _defaultStateValue;

        private ThemeInstaller _installer;

        private void OnEnable()
        {
            m_Installer = serializedObject.FindProperty("m_Installer");
            m_Text = serializedObject.FindProperty("m_Text");
            m_Image = serializedObject.FindProperty("m_Image");
            m_useState = serializedObject.FindProperty("m_useState");
            _stateItems = serializedObject.FindProperty("_stateItems");
            _defaultStateValue = serializedObject.FindProperty("_defaultStateValue");

            Init();
        }
        
        private void Init()
        {
            if (m_Installer.objectReferenceValue == null || _installer == null)
            {
                _installer = AssetDatabase.LoadAssetAtPath<ThemeInstaller>(
                    "Assets/Resources/UnityKitchen/ThemeInstaller.asset");
                m_Installer.objectReferenceValue = _installer;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            Init();

            ThemeMonoStateHelper comp = (ThemeMonoStateHelper) target;
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(m_Installer);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(m_Text);
            EditorGUILayout.PropertyField(m_Image);
            EditorGUILayout.PropertyField(m_useState);
            
            EditorGUILayout.PropertyField(_defaultStateValue);
            EditorGUI.indentLevel++;
            ShowStateList(_stateItems);
            EditorGUI.indentLevel--;
            if (m_useState.boolValue)
            {
                comp.StateValue = _defaultStateValue.intValue;
                
            }

            serializedObject.ApplyModifiedProperties();
            
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(comp);
        }

        private void ShowStateList(SerializedProperty list)
        {
            if (_installer == null)
                return;

            AppThemes themes = _installer.themes;
            

            EditorGUILayout.PropertyField(list, false);
            if (list.isExpanded)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                for (int i = 0; i < list.arraySize; i++)
                {
                    EditorGUI.indentLevel++;

                    SerializedProperty item = list.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(item, false);
                    if (item.isExpanded)
                    {
                        EditorGUI.indentLevel++;

                        SerializedProperty _stateValue = item.FindPropertyRelative("_stateValue");
                        SerializedProperty useTheme = item.FindPropertyRelative("useTheme");
                        SerializedProperty colorName = item.FindPropertyRelative("colorName");
                        SerializedProperty color = item.FindPropertyRelative("color");
                        SerializedProperty useSprite = item.FindPropertyRelative("useSprite");
                        SerializedProperty sprite = item.FindPropertyRelative("sprite");

                        EditorGUILayout.PropertyField(_stateValue);
                        EditorGUILayout.PropertyField(useTheme);

                        if (useTheme.boolValue)
                        {
                            EditorGUILayout.PropertyField(colorName);
                            ColorName clrName = (ColorName) colorName.enumValueIndex;
                            AppPaletteColor paletteColor =
                                _installer.themes.GetColor(_installer.selectedPalette, clrName);
                            color.colorValue = paletteColor.color;
                        }
                        
                        EditorGUILayout.PropertyField(color);
                        EditorGUILayout.PropertyField(useSprite);
                        if (useSprite.boolValue)
                        {
                            EditorGUILayout.PropertyField(sprite);
                        }

                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUI.indentLevel--;
                }
            }
        }
    }
}
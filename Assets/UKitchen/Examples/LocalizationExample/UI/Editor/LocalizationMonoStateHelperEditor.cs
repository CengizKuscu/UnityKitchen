using System;
using System.Collections.Generic;
using System.Linq;
using UKitchen.Localizations.Model;
using UnityEditor;

namespace UKitchen.Localizations.UI
{
    [CustomEditor(typeof(LocalizationMonoStateHelper))]
    public class LocalizationMonoStateHelperEditor : Editor
    {
        private SerializedProperty m_Installer;
        private SerializedProperty m_Text;
        private SerializedProperty m_useState;
        private SerializedProperty _stateItems;
        private SerializedProperty _defaultStateValue;

        private LocalizationInstaller _installer;

        private void OnEnable()
        {
            m_Installer = serializedObject.FindProperty("m_Installer");
            m_Text = serializedObject.FindProperty("m_Text");
            m_useState = serializedObject.FindProperty("m_useState");
            _stateItems = serializedObject.FindProperty("_stateItems");
            _defaultStateValue = serializedObject.FindProperty("_defaultStateValue");

            Init();
        }

        private void Init()
        {
            if (m_Installer.objectReferenceValue == null || _installer == null)
            {
                _installer = AssetDatabase.LoadAssetAtPath<LocalizationInstaller>(
                    "Assets/Resources/UnityKitchen/LocalizationInstaller.asset");
                m_Installer.objectReferenceValue = _installer;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Init();

            LocalizationMonoStateHelper comp = (LocalizationMonoStateHelper) target;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(m_Installer);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(m_Text);
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

            List<Word> wordList = _installer.settings.wordList;
            string[] keyList = wordList.Select(s => s.key).ToArray();

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
                        SerializedProperty useLocalization = item.FindPropertyRelative("useLocalization");
                        SerializedProperty key = item.FindPropertyRelative("key");
                        SerializedProperty stateStr = item.FindPropertyRelative("text");


                        EditorGUILayout.PropertyField(_stateValue);
                        EditorGUILayout.PropertyField(useLocalization);
                        
                            if (useLocalization.boolValue)
                            {
                                if (string.IsNullOrEmpty(key.stringValue))
                                    key.stringValue = keyList[0];

                                int keyIndex = wordList.FindIndex(s => s.key == key.stringValue);

                                keyIndex = EditorGUILayout.Popup(keyIndex, keyList);

                                Word word = wordList.FirstOrDefault(s => s.key == keyList[keyIndex]);
                                if (word != null)
                                {
                                    stateStr.stringValue = _installer.settings.GetText(key.stringValue);
                                    key.stringValue = keyList[keyIndex];
                                }
                            }

                            EditorGUILayout.PropertyField(stateStr);
                        
                        
                            //}


                        EditorGUI.indentLevel--;
                    }

                    EditorGUI.indentLevel--;
                }
            }
        }
    }
}
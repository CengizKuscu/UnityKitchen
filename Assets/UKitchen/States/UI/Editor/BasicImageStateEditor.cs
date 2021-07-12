using System;
using UnityEditor;
using UnityEngine.UI;

namespace UKitchen.States.UI
{
    [CustomEditor(typeof(BasicImageMonoState))]
    public class BasicImageStateEditor : Editor
    {
        private SerializedProperty m_image;
        private SerializedProperty m_useState;
        private SerializedProperty _stateItems;
        private SerializedProperty _defaultStateValue;


        private void OnEnable()
        {
            m_image = serializedObject.FindProperty("m_image");
            m_useState = serializedObject.FindProperty("m_useState");
            _stateItems = serializedObject.FindProperty("_stateItems");
            _defaultStateValue = serializedObject.FindProperty("_defaultStateValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            BasicImageMonoState comp = (BasicImageMonoState) target;

            EditorGUILayout.PropertyField(m_image);
            EditorGUILayout.PropertyField(m_useState);
            if (m_useState.boolValue)
            {
                EditorGUILayout.PropertyField(_defaultStateValue);

                EditorGUILayout.PropertyField(_stateItems);

                comp.StateValue = _defaultStateValue.intValue;
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
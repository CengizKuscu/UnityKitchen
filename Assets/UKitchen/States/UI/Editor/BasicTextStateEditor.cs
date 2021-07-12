using System;
using UnityEditor;

namespace UKitchen.States.UI
{
    [CustomEditor(typeof(BasicTextStateMonoHelper))]
    public class BasicTextStateEditor : Editor
    {
        private SerializedProperty m_Text;
        private SerializedProperty m_useState;
        private SerializedProperty _stateItems;
        private SerializedProperty _defaultStateValue;

        private void OnEnable()
        {
            m_Text = serializedObject.FindProperty("m_Text");
            m_useState = serializedObject.FindProperty("m_useState");
            _stateItems = serializedObject.FindProperty("_stateItems");
            _defaultStateValue = serializedObject.FindProperty("_defaultStateValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            BasicTextStateMonoHelper comp = (BasicTextStateMonoHelper) target;

            EditorGUILayout.PropertyField(m_Text);
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
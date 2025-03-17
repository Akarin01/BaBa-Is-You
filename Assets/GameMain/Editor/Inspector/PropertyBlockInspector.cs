using KitaFramework;
using System.Linq;
using UnityEditor;

namespace BabaIsYou
{
    [CustomEditor(typeof(PropertyBlock))]
    public class PropertyBlockInspctor : FrameworkManagerInspector
    {
        private SerializedProperty m_logicType;

        private string[] m_logicTypeNames;
        private int m_selectedLogicIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            int selectedIndex = EditorGUILayout.Popup("LogicType: ", m_selectedLogicIndex, m_logicTypeNames);
            if (selectedIndex != m_selectedLogicIndex)
            {
                m_selectedLogicIndex = selectedIndex;
                m_logicType.stringValue = m_logicTypeNames[selectedIndex];
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            m_logicType = serializedObject.FindProperty("m_logicType");

            RefreshLogicTypeNames();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshLogicTypeNames();
        }

        private void RefreshLogicTypeNames()
        {
            m_logicTypeNames = TypeHelper.GetTypeNames(typeof(LogicBase));

            if (!string.IsNullOrEmpty(m_logicType.stringValue))
            {
                m_selectedLogicIndex = m_logicTypeNames.ToList().IndexOf(m_logicType.stringValue);
                if (m_selectedLogicIndex < 0)
                {
                    m_logicType.stringValue = null;
                }
            }
        }
    }
}
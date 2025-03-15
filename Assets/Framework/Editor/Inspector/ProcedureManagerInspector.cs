using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

namespace KitaFramework
{
    [CustomEditor(typeof(ProcedureManager))]
    public class ProcedureManagerInspector : FrameworkManagerInspector
    {
        private SerializedProperty m_availableProcedureTypeNames;
        private SerializedProperty m_entranceProcedureTypeName;

        private string[] m_procedureTypeNames;
        private List<string> m_currentAvailableProcedureTypeNames;
        private int m_entranceProcedureIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            ProcedureManager t = (ProcedureManager)target;

            if (string.IsNullOrEmpty(m_entranceProcedureTypeName.stringValue))
            {
                EditorGUILayout.HelpBox("Entrance procedure is invalid", MessageType.Error);
            }
            else if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Current Procedure: " +
                    t.CurrentProcedure?.GetType().ToString() ?? "None");
            }

            // 设置 m_currentAvailableProcedureTypeNames 和 m_availableProcedureTypeNames
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                GUILayout.Label("Available Procedure", EditorStyles.boldLabel);

                if (m_procedureTypeNames.Length > 0)
                {
                    EditorGUILayout.BeginVertical("box");

                    foreach (var procedureTypeName in m_procedureTypeNames)
                    {
                        bool selected = m_currentAvailableProcedureTypeNames.Contains(procedureTypeName);

                        if (selected != EditorGUILayout.ToggleLeft(procedureTypeName, selected))
                        {
                            // 发生变化
                            if (!selected)
                            {
                                // 原本没选中，现在选中
                                m_currentAvailableProcedureTypeNames.Add(procedureTypeName);
                                WriteAvailableProcedureTypeNames();
                            }
                            else if (procedureTypeName != m_entranceProcedureTypeName.stringValue)
                            {
                                // 原本选中，现在没选中
                                // 不允许取消勾选到 m_entranceProcedureTypeName
                                m_currentAvailableProcedureTypeNames.Remove(procedureTypeName);
                                WriteAvailableProcedureTypeNames();
                            }
                        }
                    }

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.HelpBox("There is no available procedure", MessageType.Error);
                }
            }

            // 设置 m_entranceProcedureIndex 和 m_entranceProcedureTypeName
            if (m_currentAvailableProcedureTypeNames.Count > 0)
            {
                int selectedIndex = EditorGUILayout.Popup("Entrance Procedure: ", m_entranceProcedureIndex, m_currentAvailableProcedureTypeNames.ToArray());
                if (selectedIndex != m_entranceProcedureIndex)
                {
                    // 变化，更新
                    m_entranceProcedureTypeName.stringValue = m_currentAvailableProcedureTypeNames[selectedIndex];
                    m_entranceProcedureIndex = selectedIndex;
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Select available procedure first", MessageType.Info);
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            m_availableProcedureTypeNames = serializedObject.FindProperty("m_availableProcedureTypeNames");
            m_entranceProcedureTypeName = serializedObject.FindProperty("m_entranceProcedureTypeName");

            RefreshTypeNames();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        // 更新 m_procedureTypeNames
        private void RefreshTypeNames()
        {
            // 获取 ProcedureBase 子类
            m_procedureTypeNames = TypeHelper.GetTypeNames(typeof(ProcedureBase));

            // 检查 m_currentAvailableProcedureTypeNames
            ReadAvailableProcedureTypeNames();
            int oldCount = m_currentAvailableProcedureTypeNames.Count;
            m_currentAvailableProcedureTypeNames = m_currentAvailableProcedureTypeNames.Where(
                typeName =>
                {
                    return m_procedureTypeNames.Contains(typeName);
                }
                ).ToList();
            if (m_currentAvailableProcedureTypeNames.Count != oldCount)
            {
                // 发生了变化，更新 m_currentAvailableProcedureTypeNames
                WriteAvailableProcedureTypeNames();
            }
            else if (!string.IsNullOrEmpty(m_entranceProcedureTypeName.stringValue))
            {
                // 因为 m_currentAvailableProcedureTypeNames 变化而导致 m_entranceProcedureTypeName 需要更新
                m_entranceProcedureIndex =
                    m_currentAvailableProcedureTypeNames.IndexOf(m_entranceProcedureTypeName.stringValue);

                if (m_entranceProcedureIndex < 0)
                {
                    m_entranceProcedureTypeName.stringValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 读 m_availableProcedureTypeNames
        /// </summary>
        private void ReadAvailableProcedureTypeNames()
        {
            m_currentAvailableProcedureTypeNames = new List<string>();
            int count = m_availableProcedureTypeNames.arraySize;
            for (int i = 0; i < count; i++)
            {
                m_currentAvailableProcedureTypeNames.Add(m_availableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue);
            }
        }

        /// <summary>
        /// 写 m_availableProcedureTypeNames 及因其变化而导致 m_entranceProcedureTypeName
        /// </summary>
        private void WriteAvailableProcedureTypeNames()
        {
            m_availableProcedureTypeNames.ClearArray();

            if (m_currentAvailableProcedureTypeNames == null)
            {
                return;
            }

            m_currentAvailableProcedureTypeNames.Sort();
            int count = m_currentAvailableProcedureTypeNames.Count;

            for (int i = 0; i < count; i++)
            {
                m_availableProcedureTypeNames.InsertArrayElementAtIndex(i);
                m_availableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue =
                    m_currentAvailableProcedureTypeNames[i];
            }

            // 因为 m_currentAvailableProcedureTypeNames 变化而导致 m_entranceProcedureTypeName 需要更新
            if (!string.IsNullOrEmpty(m_entranceProcedureTypeName.stringValue))
            {
                m_entranceProcedureIndex =
                    m_currentAvailableProcedureTypeNames.IndexOf(m_entranceProcedureTypeName.stringValue);

                if (m_entranceProcedureIndex < 0)
                {
                    m_entranceProcedureTypeName.stringValue = null;
                }
            }
        }
    }
}
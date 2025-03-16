using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using KitaFramework;
using System;

namespace BabaIsYou
{
    public class MapEditorWindow : FrameworkWindow
    {
        private List<GameObject> m_prefabList = new();
        private bool m_foldout;
        private Vector2 m_scrollView;
        private Rect m_dropArea;
        private int m_selectedIndex = -1;
        private string[] m_logicTypeNames;
        private int m_logicIndex = -1;
        private bool m_foldoutNounBlock;
        private bool m_foldoutPropertyBlock;

        [MenuItem("Tools/MapEditorWindow")]
        public static void ShowWindow()
        {
            GetWindow<MapEditorWindow>();
        }

        private void OnEnable()
        {
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
        }

        protected override void OnGUI()
        {
            // ������
            EditorGUILayout.HelpBox("Tool Bar", MessageType.Info);

            // ����ѡ�е�Ԥ����ĳ�ʼֵ
            if (m_selectedIndex != -1)
            {
                GameObject selectedPrefab = m_prefabList[m_selectedIndex];
                if (selectedPrefab != null)
                {
                    EditorGUILayout.BeginVertical("box");

                    if (selectedPrefab.TryGetComponent(out NounBlock nounBlock))
                    {
                        SerializedObject serializedObject = new(nounBlock);
                        SerializedProperty m_entity = serializedObject.FindProperty("m_entity");

                        // ��ʾ�����ó�ʼֵ
                        m_foldoutNounBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutNounBlock, $"{typeof(NounBlock)}: ");
                        if (m_foldoutNounBlock)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.PrefixLabel("Entity: ");
                            EntityBase newValue = EditorGUILayout.ObjectField(m_entity.objectReferenceValue, typeof(EntityBase), true) as EntityBase;
                            if (newValue != m_entity.objectReferenceValue)
                            {
                                // ����
                                m_entity.objectReferenceValue = newValue;
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndFoldoutHeaderGroup();

                        serializedObject.ApplyModifiedProperties();
                    }
                    if (selectedPrefab.TryGetComponent(out PropertyBlock propertyBlock))
                    {
                        SerializedObject serializedObject = new(propertyBlock);
                        SerializedProperty m_logicType = serializedObject.FindProperty("m_logicType");
                        
                        // ��ʾ��ʼֵ�����ó�ʼֵ
                        m_foldoutPropertyBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutPropertyBlock, $"{typeof(PropertyBlock)}: ");
                        if (m_foldoutPropertyBlock)
                        {
                            int selectIndex = EditorGUILayout.Popup("LogicType: ", m_logicIndex, m_logicTypeNames);

                            if (selectIndex != m_logicIndex)
                            {
                                // ����
                                m_logicIndex = selectIndex;
                                m_logicType.stringValue = m_logicTypeNames[selectIndex];
                            }
                        }
                        EditorGUILayout.EndFoldoutHeaderGroup();

                        serializedObject.ApplyModifiedProperties();
                    }

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.HelpBox("Select Null", MessageType.Info);
                }
            }

            // ��ʾ�洢��Ԥ����
            EditorGUILayout.BeginHorizontal();
            m_foldout = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldout, "Prefab List: ");
            if (m_foldout)
            {
                if (GUILayout.Button("+", GUILayout.Width(20f)))
                {
                    m_prefabList.Add(null);
                }
                if (GUILayout.Button("-", GUILayout.Width(20f)))
                {
                    if (m_selectedIndex != -1)
                    {
                        // ɾ��ָ��Ԫ��
                        m_prefabList.RemoveAt(m_selectedIndex);
                        m_selectedIndex = -1;
                    }
                    else if (m_prefabList.Count > 0)
                    {
                        // ɾ�����һ��Ԫ��
                        m_prefabList.RemoveAt(m_prefabList.Count - 1);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            if (m_foldout)
            {
                EditorGUILayout.BeginVertical("framebox");
                m_scrollView = EditorGUILayout.BeginScrollView(m_scrollView);
                {
                    // ��ʾԤ�����ǵ���Ϣ
                    for (int i = 0; i < m_prefabList.Count; i++)
                    {
                        Color orginalColor = GUI.backgroundColor;
                        Color selectedColor = Color.green;

                        if (m_selectedIndex == i)
                        {
                            // �����Ԫ�ر�ѡ�У�������ʾ
                            GUI.backgroundColor = selectedColor;
                        }
                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("Select", GUILayout.MaxWidth(50f)))
                        {
                            m_selectedIndex = i;
                            InitSelectedPrefab();
                        }
                        m_prefabList[i] = EditorGUILayout.ObjectField(m_prefabList[i], typeof(GameObject), false) as GameObject;
                        EditorGUILayout.EndHorizontal();

                        // �ָ���ɫ
                        GUI.backgroundColor = orginalColor;
                    }
                }
                m_dropArea = GUILayoutUtility.GetRect(0, 0,
                    GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            HandleDragAndDrop();
        }

        private void HandleDragAndDrop()
        {
            if (!m_foldout)
            {
                // δչ������������ק�¼�
                return;
            }

            Event currentEvent = Event.current;
            switch (currentEvent.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!m_dropArea.Contains(currentEvent.mousePosition))
                    {
                        // ������λ�ò��ڷ������򣬲�����
                        break;
                    }

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (currentEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        // ������ק����
                        foreach (var obj in DragAndDrop.objectReferences)
                        {
                            if (obj is GameObject go && AssetDatabase.Contains(obj))
                            {
                                // ����������� GameObject ͬʱҲ�� asset����ӵ�Ԥ�����б�
                                m_prefabList.Add(go);
                            }
                        }

                        GUI.changed = true;

                        currentEvent.Use();
                    }

                    break;
            }
        }
    
        private void InitSelectedPrefab()
        {
            m_logicIndex = -1;
            m_foldoutNounBlock = true;
            m_foldoutPropertyBlock = true;
        }
    }
}
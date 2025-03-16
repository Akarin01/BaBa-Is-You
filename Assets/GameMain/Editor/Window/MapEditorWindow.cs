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
            // 工具栏
            EditorGUILayout.HelpBox("Tool Bar", MessageType.Info);

            // 设置选中的预制体的初始值
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

                        // 显示并设置初始值
                        m_foldoutNounBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutNounBlock, $"{typeof(NounBlock)}: ");
                        if (m_foldoutNounBlock)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.PrefixLabel("Entity: ");
                            EntityBase newValue = EditorGUILayout.ObjectField(m_entity.objectReferenceValue, typeof(EntityBase), true) as EntityBase;
                            if (newValue != m_entity.objectReferenceValue)
                            {
                                // 更新
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
                        
                        // 显示初始值并设置初始值
                        m_foldoutPropertyBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutPropertyBlock, $"{typeof(PropertyBlock)}: ");
                        if (m_foldoutPropertyBlock)
                        {
                            int selectIndex = EditorGUILayout.Popup("LogicType: ", m_logicIndex, m_logicTypeNames);

                            if (selectIndex != m_logicIndex)
                            {
                                // 更新
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

            // 显示存储的预制体
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
                        // 删除指定元素
                        m_prefabList.RemoveAt(m_selectedIndex);
                        m_selectedIndex = -1;
                    }
                    else if (m_prefabList.Count > 0)
                    {
                        // 删除最后一个元素
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
                    // 显示预制体们的信息
                    for (int i = 0; i < m_prefabList.Count; i++)
                    {
                        Color orginalColor = GUI.backgroundColor;
                        Color selectedColor = Color.green;

                        if (m_selectedIndex == i)
                        {
                            // 如果该元素被选中，高亮显示
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

                        // 恢复颜色
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
                // 未展开，不处理拖拽事件
                return;
            }

            Event currentEvent = Event.current;
            switch (currentEvent.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!m_dropArea.Contains(currentEvent.mousePosition))
                    {
                        // 如果鼠标位置不在放置区域，不处理
                        break;
                    }

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (currentEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        // 遍历拖拽对象
                        foreach (var obj in DragAndDrop.objectReferences)
                        {
                            if (obj is GameObject go && AssetDatabase.Contains(obj))
                            {
                                // 如果该物体是 GameObject 同时也是 asset，添加到预制体列表
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
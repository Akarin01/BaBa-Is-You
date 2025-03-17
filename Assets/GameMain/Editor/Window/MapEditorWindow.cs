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
        private int m_toolIndex = -1;

        [MenuItem("Tools/MapEditorWindow")]
        public static void ShowWindow()
        {
            GetWindow<MapEditorWindow>();
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += SceneView_DuringSceneGui;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= SceneView_DuringSceneGui;
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            // 工具栏
            DrawToolBarGUI();

            // 显示存储的预制体
            DrawPrefabListGUI();

            // 处理拖拽事件
            HandleDragAndDrop();
        }

        private void SceneView_DuringSceneGui(SceneView obj)
        {
            Event currentEvent = Event.current;
            bool isLeft = currentEvent.button == 0;

            if (m_toolIndex != -1)
            {
                Tools.current = Tool.View;      // 强制设置工具为 View，防止工具拦截事件
            }

            switch (m_toolIndex)
            {
                case 0:
                    // 单击实例化
                    if (currentEvent.type == EventType.MouseDown && isLeft)
                    {
                        // 单击实例化
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (!HasColliderAtPosition(position, out _))
                        {
                            // 该位置没有物体，生成物体
                            SpawnPrefabAtPosition(position);
                            currentEvent.Use();
                        }
                    }
                    break;
                case 1:
                    // 长按实例化

                    if (isLeft &&
                        (currentEvent.type == EventType.MouseDown || currentEvent.type == EventType.MouseDrag))
                    {
                        // 单击实例化
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (!HasColliderAtPosition(position, out _))
                        {
                            // 该位置没有物体，生成物体
                            SpawnPrefabAtPosition(position);
                            currentEvent.Use();
                        }
                    }
                    break;
                case 2:
                    // 删除实体
                    if (currentEvent.type == EventType.MouseDown && isLeft)
                    {
                        // 删除实体
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (HasColliderAtPosition(position, out var hitGo))
                        {
                            // 该位置有物体，销毁物体
                            Undo.DestroyObjectImmediate(hitGo);
                            currentEvent.Use();
                        }
                    }
                    break;
                default:
                    break;
            }

            DrawPositionPreview(currentEvent.mousePosition);
        }

        private void SpawnPrefabAtPosition(Vector2 position)
        {
            if (m_selectedIndex < 0 || m_selectedIndex >= m_prefabList.Count)
            {
                Debug.LogWarning("Select prefab first");
                return;
            }

            var prefab = PrefabUtility.InstantiatePrefab(m_prefabList[m_selectedIndex]) as GameObject;
            if (prefab == null)
            {
                Debug.LogError("Fail to instantiate");
                return;
            }
            prefab.transform.position = position;

            Undo.RegisterCreatedObjectUndo(prefab, "Spawn Prefab");
        }

        private void DrawPositionPreview(Vector2 mousePosition)
        {
            Vector2 worldPos = GetWorldPosition2D(mousePosition);

            Handles.color = Color.cyan;
            Handles.DrawWireCube(worldPos, Vector2.one);

            // 显示坐标标签
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            Handles.Label(worldPos, $"({worldPos.x:F1}, {worldPos.y:F1})", style);
        }

        private bool HasColliderAtPosition(Vector2 position, out GameObject hitGo)
        {
            Collider2D hit = Physics2D.OverlapPoint(position);
            hitGo = hit?.gameObject;
            return hit != null;
        }

        private Vector2 GetWorldPosition2D(Vector2 mousePosition)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
            return Vector2Int.RoundToInt(new Vector2(ray.origin.x, ray.origin.y));
        }

        private void DrawPrefabListGUI()
        {
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

            // prefab list
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
                            if (m_selectedIndex != i)
                            {
                                // 选中该元素
                                m_selectedIndex = i;
                            }
                            else
                            {
                                // 如果本来选中了该元素，取消选择
                                m_selectedIndex = -1;
                            }
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
        }

        private void DrawToolBarGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Tool Bar: ");
            string[] toolNames = new string[] { "单次实例化", "持续实例化", "删除实体" };
            Color originalColor = GUI.color;
            Color selectedColor = Color.green;
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < toolNames.Length; i++)
            {
                if (m_toolIndex == i)
                {
                    GUI.backgroundColor = selectedColor;
                }
                if (GUILayout.Button(toolNames[i]))
                {
                    if (m_toolIndex == i)
                    {
                        // 取消选择
                        m_toolIndex = -1;
                    }
                    else
                    {
                        // 选择
                        m_toolIndex = i;
                    }
                }
                GUI.backgroundColor = originalColor;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
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
    }
}
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
        private int m_toolIndex = -1;

        [MenuItem("Tools/MapEditorWindow")]
        public static void ShowWindow()
        {
            GetWindow<MapEditorWindow>();
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += SceneView_DuringSceneGui;

            RefreshLogicTypeNames();
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= SceneView_DuringSceneGui;
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
            base.OnGUI();

            // ������
            DrawToolBarGUI();

            // ��ʾѡ�е�Ԥ�������Ϣ
            DrawSelectedPrefabInfoGUI();

            // ��ʾ�洢��Ԥ����
            DrawPrefabListGUI();

            // ������ק�¼�
            HandleDragAndDrop();
        }

        private void SceneView_DuringSceneGui(SceneView obj)
        {
            Event currentEvent = Event.current;
            bool isLeft = currentEvent.button == 0;

            switch (m_toolIndex)
            {
                case 0:
                    // ����ʵ����
                    if (currentEvent.type == EventType.MouseDown && isLeft)
                    {
                        // ����ʵ����
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (!HasColliderAtPosition(position, out _))
                        {
                            // ��λ��û�����壬��������
                            SpawnPrefabAtPosition(position);
                            currentEvent.Use();
                        }
                    }
                    break;
                case 1:
                    // ����ʵ����
                    if (isLeft &&
                        (currentEvent.type == EventType.MouseDown || currentEvent.type == EventType.MouseDrag))
                    {
                        // ����ʵ����
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (!HasColliderAtPosition(position, out _))
                        {
                            // ��λ��û�����壬��������
                            SpawnPrefabAtPosition(position);
                            currentEvent.Use();
                        }
                    }
                    break;
                case 2:
                    // ɾ��ʵ��
                    if (currentEvent.type == EventType.MouseDown && isLeft)
                    {
                        // ɾ��ʵ��
                        Vector2 position = GetWorldPosition2D(currentEvent.mousePosition);
                        if (HasColliderAtPosition(position, out var hitGo))
                        {
                            // ��λ�������壬��������
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

            // ��ʾ�����ǩ
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

            // prefab list
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
                            if (m_selectedIndex != i)
                            {
                                // ѡ�и�Ԫ��
                                m_selectedIndex = i;
                                InitSelectedPrefab();
                            }
                            else
                            {
                                // �������ѡ���˸�Ԫ�أ�ȡ��ѡ��
                                m_selectedIndex = -1;
                            }
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
        }

        private void DrawSelectedPrefabInfoGUI()
        {
            EditorGUILayout.BeginVertical("box");

            if (m_selectedIndex != -1)
            {
                GameObject selectedPrefab = m_prefabList[m_selectedIndex];
                if (selectedPrefab != null)
                {

                    if (selectedPrefab.TryGetComponent(out NounBlock nounBlock))
                    {
                        SerializedObject serializedObject = new(nounBlock);
                        SerializedProperty m_entity = serializedObject.FindProperty("m_entity");

                        // ��ʾ�����ó�ʼֵ
                        m_foldoutNounBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutNounBlock, $"{nameof(NounBlock)}: ");
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
                        m_foldoutPropertyBlock = EditorGUILayout.BeginFoldoutHeaderGroup(m_foldoutPropertyBlock, $"{nameof(PropertyBlock)}: ");
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

                }
                else
                {
                    EditorGUILayout.HelpBox("Select Null", MessageType.Info);
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawToolBarGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Tool Bar: ");
            string[] toolNames = new string[] { "����ʵ����", "����ʵ����", "ɾ��ʵ��" };
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
                        // ȡ��ѡ��
                        m_toolIndex = -1;
                    }
                    else
                    {
                        // ѡ��
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
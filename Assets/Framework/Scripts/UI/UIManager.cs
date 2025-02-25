using UnityEngine;
using System.Collections.Generic;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPool m_uiPool;

    private const string DEFAULT_GROUP = "Default";

    private Dictionary<string, UIGroup> m_uiGroups;

    private void Awake()
    {
        Init();

        GameEntry.RegisterUIManager(this);
    }

    public void OpenUI<T>() where T : UIForm
    {
        // ���� UIForm ʵ��
        string name = typeof(T).Name;
        UIForm uiForm = m_uiPool.Spawn(name);

        // ��ӵ���Ӧ�� UIGroup
        AddUIForm(DEFAULT_GROUP, uiForm);
    }

    public void CloseUI(UIForm uiform)
    {
        // �Ӷ�Ӧ�� UIGroup �Ƴ� UIForm
        RemoveUIForm(DEFAULT_GROUP, uiform);

        // ���� UIForm ʵ��
        m_uiPool.UnSpawn(uiform);
    }
    private void AddUIForm(string groupName, UIForm uiForm)
    {
        if (!m_uiGroups.ContainsKey(groupName))
        {
            m_uiGroups.Add(groupName, new UIGroup());
        }
        m_uiGroups[groupName].AddUIForm(uiForm);
    }

    private void RemoveUIForm(string groupName, UIForm uiForm)
    {
        if (!m_uiGroups.ContainsKey(groupName))
        {
            Debug.LogError("�����ڸ� Group: " + groupName);
            return;
        }

        m_uiGroups[groupName].RemoveUIForm(uiForm);
    }

    private void Init()
    {
        m_uiGroups = new();
    }
}

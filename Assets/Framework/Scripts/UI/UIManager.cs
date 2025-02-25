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
        // 创建 UIForm 实例
        string name = typeof(T).Name;
        UIForm uiForm = m_uiPool.Spawn(name);

        // 添加到对应的 UIGroup
        AddUIForm(DEFAULT_GROUP, uiForm);
    }

    public void CloseUI(UIForm uiform)
    {
        // 从对应的 UIGroup 移除 UIForm
        RemoveUIForm(DEFAULT_GROUP, uiform);

        // 销毁 UIForm 实例
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
            Debug.LogError("不存在该 Group: " + groupName);
            return;
        }

        m_uiGroups[groupName].RemoveUIForm(uiForm);
    }

    private void Init()
    {
        m_uiGroups = new();
    }
}

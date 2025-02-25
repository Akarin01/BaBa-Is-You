using UnityEngine;

public static class GameEntry
{
    private static UIManager m_uiManager;

    public static UIManager UIManager
    {
        get
        {
            if (m_uiManager == null)
            {
                Debug.LogError("UIManager is not registered.");
            }
            return m_uiManager;
        }
    }

    public static void RegisterUIManager(UIManager uiManager)
    {
        m_uiManager = uiManager;
    }
}
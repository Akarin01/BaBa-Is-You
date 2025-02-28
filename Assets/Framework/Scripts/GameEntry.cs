using UnityEngine;

namespace KitaFramework
{
    public static class GameEntry
    {
        private static UIManager m_uiManager;
        private static ObjectPoolManager m_objectPoolManager;

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

        public static ObjectPoolManager ObjectPoolManager
        {
            get
            {
                if (m_objectPoolManager == null)
                {
                    Debug.LogError("ObjectPoolManager is not registered.");
                }
                return m_objectPoolManager;
            }
        }

        public static void RegisterUIManager(UIManager uiManager)
        {
            m_uiManager = uiManager;
        }

        public static void RegisterObjectPoolManager(ObjectPoolManager objectPoolManager)
        {
            m_objectPoolManager = objectPoolManager;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public static class FrameworkEntry
    {
        private static LinkedList<FrameworkManager> m_frameworkManagers = new();

        public static void RegisterManager(FrameworkManager frameworkManager)
        {
            if (frameworkManager == null)
            {
                Debug.LogError("Framework Manager is invalid");
                return;
            }
            Type t = frameworkManager.GetType();
            foreach (var manager in m_frameworkManagers)
            {
                if (manager.GetType() == t)
                {
                    Debug.LogError($"Framework Manager {manager} is registered");
                    return;
                }
            }

            m_frameworkManagers.AddLast(frameworkManager);
        }

        public static T GetManager<T>() where T : FrameworkManager
        {
            return (T)GetManager(typeof(T));
        }

        public static FrameworkManager GetManager(Type t)
        {
            foreach (var manager in m_frameworkManagers)
            {
                if (manager.GetType() == t)
                {
                    return manager;
                }
            }
            Debug.LogError($"Framework Manager {t} is not registered");
            return null;
        }
    }
}
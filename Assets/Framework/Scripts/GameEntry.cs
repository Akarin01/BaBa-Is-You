using UnityEngine;
using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public static class GameEntry
    {
        private static Dictionary<Type, FrameworkManager> m_frameworkComponents = new();

        public static IUIManager UIManager => GetManager<UIManager>();

        public static IObjectPoolManager ObjectPoolManager => GetManager<ObjectPoolManager>();

        public static void RegisterManager<T>(T frameworkComponent) where T : FrameworkManager
        {
            Type t = typeof(T);
            if (!m_frameworkComponents.TryAdd(t, frameworkComponent))
            {
                Debug.LogError($"{frameworkComponent}����Ѿ�����");
            }
        }

        public static T GetManager<T>() where T : FrameworkManager
        {
            Type t = typeof(T);
            if (!m_frameworkComponents.ContainsKey(t))
            {
                Debug.LogError($"{t}���û��ע��");
                return null;
            }
            return (T)m_frameworkComponents[t];
        }
    }
}
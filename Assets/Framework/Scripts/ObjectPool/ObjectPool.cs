﻿using System.Collections.Generic;
using UnityEngine;

namespace KitaFramework
{
    public partial class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
    {
        private Dictionary<string, LinkedList<ObjectInfo>> m_targets = new();
        private Dictionary<object, ObjectInfo> m_objectmaps = new();

        public void Register(T obj, bool isUsed)
        {
            ObjectInfo objectInfo = new ObjectInfo();
            objectInfo.Init(obj, isUsed);
            string name = obj.Name;
            if (!m_targets.ContainsKey(name))
            {
                m_targets[name] = new LinkedList<ObjectInfo>();
            }
            m_targets[name].AddFirst(objectInfo);
            if (!m_objectmaps.TryAdd(obj.Target, objectInfo))
            {
                Debug.LogError($"对象 {obj.Target} 已注册， 禁止重复注册");
            }
        }

        public override void Shutdown()
        {
            foreach (var map in m_objectmaps)
            {
                map.Value.Release();
            }

            m_objectmaps.Clear();
            m_targets.Clear();
        }

        public T Spawn(string name)
        {
            if (!m_targets.ContainsKey(name))
            {
                return null;
            }
            LinkedList<ObjectInfo> objectInfos = m_targets[name];
            foreach (ObjectInfo objectInfo in objectInfos)
            {
                if (!objectInfo.IsUsed())
                {
                    return objectInfo.Spawn();
                }
            }
            return null;
        }

        public void Unspawn(object obj)
        {
            if (!m_objectmaps.ContainsKey(obj))
            {
                Debug.LogError("该对象不由对象池管理");
                return;
            }

            ObjectInfo objectInfo = m_objectmaps[obj];
            objectInfo.Unspawn();
        }
    }
}
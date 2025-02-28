using System.Collections.Generic;
using UnityEngine;
using System;

namespace KitaFramework
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private Dictionary<Type, ObjectPoolBase> m_objectPools;

        private void Awake()
        {
            Init();

            GameEntry.RegisterObjectPoolManager(this);
        }

        public IObjectPool<T> GetObjectPool<T>() where T : ObjectBase
        {
            Type t = typeof(T);
            ObjectPoolBase objectPoolBase = null;
            m_objectPools.TryGetValue(t, out objectPoolBase);
            return (IObjectPool<T>)objectPoolBase;
        }

        public IObjectPool<T> CreateObjectPool<T>() where T : ObjectBase
        {
            Type t = typeof(T);
            ObjectPoolBase objectPoolBase = new ObjectPool<T>();
            m_objectPools.Add(t, objectPoolBase);
            return (IObjectPool<T>)objectPoolBase;
        }

        private void Init()
        {
            m_objectPools = new();
        }
    }
}
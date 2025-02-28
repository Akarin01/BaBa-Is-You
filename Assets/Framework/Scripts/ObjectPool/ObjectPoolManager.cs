using System.Collections.Generic;
using UnityEngine;
using System;

namespace KitaFramework
{
    public class ObjectPoolManager : FrameworkManager, IObjectPoolManager
    {
        private Dictionary<Type, ObjectPoolBase> m_objectPools;

        protected override void Awake()
        {
            base.Awake();

            GameEntry.RegisterManager(this);
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

        public override void Shutdown()
        {
            base.Shutdown();

            foreach (var objectPool in m_objectPools)
            {
                objectPool.Value.Shutdown();
            }

            m_objectPools.Clear();
        }

        protected override void Init()
        {
            m_objectPools = new();
        }
    }
}
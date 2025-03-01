using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public class ObjectPoolManager : FrameworkManager
    {
        private Dictionary<Type, ObjectPoolBase> m_objectPools;

        protected override void Awake()
        {
            base.Awake();

            m_objectPools = new();
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

        public void Release()
        {
            foreach (var objectPool in m_objectPools)
            {
                objectPool.Value.Shutdown();
            }

            m_objectPools.Clear();
        }
    }
}
namespace KitaFramework
{
    public partial class ObjectPool<T> where T : ObjectBase
    {
        /// <summary>
        /// Object 信息类，存储对象池内对象使用情况的信息
        /// </summary>
        private class ObjectInfo
        {
            private T m_target;
            private bool m_isUsed;

            public ObjectInfo(T target, bool isUsed)
            {
                m_target = target;
                m_isUsed = isUsed;
            }

            public string Name => m_target.Name;

            public T Target => m_target;

            public bool IsUsed() => m_isUsed;

            public T Spawn()
            {
                m_isUsed = true;
                m_target.OnSpawn();
                return m_target;
            }

            public void Unspawn() 
            {
                m_isUsed = false;
                m_target.OnUnspawn();
            }

            /// <summary>
            /// 释放资源
            /// </summary>
            public void Release()
            {
                m_target.Release();
            }
        }
    }
}
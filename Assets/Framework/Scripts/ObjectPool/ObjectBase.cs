namespace KitaFramework
{
    /// <summary>
    /// ObjectBase 类，对象池存储的对象的包装类，包装了对象的名字与生命周期方法
    /// </summary>
    public abstract class ObjectBase
    {
        private object m_target;
        private string m_name;

        public object Target { get => m_target; }
        public string Name { get => m_name; }

        public ObjectBase(object target, string name)
        {
            m_target = target;
            m_name = name;
        }

        public virtual void OnSpawn()
        {
        }

        public virtual void OnUnspawn()
        {
        }

        public virtual void Release()
        {
        }
    }
}
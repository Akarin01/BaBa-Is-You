namespace KitaFramework
{
    public interface IObjectPoolManager
    {
        public IObjectPool<T> GetObjectPool<T>() where T : ObjectBase;

        public IObjectPool<T> CreateObjectPool<T>() where T : ObjectBase;
    }
}
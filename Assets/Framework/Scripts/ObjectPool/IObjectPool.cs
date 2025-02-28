namespace KitaFramework
{
    public interface IObjectPool<T> where T : ObjectBase
    {
        public void Register(T obj, bool isUsed);

        public T Spawn(string name);

        public void Unspawn(object obj);
    }
}
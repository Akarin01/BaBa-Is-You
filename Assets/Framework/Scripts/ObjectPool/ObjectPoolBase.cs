namespace KitaFramework
{
    public abstract class ObjectPoolBase
    {
        /// <summary>
        /// 关闭对象池，清理资源
        /// </summary>
        public abstract void Shutdown();
    }
}
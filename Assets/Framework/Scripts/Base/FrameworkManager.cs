using UnityEngine;

namespace KitaFramework
{
    /// <summary>
    /// 框架模块基类
    /// </summary>
    public abstract class FrameworkManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            FrameworkEntry.RegisterManager(this);
        }

        public abstract void Shutdown();
    }
}
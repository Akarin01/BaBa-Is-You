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
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        { }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Shutdown()
        { }
    }
}
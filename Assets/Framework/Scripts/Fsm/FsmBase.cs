namespace KitaFramework
{
    /// <summary>
    /// 状态机基类
    /// </summary>
    public abstract class FsmBase
    {
        /// <summary>
        /// 状态机的名字
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 状态机是否正在运行
        /// </summary>
        public abstract bool IsRunning { get; }

        /// <summary>
        /// 帧更新
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="realDeltaTime"></param>
        internal abstract void OnUpdate(float deltaTime, float realDeltaTime);

        /// <summary>
        /// 关闭状态机，清理资源
        /// </summary>
        internal abstract void Shutdown();
    }
}
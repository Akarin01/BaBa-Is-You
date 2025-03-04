using System;

namespace KitaFramework
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    /// <typeparam name="T">状态机的持有者</typeparam>
    public interface IFsm<T> where T : class
    {
        /// <summary>
        /// 状态机的持有者
        /// </summary>
        public T Owner { get; }

        /// <summary>
        /// 状态机的名字
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 以某个状态开始状态机
        /// </summary>
        /// <typeparam name="TState">初始的状态类型</typeparam>
        public void Start<TState>() where TState : FsmState<T>;

        /// <summary>
        /// 以某个状态开始状态机
        /// </summary>
        /// <param name="stateType">初始的状态类型</param>
        public void Start(Type stateType);

        /// <summary>
        /// 设置某个数据
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="value">值</param>
        public void SetData(string name, object value);

        /// <summary>
        /// 获取某个数据
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns>值</returns>
        public object GetData(string name);

        /// <summary>
        /// 是否存在某个数据
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns>是否存在</returns>
        public bool HasData(string name);

        /// <summary>
        /// 移除某个数据
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveData(string name);
    }
}
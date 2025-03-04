using System;

namespace KitaFramework
{
    /// <summary>
    /// 状态机状态基类，定义状态生命周期方法
    /// </summary>
    /// <typeparam name="T">状态机的持有者</typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// 初始化的生命周期方法
        /// </summary>
        /// <param name="fsm">状态机</param>
        protected internal virtual void OnInit(IFsm<T> fsm)
        {

        }

        /// <summary>
        /// 进入状态的生命周期方法
        /// </summary>
        /// <param name="fsm">状态机</param>
        protected internal virtual void OnEnter(IFsm<T> fsm)
        {

        }

        /// <summary>
        /// 帧更新的生命周期方法
        /// </summary>
        /// <param name="fsm">状态机</param>
        /// <param name="deltaTime">帧更新间隔</param>
        /// <param name="realDeltaTime">帧更新实际间隔</param>
        protected internal virtual void OnUpdate(IFsm<T> fsm, float deltaTime, float realDeltaTime)
        {

        }

        /// <summary>
        /// 退出状态的生命周期方法
        /// </summary>
        /// <param name="fsm">状态机</param>
        /// <param name="isShutdown">是否是状态机关闭时调用</param>
        protected internal virtual void OnExit(IFsm<T> fsm, bool isShutdown)
        {

        }

        /// <summary>
        /// 被释放的生命周期方法
        /// </summary>
        /// <param name="fsm">状态机</param>
        protected internal virtual void OnRelease(IFsm<T> fsm)
        {

        }

        protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new ArgumentException($"{fsm} is invalid");
            }
            fsmImplement.ChangeState<TState>();
        }

        protected void ChangeState(IFsm<T> fsm, Type stateType)
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new ArgumentException($"{fsm} is invalid");
            }
            fsmImplement.ChangeState(stateType);
        }
    }
}
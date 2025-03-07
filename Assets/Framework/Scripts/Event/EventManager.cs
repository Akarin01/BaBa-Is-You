using System;
using System.Collections.Generic;

namespace KitaFramework
{
    public class EventManager : FrameworkManager
    {
        private Dictionary<int, EventHandler<BaseEventArgs>> m_events;

        protected override void Awake()
        {
            base.Awake();

            m_events = new();
        }
        public void Subscribe(int eventID, EventHandler<BaseEventArgs> handler)
        {
            if (InternalCheck(eventID, handler))
            {
                throw new Exception("Handler has been subscribed into the event");
            }

            if (!m_events.ContainsKey(eventID))
            {
                // 不存在该事件，初始化事件
                m_events[eventID] = (object sender, BaseEventArgs e) => { };
            }
            m_events[eventID] += handler;
        }

        public void Unsubscribe(int eventID, EventHandler<BaseEventArgs> handler)
        {
            if (!InternalCheck(eventID, handler))
            {
                throw new Exception("Handler has not been subscribed into the event");
            }

            m_events[eventID] -= handler;
        }

        public bool Check(int eventID, EventHandler<BaseEventArgs> handler)
        {
            return InternalCheck(eventID, handler);
        }

        public void Fire(int eventID, object sender, BaseEventArgs e)
        {
            if (!m_events.ContainsKey(eventID))
            {
                return;
            }

            m_events[eventID].Invoke(sender, e);
        }

        public override void Shutdown()
        {
            m_events.Clear();
        }

        /// <summary>
        /// 检查是否已经订阅事件的内部方法
        /// </summary>
        /// <param name="eventID">事件的 ID</param>
        /// <param name="handler">事件的响应方法</param>
        /// <returns>是否已经订阅该事件</returns>
        private bool InternalCheck(int eventID, EventHandler<BaseEventArgs> handler)
        {
            if (!m_events.ContainsKey(eventID))
            {
                // 如果不存在该事件，返回 false
                return false;
            }

            foreach (var h in m_events[eventID].GetInvocationList())
            {
                // 检查是否已经注册该事件
                if (h.Equals(handler))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
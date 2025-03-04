using System;
using System.Collections.Generic;
using UnityEngine;
using TypeNamePair = System.Collections.Generic.KeyValuePair<System.Type, string>;

namespace KitaFramework
{
    public class FsmManager : FrameworkManager
    {
        private Dictionary<TypeNamePair, FsmBase> m_fsms;
        private List<FsmBase> m_tempFsms;

        protected override void Awake()
        {
            base.Awake();

            m_fsms = new();
            m_tempFsms = new();
        }

        private void Update()
        {
            if (m_fsms.Count == 0)
            {
                return;
            }

            m_tempFsms.Clear();
            foreach (var fsm in m_fsms.Values)
            {
                m_tempFsms.Add(fsm);
            }

            foreach (var fsm in m_tempFsms)
            {
                if (!fsm.IsRunning)
                {
                    continue;
                }
                fsm.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        public IFsm<T> CreateFsm<T>(T owner, string name, params FsmState<T>[] states) where T : class
        {
            TypeNamePair key = new TypeNamePair(typeof(T), name);
            if (HasFsm<T>(name))
            {
                throw new Exception($"{key} fsm has exsited");
            }
            Fsm<T> fsm = new(owner, name, states);
            m_fsms.Add(key, fsm);
            return fsm;
        }

        public bool HasFsm<T>(string name) where T : class
        {
            TypeNamePair key = new TypeNamePair(typeof(T), name);
            return m_fsms.ContainsKey(key);
        }

        public IFsm<T> GetFsm<T>(string name) where T : class
        {
            TypeNamePair key = new TypeNamePair(typeof(T), name);
            m_fsms.TryGetValue(key, out var fsm);
            return (IFsm<T>)fsm;
        }

        public bool RemoveFsm<T>(string name) where T : class
        {
            TypeNamePair key = new TypeNamePair(typeof(T), name);
            if (m_fsms.TryGetValue (key, out var fsm))
            {
                fsm.Shutdown();
            }
            return m_fsms.Remove(key);
        }
    }
}
using System;
using System.Collections.Generic;

namespace KitaFramework
{
    internal sealed class Fsm<T> : FsmBase, IFsm<T> where T : class
    {
        private Dictionary<Type, FsmState<T>> m_states;
        private Dictionary<string, object> m_datas;
        private T m_owner;
        private string m_name;
        private bool m_isRunning;
        private FsmState<T> m_currentState;

        public T Owner => m_owner;

        public string Name => m_name;

        public override bool IsRunning => m_isRunning;

        public Fsm(T owner, string name, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }
            if (states == null || states.Length == 0)
            {
                throw new ArgumentException($"{nameof(states)} is null or empty");
            }

            m_owner = owner;
            m_name = name;
            m_states = new();
            m_datas = new();
            foreach (var state in states)
            {
                if (state == null)
                {
                    throw new ArgumentNullException(nameof(state));
                }

                Type stateType = state.GetType();
                if (m_states.ContainsKey(stateType))
                {
                    throw new Exception($"{state} has existed");
                }
                m_states.Add(stateType, state);
                state.OnInit(this);
            }
        }

        public object GetData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            m_datas.TryGetValue(name, out object data);
            return data;
        }

        public bool HasData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return m_datas.ContainsKey(name);
        }

        public bool RemoveData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return m_datas.Remove(name);
        }

        public void SetData(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            m_datas[name] = value;
        }

        public void Start<TState>() where TState : FsmState<T>
        {
            if (m_isRunning)
            {
                throw new Exception("Fsm has been running");
            }

            TState state = GetState<TState>();
            if (state == null)
            {
                throw new ArgumentException($"{typeof(TState)} has not exsited");
            }

            m_currentState = state;
            m_isRunning = true;
            m_currentState.OnEnter(this);
        }

        public void Start(Type stateType)
        {
            if (m_isRunning)
            {
                throw new Exception("Fsm has been running");
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new ArgumentException($"{stateType} has not exsited");
            }

            m_currentState = state;
            m_isRunning = true;
            m_currentState.OnEnter(this);
        }

        internal override void OnUpdate(float deltaTime, float realDeltaTime)
        {
            if (m_currentState == null)
            {
                return;
            }

            m_currentState.OnUpdate(this, deltaTime, realDeltaTime);
        }

        internal override void Shutdown()
        {
            if (m_currentState != null)
            {
                m_currentState.OnExit(this, true);
            }

            foreach (var state in m_states.Values)
            {
                state.OnRelease(this);
            }

            m_states.Clear();
            m_datas.Clear();
            m_owner = null;
            m_name = null;
            m_isRunning = false;
            m_currentState = null;
        }

        private TState GetState<TState>() where TState: FsmState<T>
        {
            m_states.TryGetValue(typeof(TState), out var state);
            return (TState)state;
        }

        private FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new ArgumentNullException(nameof(stateType));
            }
            if (!stateType.IsAssignableFrom(typeof(FsmState<T>)))
            {
                throw new ArgumentException($"{stateType} is invalid");
            }

            m_states.TryGetValue(stateType, out var state);
            return state;
        }

        internal void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        internal void ChangeState(Type stateType)
        {
            if (m_currentState != null)
            {
                m_currentState.OnExit(this, false);
            }
            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new ArgumentException($"{stateType} has not exsited");
            }
            m_currentState = state;
            m_currentState.OnEnter(this);
        }
    }
}
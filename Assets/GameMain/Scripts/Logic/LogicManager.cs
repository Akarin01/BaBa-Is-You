using System;
using System.Collections.Generic;

namespace BabaIsYou
{
    /// <summary>
    /// �߼��������࣬������Ԫģʽ�����߼���
    /// </summary>
    public class LogicManager
    {
        private static readonly Dictionary<Type, LogicBase> s_logicDict = new();

        public static LogicBase GetLogic<T>() where T : LogicBase, new()
        {
            Type t = typeof(T);
            if (s_logicDict.ContainsKey(t))
            {
                return s_logicDict[t];
            }
            // ������ʵ��
            LogicBase logic = Activator.CreateInstance<T>();
            s_logicDict.Add(t, logic);
            return logic;
        }

        public static LogicBase GetLogic(Type t)
        {
            if (!t.IsSubclassOf(typeof(LogicBase)))
            {
                throw new ArgumentException("Type must be subclass of LogicBase");
            }
            if (s_logicDict.ContainsKey(t))
            {
                return s_logicDict[t];
            }
            // ������ʵ��
            LogicBase logic = Activator.CreateInstance(t) as LogicBase;
            s_logicDict.Add(t, logic);
            return logic;
        }

        public static LogicBase GetLogic(string typeStr)
        {
            string namespaceName = typeof(LogicBase).Namespace;
            typeStr = string.Concat(namespaceName, ".", typeStr);
            Type t = Type.GetType(typeStr);
            if (t == null)
            {
                throw new ArgumentException($"Type {typeStr} not found");
            }
            return GetLogic(t);
        }

        public static LogicBase GetDefaultLogic() => GetLogic<DefaultLogic>();
    }
}
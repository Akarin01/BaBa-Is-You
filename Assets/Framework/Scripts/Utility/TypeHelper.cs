using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public static class TypeHelper
    {
        /// <summary>
        /// 获取指定基类的子类类型名称
        /// </summary>
        /// <param name="baseType">指定基类</param>
        /// <returns>子类名字数组</returns>
        public static string[] GetTypeNames(Type baseType)
        {
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            List<string> typeNames = new();

            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                    {
                        typeNames.Add(type.FullName);
                    }
                }
            }

            typeNames.Sort();
            return typeNames.ToArray();
        }
    }
}
using System.Collections.Generic;
using System;
using TypeNamePair = System.Collections.Generic.KeyValuePair<System.Type, string>;

namespace KitaFramework
{
    public class DataTableManager : FrameworkManager
    {
        private Dictionary<TypeNamePair, DataTableBase> m_dataTables;

        protected override void Awake()
        {
            base.Awake();

            m_dataTables = new();
        }

        public IDataTable<T> CreateDataTable<T>(string name = null) where T : DataRowBase, new()
        {
            TypeNamePair key = new(typeof(T), name);
            if (m_dataTables.ContainsKey(key))
            {
                throw new ArgumentException("Data table has been created");
            }
            DataTableBase dataTable = new DataTable<T>(name);
            m_dataTables.Add(key, dataTable);
            return (IDataTable<T>)dataTable;
        }

        public IDataTable<T> GetDataTable<T>(string name = null) where T : DataRowBase
        {
            TypeNamePair key = new(typeof(T), name);
            m_dataTables.TryGetValue(key, out DataTableBase dataTable);
            return (IDataTable<T>)dataTable;
        }

        public bool HasDataTable<T>(string name = null) where T : DataRowBase
        {
            TypeNamePair key = new(typeof(T), name);
            return m_dataTables.ContainsKey(key);
        }

        public bool RemoveDataTable<T>(string name = null) where T :DataRowBase
        {
            TypeNamePair key = new(typeof(T), name);
            return m_dataTables.Remove(key);
        }

        public override void Shutdown()
        {
            m_dataTables.Clear();
        }
    }
}
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace KitaFramework
{
    public class DataTable<T> : DataTableBase, IDataTable<T> where T : DataRowBase, new()
    {
        private string m_dataTableName;
        private Dictionary<int, T> m_dataRows;

        public string Name => m_dataTableName;

        public DataTable(string tableName)
        {
            m_dataTableName = tableName;
            m_dataRows = new();
        }

        public T GetDataRow(int id)
        {
            m_dataRows.TryGetValue(id, out T dataRow);
            return dataRow;
        }

        public void ReadData(string fileName)
        {
            // 读取指定 .csv 文件，并初始化 m_dataRows
            fileName += ".csv";
            string path = Path.Combine(Application.streamingAssetsPath, fileName);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    int cnt = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ++cnt;
                        if (line.StartsWith('#'))
                        {
                            // # 开头的注释行
                            continue;
                        }

                        T row = new();
                        if (!row.ParseRow(line))
                        {
                            Debug.Log($"Line {cnt} failed to parse");
                            continue;
                        }

                        if (m_dataRows.ContainsKey(row.Id))
                        {
                            throw new Exception($"ID {row.Id} has been existed");
                        }
                        m_dataRows.Add(row.Id, row);
                    }
                }
            }
        }
    }
}
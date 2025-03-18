# DataTable\<T\>
```
public class DataTable<T> : DataTableBase, IDataTable<T> where T : DataRowBase, new();
```
- **描述**
    数据表类
> [DataTableBase](./DataTableBase.md)
> [IDataTable](./IDataTable.md)

## 成员变量
#### m_dataTableName
```
private string m_dataTableName;
```
- **描述**
    数据表的名字
<br>

#### m_dataRows
```
private Dictionary<int, T> m_dataRows;
```
- **描述**
    以<id, 数据表行>为键值对存储的字典
<br>

## 属性
#### Name
```
public string Name { get; }
```
- **具体实现**
    返回 m_dataTableName 字段

## public 方法
#### 构造方法
```
public DataTable(string tableName);
```
- **参数**
    |   参数    |     描述     |
    | :-------: | :----------: |
    | tableName | 数据表的名字 |
- **具体实现**
    初始化 m_dataTableName 和 m_dataRows
<br>

#### GetDataRow
```
public T GetDataRow(int id);
```
- **具实现**
    调用 m_dataRows 的 TryGetValue 方法
<br>

#### ReadData
```
public void ReadData(string fileName);
```
- **异常**
    如果解析数据表行失败，打印日志；如果包含重复的 id，抛出异常
- **具体实现**
    读取指定 .csv 文件，逐行读取并构造数据表行；跳过以 # 开头的注释行
<br>
# DataTableManager
```
public class DataTableManager : FrameworkManager;
```
- **描述**
    DataTable 管理器，管理已经创建的 DataTable
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_dataTables
```
private Dictionary<TypeNamePair, DataTableBase> m_dataTables;
```
> ```TypeNamePair``` 为 ```KeyValuePair<Type, string>```
- **描述**
    以<<数据表行类型，数据表名字名字>，数据表>存储的字典
<br>

## public 方法
#### CreateDataTable
```
public IDataTable<T> CreateDataTable<T>(string name = null) where T : DataRowBase, new();
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 数据表行的类型 |
    | name  |  数据表的名字  |
- **返回**
    返回新创建的数据表
- **描述**
    创建数据表的方法
- **异常**
    如果已经存在该数据表，抛出异常
<br>

#### GetDataTable
```
public IDataTable<T> GetDataTable<T>(string name = null) where T : DataRowBase;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 数据表行的类型 |
    | name  |  数据表的名字  |
- **返回**
    返回指定的数据表；如果不存在，返回 null
- **描述**
    获取数据表的方法
<br>

#### HasDataTable
```
public bool HasDataTable<T>(string name = null) where T : DataRowBase;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 数据表行的类型 |
    | name  |  数据表的名字  |
- **返回**
    是否存在指定的数据表
- **描述**
    判断是否存在指定数据表的方法
<br>

#### RemoveDataTable
```
public bool RemoveDataTable<T>(string name = null) where T :DataRowBase;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 数据表行的类型 |
    | name  |  数据表的名字  |
- **返回**
    是否成功移除指定数据表
- **描述**
    移除数据表的方法
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    调用 m_dataTables 的 Clear 方法
<br>
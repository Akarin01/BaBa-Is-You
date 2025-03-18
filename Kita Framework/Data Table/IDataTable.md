# IDataTable\<T\>
```
public interface IDataTable<T> where T : DataRowBase;
```
- **描述**
    数据表接口，提供数据表对外的接口

## 属性
#### Name
```
public string Name { get; }
```
- **描述**
    数据表的名字

## public 方法
#### GetDataRow
```
public T GetDataRow(int id);
```
- **参数**
    | 参数  |     描述      |
    | :---: | :-----------: |
    |  id   | 数据表行的 id |
- **返回**
    对应的数据表行；如果不存在，返回 null
- **描述**
    获取 id 对应的数据表行
<br>

#### ReadData
```
public void ReadData(string fileName);
```
- **参数**
    |   参数   |        描述        |
    | :------: | :----------------: |
    | fileName | 存储数据表的文件名 |
- **描述**
    读取指定文件，并初始化数据表
<br>
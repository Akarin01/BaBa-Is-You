# DataRowBase
```
public abstract class DataRowBase;
```
- **描述**
    数据表行基类

## 属性
#### Id
```
public abstract int Id { get; }
```
- **描述**
    数据表行中的 Id 数据

## public 方法
#### ParseRow
```
public abstract bool ParseRow(string line);
```
- **参数**
    | 参数  |          描述          |
    | :---: | :--------------------: |
    | line  | 待解析的数据表行字符串 |
- **返回**
    是否解析成功
- **描述**
    解析字符串，并初始化数据表行的各个数据
<br>
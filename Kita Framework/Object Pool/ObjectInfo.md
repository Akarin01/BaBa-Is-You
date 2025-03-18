# ObjectInfo
```
public class ObjectPool<T> where T : ObjectBase
{
    private class ObjectInfo;
}
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **描述**
    对象信息类，是对象池的内部类，存储对象池中对象使用情况的信息
    是一层对象的包装

## 成员变量
#### m_target
```
private T m_target;
```
- **描述**
    对象池存储的对象
<br>

#### m_isUsed
```
private bool m_isUsed;
```
- **描述**
    对象是否正在被使用
<br>

## 属性
#### Name
```
public string Name { get; }
```
- **描述**
    对象的名字
<br>

#### Target
```
public T Target { get; }
```
- **描述**
    对象

## public 方法
#### IsUsed
```
public bool IsUsed();
```
- **返回**
    返回对象是否正在被使用
- **描述**
    确认对象是否正在被使用的方法
<br>

#### Init
```
public void Init(T target, bool isUsed);
```
- **参数**
    |  参数  |         描述         |
    | :----: | :------------------: |
    | target |   对象池存储的对象   |
    | isUsed | 该对象是否立刻被使用 |
- **描述**
    初始化方法
<br>

#### Spawn
```
public T Spawn();
```
- **返回**
    返回 ObjectInfo 包装的对象
- **描述**
    对象池提供对象时调用的方法，修改 ObjectInfo 内部的状态
<br>

#### Unspawn
```
public void Unspawn() ;
```
- **描述**
    归还对象池对象时调用的方法，修改 ObjectInfo 内部的状态
<br>

#### Release
```
public void Release();
```
- **描述**
    释放 ObjectInfo 实例，并调用包装对象的 Release 方法
<br>
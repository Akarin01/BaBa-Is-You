# ObjectPool\<T\>
```
public class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **描述**
    对象池类
> [ObjectPoolBase](./ObjectPoolBase.md)
> [IObjectPool](./IObjectPool.md)

## 成员变量
#### m_targets
```
private Dictionary<string, LinkedList<ObjectInfo>> m_targets;
```
- **描述**
    存储<名字，对象池对象信息链表>的字典
<br>

#### m_objectmaps
```
private Dictionary<object, ObjectInfo> m_objectmaps;
```
- **描述**
    存储<对象池对象，对象池对象信息>的字典
<br>

## public 方法
#### Register
```
public void Register(T obj, bool isUsed);
```
- **实现逻辑**
    创建 obj 对应的 ObjectInfo 实例，并存储进两个字典
- **异常**
    如果 obj 已经注册过，打印错误日志
<br>

#### Spawn
```
public T Spawn(string name);
```
- **实现逻辑**
    根据 name 查找对应的链表，遍历寻找第一个可使用的对象
<br>

#### Unspawn
```
public void Unspawn(object obj);
```
- **实现逻辑**
    根据 m_objectmaps 查找 obj 对应的 ObjectInfo 实例
- **异常**
    如果 obj 不由该对象池管理，打印错误日志
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现逻辑**
    释放每个 ObjectInfo 实例，并清除字典
<br>
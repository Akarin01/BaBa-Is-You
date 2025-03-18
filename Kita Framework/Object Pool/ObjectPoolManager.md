# ObjectPoolManager
```
public class ObjectPoolManager : FrameworkManager;
```
- **描述**
    ObjectPool 管理器类，管理存在的对象池
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_objectPools
```
private Dictionary<Type, ObjectPoolBase> m_objectPools;
```
- **描述**
    存储<对象类型，对象池>的字典
<br>

## public 方法
#### GetObjectPool
```
public IObjectPool<T> GetObjectPool<T>() where T : ObjectBase;
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **返回**
    如果存在，返回存储指定类型对象的对象池；否则，返回 null
- **描述**
    获取对象池的方法，不保证对象池一定存在
<br>

#### HasObjectPool
```
public bool HasObjectPool<T>() where T : ObjectBase;
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **返回**
    返回指定类型对象的对象池是否存在
- **描述**
    确认对象池是否存在的方法
<br>
        
#### CreateObjectPool
```
public IObjectPool<T> CreateObjectPool<T>() where T : ObjectBase;
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **返回**
    创造的对象池
- **描述**
    创造对象池的方法，并返回创造的对象池
<br>

#### Shutdown
```
public override void Shutdown()
```
- **描述**
    关闭对象池管理器，清理资源。会调用每个对象池的 Shutdown 方法
<br>
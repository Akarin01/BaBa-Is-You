# IObjectPool\<T\>
```
public interface IObjectPool<T> where T : ObjectBase;
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    |   T   | 对象池存储对象的类型 |
- **描述**
    对象池接口，提供外界访问对象的能力

## public 方法
#### Register
``` 
public void Register(T obj, bool isUsed);
```
- **参数**
    |  参数  |           描述           |
    | :----: | :----------------------: |
    |  obj   |   向对象池中注册的对象   |
    | isUsed | 注册的对象是否立刻被使用 |
- **描述**
    向对象池注册对象的方法
<br>

#### Spawn
```
public T Spawn(string name);
```
- **参数**
    | 参数  |    描述    |
    | :---: | :--------: |
    | name  | 对象的名字 |
- **返回**
    如果存在，返回对应该名字的对象；否则，返回 null
- **描述**
    从对象池中获取对象的方法
<br>

#### Unspawn
```
public void Unspawn(object obj);
```
- **参数**
    | 参数  |        描述        |
    | :---: | :----------------: |
    |  obj  | 向对象池归还的对象 |
- **描述**
    向对象池归还对象的方法
<br>
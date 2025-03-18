# ObjectBase
```
public abstract class ObjectBase;
```
- **描述**
    对象池存储对象的基类，包装了实际存储的对象，并提供对象生命周期方法

## 成员变量
#### m_target
```
private object m_target;
```
- **描述**
    实际需要存储的对象
<br>

#### m_name
```
private string m_name;
```
- **描述**
    对象的名字
<br>

## 属性
#### Target
```
public object Target { get; }
```
- **描述**
    实际存储的对象
<br>

#### Name
```
public string Name { get; }
```
- **描述**
    对象的名字
<br>

## public 方法
#### Init
```
public virtual void Init(object target, string name);
```
- **参数**
    |  参数  |      描述      |
    | :----: | :------------: |
    | target | 实际存储的对象 |
    |  name  |   对象的名字   |
- **描述**
    初始化方法，初始化 m_target 与 n_name 成员变量

```
public virtual void Init(object target);
```
- **参数**
    |  参数  |      描述      |
    | :----: | :------------: |
    | target | 实际存储的对象 | S |
- **描述**
    初始化方法，初始化 m_Target 成员变量，将 
    m_name 初始化为 null
<br>

#### OnSpawn
```
public virtual void OnSpawn();
```
- **描述**
    获取对象池对象池时调用的生命周期方法
<br>

#### OnUnspawn
```
public virtual void OnUnspawn();
```
- **描述**
    归还对象池对象池时调用的生命周期方法
<br>

#### Release
```
public virtual void Release();
```
- **描述**
    释放对象
<br>
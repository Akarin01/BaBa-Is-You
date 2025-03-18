# FsmManager
```
public class FsmManager : FrameworkManager;
```
- **描述**
    Fsm 管理器类，存储并帧更新存在的状态机
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_fsms
```
private Dictionary<TypeNamePair, FsmBase> m_fsms;
```
> ```TypeNamePair``` 为 ```KeyValuePair<Type, string>```
- **描述**
    存储<<状态机持有者类型，状态机名字>，状态机>的状态机字典
<br>

#### m_tempFsms
```
private List<FsmBase> m_tempFsms;
```
- **描述**
    存储状态机集合的临时列表
<br>

## public 方法
#### CreateFsm
```
public IFsm<T> CreateFsm<T>(T owner, string name, params FsmState<T>[] states) where T : class;
```
- **参数**
    |  参数  |        描述        |
    | :----: | :----------------: |
    |   T    | 状态机持有者的类型 |
    | owner  |    状态机持有者    |
    |  name  |    状态机的名字    |
    | states |      状态集合      |
- **返回**
    返回新创建的状态机
- **描述**
    创建状态机的方法
- **异常**
    如果已经存在该状态机，抛出异常
<br>

#### HasFsm
```
public bool HasFsm<T>(string name) where T : class;
```
- **参数**
    | 参数  |        描述        |
    | :---: | :----------------: |
    |   T   | 状态机持有者的类型 |
    | name  |    状态机的名字    |
- **返回**
    是否存在该状态机
- **描述**
    判断是否存在指定状态机的方法
<br>
        
#### GetFsm
```
public IFsm<T> GetFsm<T>(string name) where T : class;
```
- **参数**
    | 参数  |        描述        |
    | :---: | :----------------: |
    |   T   | 状态机持有者的类型 |
    | name  |    状态机的名字    |
- **返回**
    指定的状态机
- **描述**
    获取指定状态机的方法
<br>

#### RemoveFsm
```
public bool RemoveFsm<T>(string name) where T : class;
```
- **参数**
    | 参数  |        描述        |
    | :---: | :----------------: |
    |   T   | 状态机持有者的类型 |
    | name  |    状态机的名字    |
- **返回**
    是否成功移除状态机
- **描述**
    移除状态机的方法
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    调用每个状态机的 Shutdown 方法，清理资源
<br>

## 非 public 方法
#### Update
```
private void Update();
```
- **描述**
    Unity 生命周期方法
- **实现思路**
    将状态机集合放入临时列表，帧更新正在运行的状态机
<br>
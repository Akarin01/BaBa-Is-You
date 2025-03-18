# Fsm\<T\>
```
internal sealed class Fsm<T> : FsmBase, IFsm<T> where T : class;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 状态机的持有者 |
- **描述**
    状态机
> [FsmBase](./FsmBase.md)
> [IFsm](./IFsm.md)

## 成员变量
#### m_states
```
private Dictionary<Type, FsmState<T>> m_states;
```
- **描述**
    以<状态机持有者的类型，状态>键值对存储的状态字典
<br>

#### m_datas
```
private Dictionary<string, object> m_datas;
```
- **描述**
    以<值的名字，值>键值对存储的值字典
<br>

#### m_owner
```
private T m_owner;
```
- **描述**
    状态机的持有者
<br>

#### m_name
```
private string m_name;
```
- **描述**
    状态机的名字
<br>

#### m_isRunning
```
private bool m_isRunning;
```
- **描述**
    状态机是否正在运行
<br>

#### m_currentState
```
private FsmState<T> m_currentState;
```
- **描述**
    状态机的当前状态
<br>

## 属性
#### Owner
```
public T Owner => m_owner;
```
- **描述**
    状态机的持有者
<br>

#### Name
```
public string Name => m_name;
```
- **描述**
    状态机的名字
<br>

#### CurrentState
```
public FsmState<T> CurrentState => m_currentState;
```
- **描述**
    状态机当前的状态
<br>

#### IsRunning
```
public override bool IsRunning => m_isRunning;
```
- **描述**
    状态机是否正在运行
<br>

## public 方法
#### 构造函数
```
public Fsm(T owner, string name, params FsmState<T>[] states);
```
- **参数**
    |  参数  |       描述       |
    | :----: | :--------------: |
    | owner  |  状态机的持有者  |
    |  name  |   状态机的名字   |
    | states | 状态机的状态集合 |
- **描述**
    状态机的构造函数
- **异常**
    如果状态机的持有者为 null，抛出异常；如果状态集合为 null 或 empty，抛出异常；如果状态为 null，抛出异常；如果状态已经存在，抛出异常
- **实现思路**
    初始化状态机，并调用状态的 OnInit 方法
<br>

#### SetData
```
public void SetData(string name, object value);
```
- **异常**
    如果 name 为 null 或 empty，抛出异常
<br>

#### GetData
```
public object GetData(string name);
```
- **异常**
    如果 name 为 null 或 empty，抛出异常
<br>

#### HasData
```
public bool HasData(string name);
```
- **异常**
    如果 name 为 null 或 empty，抛出异常
<br>

#### RemoveData
```
public bool RemoveData(string name);
```
- **异常**
    如果 name 为 null 或 empty，抛出异常
<br>

#### Start
```
public void Start<TState>() where TState : FsmState<T>;
```
- **异常**
    如果状态机正在运行，抛出异常；如果不存在指定状态，抛出异常
- **实现思路**
    通过 GetState 获取指定状态实例，设置为当前状态，并调用 OnEnter 方法
```
public void Start(Type stateType);
```
- **异常**
    如果状态机正在运行，抛出异常；如果不存在指定状态，抛出异常
- **实现思路**
    通过 GetState 获取指定状态实例，设置为当前状态，并调用 OnEnter 方法
<br>

## 非 public 方法
#### OnUpdate
```
internal override void OnUpdate(float deltaTime, float realDeltaTime);
```
- **实现思路**
    调用当前状态的 OnUpdate 方法
<br>

#### Shutdown
```
internal override void Shutdown();
```
- **实现思路**
    调用当前状态的 Exit 方法，调用每个状态的 OnRelease 方法；清理资源
<br>

#### GetState
```
private TState GetState<TState>() where TState: FsmState<T>;
```
- **参数**
    |  参数  |   描述   |
    | :----: | :------: |
    | TState | 状态类型 |
- **返回**
    如果存在，返回指定状态；否则，返回 null
- **描述**
    获取指定类型状态
```
private FsmState<T> GetState(Type stateType);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | stateType | 状态类型 |
- **返回**
    如果存在，返回指定状态；否则，返回 null
- **描述**
    获取指定类型状态
- **异常**
    如果 stateType 为 null，抛出异常；如果 stateType 类型变量无法赋值给 FsmState<T> 类型变量，抛出异常
<br>

#### ChangeState
```
internal void ChangeState<TState>() where TState : FsmState<T>;
```
- **参数**
    |  参数  |   描述   |
    | :----: | :------: |
    | TState | 状态类型 |
- **描述**
    切换到指定状态
- **异常**
    如果不存在指定状态，抛出异常
- **实现思路**
    调用当前状态的 OnExit 方法，切换到指定状态，调用当前状态的 OnEnter 方法
```
internal void ChangeState(Type stateType);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | stateType | 状态类型 |
- **描述**
    切换到指定状态
- **异常**
    如果不存在指定状态，抛出异常
- **实现思路**
    调用当前状态的 OnExit 方法，切换到指定状态，调用当前状态的 OnEnter 方法
<br>
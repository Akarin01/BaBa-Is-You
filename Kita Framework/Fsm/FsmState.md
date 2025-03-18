# FsmState\<T\>
```
public abstract class FsmState<T> where T : class;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 状态机的持有者 |
- **描述**
    状态机状态基类，定义状态的生命周期方法

## 非 public 方法
#### OnInit
```
protected internal virtual void OnInit(IFsm<T> fsm);
```
- **参数**
    | 参数  |  描述  |
    | :---: | :----: |
    |  fsm  | 状态机 |
- **描述**
    在状态初始化时调用
<br>

#### OnEnter
```
protected internal virtual void OnEnter(IFsm<T> fsm);
```
- **参数**
    | 参数  |  描述  |
    | :---: | :----: |
    |  fsm  | 状态机 |
- **描述**
    在状态进入时调用
<br>

#### OnUpdate
```
protected internal virtual void OnUpdate(IFsm<T> fsm, float deltaTime, float realDeltaTime);
```
- **参数**
    |     参数      |      描述      |
    | :-----------: | :------------: |
    |      fsm      |     状态机     |
    |   deltaTime   |   帧更新间隔   |
    | realDeltaTime | 帧更新实际间隔 |
- **描述**
    在状态帧更新时调用
<br>

#### OnExit
```
protected internal virtual void OnExit(IFsm<T> fsm, bool isShutdown);
```
- **参数**
    |    参数    |            描述            |
    | :--------: | :------------------------: |
    |    fsm     |           状态机           |
    | isShutdown | 是否是在状态机关闭时被调用 |
- **描述**
    在状态退出时调用
<br>

#### OnRelease
```
protected internal virtual void OnRelease(IFsm<T> fsm);
```
- **参数**
    | 参数  |  描述  |
    | :---: | :----: |
    |  fsm  | 状态机 |
- **描述**
    在状态被释放时调用
<br>

#### ChangeState
```
protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>;
```
- **参数**
    |  参数  |    描述    |
    | :----: | :--------: |
    | TState | 状态的类型 |
    |  fsm   |   状态机   |
- **描述**
    切换到指定状态
```
protected void ChangeState(IFsm<T> fsm, Type stateType);
```
- **参数**
    |   参数    |    描述    |
    | :-------: | :--------: |
    |    fsm    |   状态机   |
    | stateType | 状态的类型 |
- **描述**
    切换到指定状态
<br>
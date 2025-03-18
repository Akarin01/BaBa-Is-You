# ProcedureBase
```
public abstract class ProcedureBase : FsmState<ProcedureManager>;
```
- **描述**
    流程基类
> [FsmState](../Fsm/FsmState.md)

## 非 public 方法
#### OnInit
```
protected internal override void OnInit(ProcedureOwner procedureOwner);
```
> ```ProcedureOwner``` 为 ```IFsm<ProcedureManager>```
- **参数**
    |      参数      |         描述         |
    | :------------: | :------------------: |
    | procedureOwner | 流程持有者（状态机） |
- **描述**
    在流程初始化时调用
- **实现思路**
    调用父类的 OnInit 方法
<br>

#### OnEnter
```
protected internal override void OnEnter(ProcedureOwner procedureOwner);
```
- **参数**
    |      参数      |         描述         |
    | :------------: | :------------------: |
    | procedureOwner | 流程持有者（状态机） |
- **描述**
    在流程进入时调用
- **实现思路**
    调用父类的 OnEnter 方法
<br>

#### OnUpdate
```
protected internal override void OnUpdate(ProcedureOwner procedureOwner, float deltaTime, float realDeltaTime);
```
- **参数**
    |      参数      |         描述         |
    | :------------: | :------------------: |
    | procedureOwner | 流程持有者（状态机） |
    |   deltaTime    |      帧更新间隔      |
    | realDeltaTime  |    帧更新实际间隔    |
- **描述**
    在状态帧更新时调用
- **实现思路**
    调用父类的 OnUpdate 方法
<br>

#### OnExit
```
protected internal override void OnExit(ProcedureOwner procedureOwner, bool isShutdown);
```
- **参数**
    |      参数      |            描述            |
    | :------------: | :------------------------: |
    | procedureOwner |    流程持有者（状态机）    |
    |   isShutdown   | 是否是在状态机关闭时被调用 |
- **描述**
    在流程退出时调用
- **实现思路**
    调用父类的 OnExit 方法
<br>

#### OnRelease
```
protected internal override void OnRelease(ProcedureOwner procedureOwner);
```
- **参数**
    |      参数      |         描述         |
    | :------------: | :------------------: |
    | procedureOwner | 流程持有者（状态机） |
- **描述**
    在流程被释放时调用
- **实现思路**
    调用父类的 OnRelease 方法
<br>
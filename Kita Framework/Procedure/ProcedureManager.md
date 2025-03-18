# ProcedureManager
```
public class FsmManager : FrameworkManager;
```
- **描述**
    流程管理器类，利用状态机管理流程
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_availableProcedureTypeNames
```
[SerializeField] private string[] m_availableProcedureTypeNames;
```
- **描述**
    可用的流程类型名字的数组
<br>

#### m_entranceProcedureTypeName
```
[SerializeField] private string m_entranceProcedureTypeName;
```
- **描述**
    初始流程类型的名字
<br>

#### m_procedureFsm
```
private IFsm<ProcedureManager> m_procedureFsm;
```
- **描述**
    用于流程管理的状态机
<br>

#### m_fsmManager
```
private FsmManager m_fsmManager;
```
- **描述**
    用于创建和销毁状态机的状态机管理器
<br>

## 属性
#### CurrentProcedure
```
public ProcedureBase CurrentProcedure => (ProcedureBase)m_procedureFsm.CurrentState;
```
- **描述**
    当前流程
<br>

## public 方法
#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    利用状态机管理器销毁状态机
<br>

## 非 public 方法
#### Start
```
private IEnumerator Start();
```
- **描述**
    Unity 生命周期方法
- **异常**
    如果不存在 ProcedureBase 的子类，打印错误日志
- **实现思路**
    初始化，并以指定的流程启动状态机
<br>

#### StartProcedure
```
private void StartProcedure<TProcedure>() where TProcedure : ProcedureBase;
```
- **参数**
    |    参数    |    描述    |
    | :--------: | :--------: |
    | TProcedure | 流程的类型 |
- **描述**
    以指定流程开始状态机
- **异常**
    如果状态机不存在，抛出异常
- **实现思路**
    调用状态机的 Start 方法

```
private void StartProcedure(Type procedureType);
```
- **参数**
    |     参数      |    描述    |
    | :-----------: | :--------: |
    | procedureType | 流程的类型 |
- **描述**
    以指定流程开始状态机
- **异常**
    如果状态机不存在，抛出异常
- **实现思路**
    调用状态机的 Start 方法
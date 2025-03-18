# FsmBase
```
public abstract class FsmBase;
```
- **描述**
    状态机基类

## 属性
#### Name
```
public abstract bool IsRunning { get; }
```
- **描述**
    状态是否正在运行
<br>

## 非 public 方法
#### OnUpdate
```
internal abstract void OnUpdate(float deltaTime, float realDeltaTime);
```
- **参数**
    |     参数      |      描述      |
    | :-----------: | :------------: |
    |   deltaTime   |   帧更新间隔   |
    | realDeltaTime | 帧更新实际间隔 |
- **描述**
    帧更新
<br>

#### Shutdown
```
internal abstract void Shutdown();
```
- **描述**
    关闭状态机
<br>
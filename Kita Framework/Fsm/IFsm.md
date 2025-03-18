# IFsm\<T\>
```
public interface IFsm<T> where T : class;
```
- **参数**
    | 参数  |      描述      |
    | :---: | :------------: |
    |   T   | 状态机的持有者 |
- **描述**
    状态机接口

## 属性
#### Owner
```
public T Owner { get; }
```
- **描述**
    状态机的持有者
<br>

#### Name
```
public string Name { get; }
```
- **描述**
    状态机的名字
<br>

#### CurrentState
```
public FsmState<T> CurrentState { get; }
```
- **描述**
    状态机当前的状态
<br>

## public 方法
#### Start
```
public void Start<TState>() where TState : FsmState<T>;
```
- **参数**
    |  参数  |   描述   |
    | :----: | :------: |
    | TState | 状态类型 |
- **描述**
    以指定状态开始状态机
```
public void Start(Type stateType);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | stateType | 状态类型 |
- **描述**
    以指定状态开始状态机
<br>

#### SetData
```
public void SetData(string name, object value);
```
- **参数**
    | 参数  |   描述   |
    | :---: | :------: |
    | name  | 值的名字 |
    | value |    值    |
- **描述**
    设置指定数据
<br>

#### GetData
```
public object GetData(string name);
```
- **参数**
    | 参数  |   描述   |
    | :---: | :------: |
    | name  | 值的名字 |
- **返回**
    如果存在该数据，返回数据的值；否则，返回 null
- **描述**
    获取指定数据
<br>

#### HasData
```
public bool HasData(string name);
```
- **参数**
    | 参数  |   描述   |
    | :---: | :------: |
    | name  | 值的名字 |
- **返回**
    是否存在指定数据
- **描述**
    判断是否存在指定数据
<br>

#### RemoveData
```
public bool RemoveData(string name);
```
- **参数**
    | 参数  |   描述   |
    | :---: | :------: |
    | name  | 值的名字 |
- **返回**
    是否删除成功
- **描述**
    删除指定数据
<br>
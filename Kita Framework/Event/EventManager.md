# EventManager
```
public class EventManager : FrameworkManager;
```
- **描述**
    事件管理类，存储已注册的事件
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_events
```
private Dictionary<int, EventHandler<BaseEventArgs>> m_events;
```
- **描述**
    以<事件ID，事件响应委托>为键值对存储的以注册事件
<br>

## public 方法
#### Subscribe
```
public void Subscribe(int eventID, EventHandler<BaseEventArgs> handler);
```
- **参数**
    |  参数   |     描述     |
    | :-----: | :----------: |
    | eventID |   事件 ID    |
    | handler | 事件响应方法 |
- **描述**
    向指定事件注册指定方法
- **异常**
    如果重复注册，抛出异常
- **实现思路**
    向对应事件响应委托添加响应方法；如果字典中不存在对应事件，则初始化一个空委托
<br>

#### Unsubscribe
```
public void Unsubscribe(int eventID, EventHandler<BaseEventArgs> handler);
```
- **参数**
    |  参数   |     描述     |
    | :-----: | :----------: |
    | eventID |   事件 ID    |
    | handler | 事件响应方法 |
- **描述**
    向指定事件注销指定方法
- **异常**
    如果未注册，抛出异常
- **实现思路**
    从对应事件响应委托移除响应方法
<br>

#### Check
```
public bool Check(int eventID, EventHandler<BaseEventArgs> handler);
```
- **参数**
    |  参数   |     描述     |
    | :-----: | :----------: |
    | eventID |   事件 ID    |
    | handler | 事件响应方法 |
- **描述**
    确认指定事件是否注册过指定方法
- **返回**
    是否已经注册过
- **实现思路**
    调用 InternalCheck 方法
<br>

#### Fire
```
public void Fire(int eventID, object sender, BaseEventArgs e);
```
- **参数**
    |  参数   |    描述    |
    | :-----: | :--------: |
    | eventID |  事件 ID   |
    | sender  | 事件触发者 |
    |    e    |  事件参数  |
- **描述**
    触发事件
- **实现思路**
    触发对应事件；因为事件使用空委托初始化，所以不会为 null
<br>

## 非 public 方法
#### InternalCheck
```
private bool InternalCheck(int eventID, EventHandler<BaseEventArgs> handler);
```
- **参数**
    |  参数   |     描述     |
    | :-----: | :----------: |
    | eventID |   事件 ID    |
    | handler | 事件响应方法 |
- **描述**
    确认指定事件是否注册过指定方法
- **返回**
    是否已经注册过
- **实现思路**
    如果字典中不存在，返回 false；遍历对应的事件响应委托，检查是否已经注册
<br>
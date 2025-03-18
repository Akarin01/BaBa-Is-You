# UIForm
```
public abstract class UIForm : MonoBehaviour;
```
- **描述**
    UI 窗体基类，定义 UI 生命周期逻辑

## 属性
#### GroupName
```
public string GroupName { get; set; }
```
- **描述**
    UI 窗体所属的 Group 的名字
<br>

## public 方法
#### OnInit
```
public virtual void OnInit();
```
- **描述**
    初始化生命周期函数，在 UI 窗体被初始化时调用
<br>

#### OnOpen
```
public virtual void OnOpen(object data);
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    | data  | 关闭 UI 时传递的数据 |
- **描述**
    打开生命周期函数，在 UI 窗体被打开时调用
<br>

#### OnClose
```
public virtual void OnClose(object data);
```
- **参数**
    | 参数  |         描述         |
    | :---: | :------------------: |
    | data  | 关闭 UI 时传递的数据 |
- **描述**
    关闭生命周期函数，在 UI 窗体被关闭时调用
<br>

#### OnPause
```
public virtual void OnPause();
```
- **描述**
    暂停生命周期函数，在 UI 窗体被暂停时调用
<br>

#### OnResume
```
public virtual void OnResume();
```
- **描述**
    恢复生命周期函数，在 UI 窗体被恢复时调用
<br>

#### OnRelease
```
public virtual void OnRelease();
```
- **描述**
    释放生命周期函数，在 UI 窗体被释放时调用
<br>

## protected 方法
#### Close
```
protected void Close(bool immediate);
```
- **参数**
    |   参数    |         描述         |
    | :-------: | :------------------: |
    | immediate | 是否立刻关闭 UI 窗体 |
- **描述**
    关闭 UI 窗体的方法，调用 UIManager 的 CloseUI 方法。如果不立刻关闭，会应用淡出，再关闭
<br>
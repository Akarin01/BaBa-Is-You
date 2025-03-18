# UIGroup
```
public class UIGroup;
```
- **描述**
    UI 组类，作为同一组别的 UIForm 栈，负责调用 UIForm 的生命周期方法

## 成员变量
#### m_uiForms
```
private LinkedList<UIForm> m_uiForms;
```
- **描述**
    以链表存储的 UIForm 栈
<br>

## public 方法
#### AddUIForm
```
public void AddUIForm(UIForm uiForm, object data);
```
- **参数**
    |  参数  |         描述         |
    | :----: | :------------------: |
    | uiForm |    添加的 UI 窗体    |
    |  data  | 打开 UI 时传递的数据 |
- **描述**
    向 UI 组栈顶添加 UI 窗体的方法，会触发添加的 UI 窗体的OnOpen 和栈顶的 UI 窗体的 OnPause 生命周期方法
<br>

#### RemoveUIForm
```
public void RemoveUIForm(UIForm uiForm, object data);
```
- **参数**
    |  参数  |         描述         |
    | :----: | :------------------: |
    | uiForm |    移除的 UI 窗体    |
    |  data  | 关闭 UI 时传递的数据 |
- **描述**
    从 UI 组中移除 UI 窗体的方法，会触发 UI 窗体的OnClose 和 UI 窗体下的窗体的 OnResume 生命周期方法
<br>

#### Release
```
public void Release();
```
- **描述**
    释放对象
<br>
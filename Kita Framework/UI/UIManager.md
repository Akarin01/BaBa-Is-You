# UIManager
```
public class UIManager : FrameworkManager;
```
- **描述**
    UI 管理器类，管理 UI 组，提供外部开关 UI 的接口
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_uiFormInstancesRoot
```
[SerializeField] private Transform m_uiFormInstancesRoot;
```
- **描述**
    新创建的 UIForm 的 GameObject 的父对象
<br>

#### m_uiGroups
```
private Dictionary<string, UIGroup> m_uiGroups;
```
- **描述**
    以<组名字，UI 组>存储的字典
<br>

#### m_objectPool
```
private IObjectPool<UIFormObject> m_objectPool;
```
> [IObjectPool](../Object%20Pool/IObjectPool.md)
- **描述**
    提供 UIForm 对象的对象池
<br>

#### m_assetLoader
```
private AssetLoaderBase m_assetLoader;
```
> [AssetLoaderBase](../Resource/AssetLoaderBase.md)
- **描述**
    资源加载器
<br>

#### m_loadedAssetNames
```
private HashSet<string> m_loadedAssetNames;
```
- **描述**
    已加载资源名字的集合
<br>

## public 方法
#### OpenUI
```
public void OpenUI(string uiPath, string groupName, object data);
```
- **参数**
    |   参数    |         描述         |
    | :-------: | :------------------: |
    |  uiPath   |  UIForm 的加载路径   |
    | groupName |   UIForm 组的名字    |
    |   data    | 打开 UI 时传递的数据 |
- **描述**
    打开 UI 窗体的方法。根据窗体的名字从对象池获取对象，如果没有对象，动态加载对象，创建对应的 UIFormObject 实例，并注册进对象池；然后向窗体对应的 UI 组中添加窗体
<br>

#### CloseUI
```
public void CloseUI(UIForm uiForm, object data = null);
```
- **参数**
    |  参数  |         描述         |
    | :----: | :------------------: |
    | uiForm |    关闭的 UI 窗体    |
    |  data  | 关闭 UI 时传递的数据 |
- **描述**
    关闭 UI 窗体的方法。从窗体对应的 UI 组中移除窗体，然后归还对象池对象
<br>

#### Shutdown
```
public override void Shutdown();
```
- **描述**
    关闭 UI 管理器，清理资源。会调用每个 UI 组的 Release 方法，关闭持有的对象池，卸载已加载的资源
<br>

## private 方法
#### AddUIForm
```
private void AddUIForm(string groupName, UIForm uiForm, object data);
```
- **参数**
    |   参数    |         描述         |
    | :-------: | :------------------: |
    | groupName |      UI 组名字       |
    |  uiForm   |    添加的 UI 窗体    |
    |   data    | 打开 UI 时传递的数据 |
- **描述**
    向指定 UI 组添加 UI 窗体的方法，如果组不存在，则创建 UI 组
<br>

#### RemoveUIForm
```
private void RemoveUIForm(string groupName, UIForm uiForm, object data);
```
- **参数**
    |   参数    |         描述         |
    | :-------: | :------------------: |
    | groupName |      UI 组名字       |
    |  uiForm   |    移除的 UI 窗体    |
    |   data    | 关闭 UI 时传递的数据 |
- **描述**
    从指定 UI 组移除 UI 窗体的方法
- **异常**
    如果组不存在，则打印错误日志
<br>

#### LoadUIFormSuccessCallback
```
private void LoadUIFormSuccessCallback(string assetName, object asset, object userData);
```
- **参数**
    |   参数    |     描述     |
    | :-------: | :----------: |
    | assetName |   资源名字   |
    |   asset   |     资源     |
    | userData  | 回调方法参数 |
- **描述**
    资源加载成功的回调方法
- **异常**
    如果资源不是 GameObject 类型，打印错误日志；如果 userData 不是 LoadUIFormData 类型，打印错误日志
<br>

#### LoadUIFormFailureCallback
```
private void LoadUIFormFailureCallback(string assetName, string errorMsg, object userData);
```
- **参数**
    |   参数    |     描述     |
    | :-------: | :----------: |
    | assetName |   资源名字   |
    | errorMsg  |   错误信息   |
    | userData  | 回调方法参数 |
- **描述**
    加载资源失败的回调方法
<br>
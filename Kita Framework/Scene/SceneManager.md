# SceneManager
```
public class SceneManager : FrameworkManager;
```
- **描述**
    Scene 管理器，管理加载的、正在加载的、正在卸载的场景资源
> [FrameworkManager](../Base/FrameworkManager.md)

## 成员变量
#### m_loadedSceneAssetNames
```
private List<string> m_loadedSceneAssetNames;
```
- **描述**
    存储已经加载的场景资源名字的列表
<br>

#### m_loadingSceneAssetNames
```
private List<string> m_loadingSceneAssetNames;
```
- **描述**
    存储正在加载的场景资源名字的列表
<br>

#### m_unloadingSceneAssetNames
```
private List<string> m_unloadingSceneAssetNames;
```
- **描述**
    存储正在卸载的场景资源名字的列表
<br>

#### m_loadSceneCallbacks
```
private LoadSceneCallbacks m_loadSceneCallbacks;
```
> [LoadSceneCallbacks](../Resource/LoadSceneCallbacks.md)
- **描述**
    加载场景的回调方法类
<br>

#### m_unloadSceneCallbacks
```
private LoadSceneCallbacks m_unloadSceneCallbacks;
```
> [LoadSceneCallbacks](../Resource/LoadSceneCallbacks.md)
- **描述**
    卸载场景的回调方法类
<br>

#### m_sceneLoader
```
private SceneLoaderBase m_sceneLoader;
```
> [SceneLoaderBase](../Resource/SceneLoaderBase.md)
- **描述**
    场景加载器
<br>

## 事件
#### OnUnloadSceneSuccess
```
public event EventHandler<UnloadSceneSuccessEventArgs> OnUnloadSceneSuccess;
```
- **描述**
    卸载场景成功时的事件
<br>

## public 方法
#### LoadScene
```
public void LoadScene(string sceneAssetName, object userData = null);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    userData    | 回调函数参数 |
- **描述**
    加载场景的方法
- **异常**
    如果场景正在加载、正在卸载、已经加载，抛出异常
- **实现思路**
    将场景名字加入 m_loadingSceneAssetNames 列表，然后调用 m_sceneLoader 的 LoadScene 异步加载
<br>

#### UnloadScene
```
public void UnloadScene(string sceneAssetName, object userData = null);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    userData    | 回调函数参数 |
- **描述**
    卸载场景的方法
- **异常**
    如果场景正在加载、正在卸载、没被加载，抛出异常
- **实现思路**
    将场景名字从 m_loadedSceneAssetNames 列表中移除，加入 m_unloadingSceneAssetNames 列表，然后调用 m_sceneLoader 的 UnloadScene 异步卸载
<br>

#### IsSceneLoaded
```
public bool IsSceneLoaded(string sceneAssetName);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
- **返回**
    场景是否已经加载
- **描述**
    判断场景是否已经加载的方法
<br>

#### GetLoadedSceneAssetNames
```
public string[] GetLoadedSceneAssetNames();
```
- **返回**
    所有已经加载的场景的名字数组
- **描述**
    获取所有已经加载的场景的名字
<br>

#### IsSceneLoading
```
public bool IsSceneLoading(string sceneAssetName);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
- **返回**
    场景是否正在加载
- **描述**
    判断场景是否正在加载的方法
<br>

#### GetLoadingSceneAssetNames
```
public string[] GetLoadingSceneAssetNames();
```
- **返回**
    所有正在加载的场景的名字数组
- **描述**
    获取所有正在加载的场景的名字
<br>

#### IsSceneUnloading
```
public bool IsSceneUnloading(string sceneAssetName);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
- **返回**
    场景是否正在卸载
- **描述**
    判断场景是否正在卸载的方法
<br>

#### GetUnloadingSceneAssetNames
```
public string[] GetUnloadingSceneAssetNames();
```
- **返回**
    所有正在卸载的场景的名字数组
- **描述**
    获取所有正在卸载的场景的名字
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    清除各个 List，清除事件的委托
<br>

## 非 public 方法
#### LoadSceneSuccessCallback
```
private void LoadSceneSuccessCallback(string sceneAssetName, object userData);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    userData    | 回调方法参数 |
- **描述**
    加载场景成功的回调方法
- **实现思路**
    将场景名字从 m_loadingSceneAssetNames 列表移至 m_loadedSceneAssetNames 列表
<br>

#### LoadSceneFailureCallback
```
private void LoadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    errorMsg    |   错误信息   |
    |    userData    | 回调方法参数 |
- **描述**
    加载场景失败的回调方法
- **异常**
    打印错误日志
- **实现思路**
    将场景名字从 m_loadingSceneAssetNames 移除
<br>

#### UnloadSceneSuccessCallback
```
private void UnloadSceneSuccessCallback(string sceneAssetName, object userData);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    userData    | 回调方法参数 |
- **描述**
    卸载场景成功的回调方法
- **实现思路**
    将场景名字从 m_unloadingSceneAssetNames 移除，触发 OnUnloadSceneSuccess 事件
<br>

#### UnloadSceneFailureCallback
```
private void UnloadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData);
```
- **参数**
    |      参数      |     描述     |
    | :------------: | :----------: |
    | sceneAssetName | 场景资源名字 |
    |    errorMsg    |   错误信息   |
    |    userData    | 回调方法参数 |
- **描述**
    卸载场景失败的回调方法
- **异常**
    打印错误日志
- **实现思路**
    将场景名字从 m_unloadingSceneAssetNames 移除
<br>
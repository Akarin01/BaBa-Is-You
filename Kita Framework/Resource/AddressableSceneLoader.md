# AddressableSceneLoader
```
public class AddressableSceneLoader : SceneLoaderBase;
```
> [SceneLoaderBase](./SceneLoaderBase.md)
- **描述**
    使用 Addressables 实现的场景加载器

## 成员变量
#### m_sceneAssetNameHandlerMaps
```
private Dictionary<string, AsyncOperationHandle<SceneInstance>> m_sceneAssetNameHandlerMaps;
```
- **描述**
    以<正在加载或者已经加载的场景资源名字，场景加载 Handle>存储的映射

## public 方法
#### LoadScene
```
public override void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);
```
- **实现思路**
    调用 Addressables.LoadSceneAsync 异步加载场景；存储返回的 handler；订阅其 Completed 事件，处理回调方法
<br>

#### UnloadScene
```
public override void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);
```
- **实现思路**
    根据 m_sceneAssetNameHandlerMaps 获取对应的 handler，调用 Addressables.UnloadSceneAsync 异步加载场景；订阅其 Completed 事件，处理回调方法
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    卸载 m_sceneAssetNameHandlerMaps 中的所有场景，并清空 m_sceneAssetNameHandlerMaps
<br>
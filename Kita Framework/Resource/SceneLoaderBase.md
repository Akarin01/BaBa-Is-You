# SceneLoaderBase
```
public abstract class SceneLoaderBase : MonoBehaviour;
```
- **描述**
    场景加载器基类


## public 方法
#### LoadScene
```
public abstract void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);
```
- **参数**
    |        参数        |         描述         |
    | :----------------: | :------------------: |
    |   sceneAssetName   |     场景资源名字     |
    | loadSceneCallbacks | 加载场景的回调方法类 |
    |      userData      |     回调方法参数     |
- **描述**
    异步加载场景，在加载成功或失败后调用回调方法
<br>

#### UnloadScene
```
public abstract void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);
```
- **参数**
    |         参数         |         描述         |
    | :------------------: | :------------------: |
    |    sceneAssetName    |     场景资源名字     |
    | unloadSceneCallbacks | 卸载场景的回调方法类 |
    |       userData       |     回调方法参数     |
- **描述**
    异步卸载场景，在卸载成功或失败后调用回调方法
<br>

#### Shutdown
```
public abstract void Shutdown();
```
- **描述**
    关闭场景加载器，清理资源
<br>
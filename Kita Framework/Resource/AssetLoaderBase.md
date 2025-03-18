# AssetLoaderBase
```
public abstract class AssetLoaderBase : MonoBehaviour;
```
- **描述**
    资源加载器基类


## public 方法
#### LoadAsset
```
public abstract void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData);
```
- **参数**
    |        参数        |         描述         |
    | :----------------: | :------------------: |
    |      TObject       |       资源类型       |
    |   sceneAssetName   |       资源名字       |
    | loadAssetCallbacks | 加载资源的回调方法类 |
    |      userData      |     回调方法参数     |
- **描述**
    异步加载资源，在加载成功或失败后调用回调方法
<br>

#### UnloadScene
```
public abstract void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData);
```
- **参数**
    |         参数         |         描述         |
    | :------------------: | :------------------: |
    |      assetName       |       资源名字       |
    | unloadAssetCallbacks | 卸载资源的回调方法类 |
    |       userData       |     回调方法参数     |
- **描述**
    异步卸载资源，在卸载成功或失败后调用回调方法
<br>

#### Shutdown
```
public abstract void Shutdown();
```
- **描述**
    关闭资源加载器，清理资源
<br>
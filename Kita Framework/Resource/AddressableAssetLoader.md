# AddressableAssetLoader
```
public class AddressableAssetLoader : IAssetLoader;
```
> [AssetLoaderBase](./AssetLoaderBase.md)
- **描述**
    使用 Addressables 实现的资源加载器

## 成员变量
#### m_loadedAssets
```
private Dictionary<string, AsyncOperationHandle> m_loadedAssets;
```
- **描述**
    以<已加载资源的名字，资源加载句柄>存储的字典
<br>

#### m_loadingAssets
```
private Dictionary<string, LoadingAssetInfo> m_loadingAssets;
```
> [LoadingAssetInfo](./LoadingAssetInfo.md)
- **描述**
    以<正在加载资源的名字，加载中资源信息>存储的字典
<br>

## public 方法
#### LoadAsset 
```
public override void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData);
```
- **实现思路**
    如果已经加载过该资源，直接调用回调方法；如果正在加载该资源，向 LoadingAssetInfo 中添加回调方法及参数；如果没有加载过该资源，利用协程加载资源
<br>

#### UnloadAsset
```
public override void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData);
```
- **实现思路**
    如果资源已经被加载，直接卸载，调用回调方法；如果资源正在加载，中断加载，调用回调方法；如果资源没被加载，调用回调方法
<br>

#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    卸载已经加载的资源；中断正在加载的协程；调用正在加载资源的回调方法；清空字典
<br>

## 非 public 方法
#### LoadAssetCO
```
private IEnumerator LoadAssetCO<TObject>(string assetName);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | assetName | 资源名字 |
- **返回**
    协程使用的迭代器
- **具体实现**
    使用 Addressables.LoadAssetAsync<TObject> 加载资源
<br>
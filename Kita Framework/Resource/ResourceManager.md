# ResourceManager
```
public class ResourceManager : FrameworkManager;
```
- **描述**
    Resource 管理器，存储场景、资源加载器
> [FrameworkManager](../Base/FrameworkManager.md)

## 属性
#### SceneLoader
```
public ISceneLoader SceneLoader { get; private set; }
```
- **描述**
    场景加载器
<br>

#### AssetLoader
```
public IAssetLoader AssetLoader { get; private set; }
```
- **描述**
    资源加载器
<br>


## public 方法
#### Shutdown
```
public override void Shutdown();
```
- **实现思路**
    关闭场景、资源加载器
<br>
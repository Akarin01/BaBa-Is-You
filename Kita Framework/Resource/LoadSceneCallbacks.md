# LoadSceneCallbacks
```
public class LoadSceneCallbacks;
```
- **描述**
    加载场景的回调方法类，包装了加载场景成功的回调方法和加载场景失败的回调方法


## 属性
#### LoadSceneSuccessCallback
```
public LoadSceneSuccessCallback LoadSceneSuccessCallback { get; private set; }
```
> ```public delegate void LoadSceneSuccessCallback(string sceneAssetName, object userData);```
- **描述**
    加载场景成功的回调方法
<br>

#### LoadSceneFailureCallback
```
public LoadSceneFailureCallback LoadSceneFailureCallback { get; private set; }
```
> ```public delegate void LoadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData);```
- **描述**
    加载场景失败的回调方法
<br>
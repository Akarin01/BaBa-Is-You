# UnloadSceneCallbacks
```
public class UnloadSceneCallbacks;
```
- **描述**
    卸载场景的回调方法类，包装了卸载场景成功的回调方法和卸载场景失败的回调方法


## 属性
#### UnloadSceneSuccessCallback
```
public UnloadSceneSuccessCallback UnloadSceneSuccessCallback { get; private set; }
```
> ```public delegate void UnloadSceneSuccessCallback(string sceneAssetName, object userData);```
- **描述**
    卸载场景成功的回调方法
<br>

#### UnloadSceneFailureCallback
```
public UnloadSceneFailureCallback UnloadSceneFailureCallback { get; private set; }
```
> ```public delegate void UnloadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData);```
- **描述**
    卸载场景失败的回调方法
<br>
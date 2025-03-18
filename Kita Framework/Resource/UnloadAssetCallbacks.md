# UnloadAssetCallbacks
```
public class UnloadAssetCallbacks;
```
- **描述**
    卸载资源的回调方法类，包装了卸载资源成功的回调方法和卸载资源失败的回调方法


## 属性
#### UnloadAssetSuccessCallback
```
public UnloadAssetSuccessCallback UnloadAssetSuccessCallback { get; private set; }
```
> ```public delegate void UnloadAssetSuccessCallback(string assetName, object userData);```
- **描述**
    卸载资源成功的回调方法
<br>

#### UnloadAssetFailureCallback
```
public UnloadAssetFailureCallback UnloadAssetFailureCallback { get; private set; }
```
> ```public delegate void UnloadAssetFailureCallback(string assetName, string errorMsg, object userData);```
- **描述**
    卸载资源失败的回调方法
<br>
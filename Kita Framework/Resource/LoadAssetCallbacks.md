# LoadAssetCallbacks
```
public class LoadAssetCallbacks;
```
- **描述**
    加载资源的回调方法类，包装了加载资源成功的回调方法和加载资源失败的回调方法


## 属性
#### LoadAssetSuccessCallback
```
public LoadAssetSuccessCallback LoadAssetSuccessCallback { get; private set; }
```
> ```public delegate void LoadAssetSuccessCallback(string assetName, object asset, object userData);```
- **描述**
    加载资源成功的回调方法
<br>

#### LoadAssetFailureCallback
```
public LoadAssetFailureCallback LoadAssetFailureCallback { get; private set; }
```
> ```public delegate void LoadAssetFailureCallback(string assetName, string errorMsg, object userData);```
- **描述**
    加载资源失败的回调方法
<br>
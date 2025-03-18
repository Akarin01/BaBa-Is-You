# LoadingAssetInfo
```
public partial class AddressableAssetLoader
{
    private class LoadingAssetInfo;
}
```
- **描述**
    存储加载中资源的相关信息类

## 成员变量
#### m_loadAssetCallbacksList
```
private List<LoadAssetCallbacks> m_loadAssetCallbacksList = new();
```
- **描述**
    加载资源的回调方法
<br>

#### m_userDataList
```
private List<object> m_userDataList = new();
```
- **描述**
    回调方法数据
<br>

## 属性
#### Coroutine
```
public Coroutine Coroutine { get; set; }
```
- **描述**
    加载资源的协程
<br>

#### Handle
```
public AsyncOperationHandle Handle { get; set; }
```
- **描述**
    异步加载资源的句柄
<br>

## public 方法
#### AddCallback
```
public void AddCallback(LoadAssetCallbacks loadAssetCallbacks, object userData);
```
- **参数**
    |        参数        |     描述     |
    | :----------------: | :----------: |
    | loadAssetCallbacks |   回调方法   |
    |      userData      | 回调方法参数 |
- **描述**
    添加回调方法及参数
<br>

#### InvokeSuccessCallbacks
```
public void InvokeSuccessCallbacks(string assetName, object asset);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | assetName | 资源名字 |
    |   asset   |   资源   |
- **描述**
    调用加载成功的回调方法
<br>

#### InvokeFailureCallbacks
```
public void InvokeFailureCallbacks(string assetName, string errorMsg);
```
- **参数**
    |   参数    |   描述   |
    | :-------: | :------: |
    | assetName | 资源名字 |
    | errorMsg  | 错误信息 |
- **描述**
    调用加载失败的回调方法
<br>

#### Release
```
public void Release();
```
- **描述**
    释放资源
<br>
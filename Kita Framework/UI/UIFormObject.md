# UIFormObject
```
public class UIFormObject : ObjectBase;
```
- **描述**
    UI 窗体对应的 ObjectBase 类，定于 UI 窗体相关的生命周期方法
> [ObjectBase](../Object%20Pool/ObjectBase.md)

## public 方法
#### OnSpawn
```
public override void OnSpawn();
```
- **实现逻辑**
    调用父类的 OnSpawn 方法，并调用包装的 UIForm 对象对应的 GameObject 的 SetActive(true)
<br>

#### OnUnspawn
```
public override void OnUnspawn();
```
- **实现逻辑**
    调用父类的 OnUnspawn 方法，并调用包装的 UIForm 对象对应的 GameObject 的 SetActive(false)
<br>

#### Release
```
public override void Release();
```
- **实现逻辑**
    调用父类的 Release 方法，并 Destroy 包装的 UIForm 对象对应的 GameObject
<br>
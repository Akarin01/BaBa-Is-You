# ObjectPool 模块

![ObjectPool 模块类图](../Img/Object%20Pool/ObjectPoolUML.png)

- [ObjectPoolManager](./ObjectPoolManager.md)
- [IObjectPool](./IObjectPool.md)
- [ObjectPoolBase](./ObjectPoolBase.md)
- [ObjectPool](./ObjectPool.md)
- [ObjectInfo](./ObjectInfo.md)
- [ObjectBase](./ObjectBase.md)

## 使用方法
1. 用户定义一个继承自 ObjectBase 的类，用于定义对象池对象的生命周期方法
2. 通过 ObjectPoolManager 创建或获取一个对象池
3. 通过 ObjectPool 的 Spawn 方法来获取对象，通过 Unspawn 来归还对象
4. 如果对象为 null，需通过 Register 方法向对象池注册对象。注册对象需要先创建一个 ObjectBase 实例，并初始化 m_isUsed 为 true，再将对象 Register 到对象池

## Q&A
- 为什么对象池对象需要包装两层？
> ObjectBase 定义了对象的生命周期方法；ObjectInfo 是对象池内部维护对象使用状态的类

- 为什么同时需要 ObjectBase 和 IObjectPool？
> IObjectPool\<T1\> 和 IObjectPool\<T2\> 是不同的类型
> ObjectBase 使得 Manager 可以统一存储不同类型的对象池
> IObjectPool\<T\> 则是外部访问对象池的接口
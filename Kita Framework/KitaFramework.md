# Kita Framework
> 该项目为框架学习总结，是基于个人理解而简化的 
> ## [UnityGameFramework](https://github.com/EllanJiang/UnityGameFramework)

- ### [FrameworkEntry](./Base/FrameworkEntry.md)

- ### [FrameworkManager](./Base/FrameworkManager.md)

- ### [Data Table 模块](./Data%20Table/DataTable%20Overview.md)

- ### [Event 模块](./Event/Event%20Overview.md)

- ### [Fsm 模块](./Fsm/Fsm%20Overview.md)

- ### [Object Pool 模块](./Object%20Pool/ObjectPool%20Overview.md)

- ### [Procedure 模块](./Procedure/Procedure%20Overview.md)

- ### [Resource 模块](./Resource/Resource%20Overview.md)

- ### [Scene 模块](./Scene/Scene%20Overview.md)

- ### [UI 模块](./UI/UI%20Overview.md)

## 框架流程
1. 各个管理器在 Awake 阶段向 FrameworkEntry 注册，并初始化
2. 各个管理器在 Start 阶段利用其他管理器初始化剩余部分
3. 在 Update 的第一帧最后，切换流程与场景
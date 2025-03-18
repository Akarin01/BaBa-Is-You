# FrameworkEntry
```
public static class FrameworkEntry;
```
- **描述**
    框架入口，采用服务定位器模式，提供各个模块的全局访问

## 成员变量
#### m_frameworkManagers
```
private static LinkedList<FrameworkManager> m_frameworkManagers;
```
- **描述**
    以链表存储的管理器
<br>

## public 方法
#### RegisterManager
```
public static void RegisterManager(FrameworkManager frameworkManager);
```
- **参数**
    |       参数       |       描述       |
    | :--------------: | :--------------: |
    | frameworkManager | 注册的管理器对象 |
- **描述**
    注册管理器的方法
- **异常**
    如果对象为 null，打印错误日志；如果该类型对象已存在，打印错误日志
<br>

#### GetManager
```
public static T GetManager<T>() where T : FrameworkManager;
```
- **参数**
    | 参数  |       描述       |
    | :---: | :--------------: |
    |   T   | 获取的管理器类型 |
- **描述**
    获取管理器的方法，时间复杂度 O(n)。如果不存在，返回 null
- **异常**
    如果查找不到对应类型的对象，打印错误日志
```
public static FrameworkManager GetManager(Type t);
```
- **参数**
    | 参数  |       描述       |
    | :---: | :--------------: |
    |   t   | 获取的管理器类型 |
- **描述**
    获取管理器的方法，时间复杂度 O(n)。如果不存在，返回 null
- **异常**
    如果查找不到对应类型的对象，打印错误日志
<br>

#### Shutdown
```
public static void Shutdown();
```
- **描述**
    关闭框架，清理资源。会调用所有注册的管理器的 Shutdown 方法
<br>
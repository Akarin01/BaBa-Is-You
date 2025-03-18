# DataTable 模块

![DataTable 模块类图](../Img/Data%20Table/DataTableUML.png)

- [DataTableManager](./DataTableManager.md)
- [DataTableBase](./DataTableBase.md)
- [IDataTable](./IDataTable.md)
- [DataTable](./DataTable.md)
- [DataRowBase](./DataRowBase.md)

## 使用方法
1. 用户创建数据表对应的 .csv 文件，配置好数据
2. 根据 .csv 文件，定义继承自 DataRowBase 的子类，定义需要的数据属性
3. 创建并初始化数据表
4. 在需要数据表数据的地方获取数据表，再获取数据表行
> UIManager 的拓展方法利用 DataTable 模块实现了 UIFormId 到对应 Path、GroupName 的映射
> 这么做解耦了 UIManager 与游戏需要的数据表、UIFormId

## Q&A
- DataTableBase 什么都没有定义？
> DataTableBase 用于 DataTableManager 的统一存储，因为 DataTable\<T1\> 的 DataTable\<T2\> 不是一个类型
using UnityEngine;

/// <summary>
/// 逻辑基类，处理逻辑
/// </summary>
public abstract class LogicBase : ScriptableObject
{
    public abstract void OnUpdate(EntityBase entity);
    public abstract void OnFixedUpdate(EntityBase entity);
    /// <summary>
    /// 尝试交互
    /// </summary>
    /// <param name="initiator">交互的发起者</param>
    /// <param name="receiver">交互的接收者（拥有该逻辑的实体）</param>
    /// <returns>是否可以移动</returns>
    public abstract bool TryInteract(EntityBase initiator, EntityBase receiver);
}

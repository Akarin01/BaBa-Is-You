/// <summary>
/// �߼����࣬�����߼�
/// </summary>
public abstract class LogicBase
{
    public abstract void OnUpdate(EntityBase entity);
    public abstract void OnFixedUpdate(EntityBase entity);
    /// <summary>
    /// ���Խ���
    /// </summary>
    /// <param name="initiator">�����ķ�����</param>
    /// <param name="receiver">�����Ľ����ߣ�ӵ�и��߼���ʵ�壩</param>
    /// <returns></returns>
    public abstract bool TryInteract(EntityBase initiator, EntityBase receiver);
}

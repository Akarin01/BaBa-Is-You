using UnityEngine;

public class PushLogic : LogicBase
{
    public override void OnFixedUpdate(EntityBase entity)
    {
        Debug.Log("PushLogic OnFixedUpdate");
    }

    public override void OnUpdate(EntityBase entity)
    {
        Debug.Log("PushLogic OnUpdate");
    }

    public override bool TryInteract(EntityBase initiator, EntityBase receiver)
    {
        if (receiver.IsMoving)
        {
            return false;
        }

        Vector2Int dir = Vector2Int.RoundToInt(receiver.Position - initiator.Position);
        receiver.TryInteractByDir(dir);
        receiver.MoveByDir(dir);
        return true;
    }
}
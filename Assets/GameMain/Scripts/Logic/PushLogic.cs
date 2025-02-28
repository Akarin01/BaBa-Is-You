using UnityEngine;

public class PushLogic : LogicBase
{
    public override void OnFixedUpdate(EntityBase entity)
    {
        
    }

    public override void OnUpdate(EntityBase entity)
    {
        
    }

    public override bool TryInteract(EntityBase initiator, EntityBase receiver)
    {
        if (receiver.IsMoving)
        {
            return false;
        }

        Vector2Int dir = Vector2Int.RoundToInt(receiver.Position - initiator.Position);
        bool interacted = receiver.TryInteractByDir(dir);
        if (interacted)
        {
            receiver.MoveByDir(dir);
        }
        return interacted;
    }
}
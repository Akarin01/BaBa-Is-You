using UnityEngine;

public class YouLogic : LogicBase
{
    public override void OnFixedUpdate(EntityBase entity)
    {
        
    }

    public override void OnUpdate(EntityBase entity)
    {
        GetInput(entity);
    }

    public override bool TryInteract(EntityBase initiator, EntityBase receiver)
    {
        return false;
    }

    private void GetInput(EntityBase entity)
    {
        if (entity.IsMoving)
        {
            return;
        }

        int h = 0;
        bool leftDown = Input.GetKeyDown(KeyCode.A);
        bool rightDown = Input.GetKeyDown(KeyCode.D);
        if (leftDown && !rightDown)
        {
            h = -1;
        }
        else if (!leftDown && rightDown)
        {
            h = 1;
        }
        else
        {
            h = 0;
        }
        if (h != 0)
        {
            // 水平移动
            InteractAndMove(entity, new Vector2(h, 0));
            return;
        }

        int v = 0;
        bool downDown = Input.GetKeyDown(KeyCode.S);
        bool upDown = Input.GetKeyDown(KeyCode.W);
        if (downDown && !upDown)
        {
            v = -1;
        }
        else if (!downDown && upDown)
        {
            v = 1;
        }
        else
        {
            v = 0;
        }
        if (v != 0)
        {
            // 竖直移动
            InteractAndMove(entity, new Vector2(0, v));
            return;
        }

        // 没有移动输入
    }

    /// <summary>
    /// 朝指定方向交互并移动
    /// </summary>
    /// <param name="entity">需要移动的实体</param>
    /// <param name="moveDir">指定方向</param>
    private void InteractAndMove(EntityBase entity, Vector2 moveDir)
    {
        bool interacted = entity.TryInteractByDir(moveDir);
        if (interacted)
        {
            entity.MoveByDir(moveDir);
        }
    }
}
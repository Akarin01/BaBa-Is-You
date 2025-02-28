using KitaFramework;
using UnityEngine;

public class WinLogic : LogicBase
{
    public override void OnFixedUpdate(EntityBase entity)
    {
        
    }

    public override void OnUpdate(EntityBase entity)
    {
        
    }

    public override bool TryInteract(EntityBase initiator, EntityBase receiver)
    {
        // 游戏胜利
        if (initiator.Logic is YouLogic)
        {
            GameEntry.UIManager.OpenUI<WinForm>();
            Debug.Log("Win!");
        }
        return true;
    }
}
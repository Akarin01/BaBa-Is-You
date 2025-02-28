using UnityEngine;

public class EdgeEntity : EntityBase
{
    public override LogicBase Logic
    {
        get => s_logic;
        set
        {
            Debug.LogError("Edge Entity can't change logic");
        }
    }
    private static LogicBase s_logic = LogicManager.GetLogic<StopLogic>();
}
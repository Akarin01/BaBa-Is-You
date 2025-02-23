using UnityEngine;

public class RuleBlockEntity : EntityBase
{
    public override LogicBase Logic
    {
        get => s_logic;
        set
        {
            Debug.LogError("Rule Block can't change logic");
        }
    }

    private static LogicBase s_logic = LogicManager.GetLogic<PushLogic>();
}
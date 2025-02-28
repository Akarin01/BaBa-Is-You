public class PlayerEntity : EntityBase
{
    public override LogicBase Logic
    {
        get => s_logic;
        set => s_logic = value;
    }

    private static LogicBase s_logic;

    protected override void Awake()
    {
        base.Awake();
        s_logic = LogicManager.GetDefaultLogic();
    }
}
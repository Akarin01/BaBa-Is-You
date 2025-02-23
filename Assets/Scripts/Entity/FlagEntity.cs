public class FlagEntity : EntityBase
{
    public override LogicBase Logic
    {
        get => s_logic;
        set => s_logic = value;
    }

    private static LogicBase s_logic = new WinLogic();
}
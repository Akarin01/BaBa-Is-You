using KitaFramework;

namespace BabaIsYou
{
    public class ProcedureMenu : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.UIManager.OpenUI<MenuForm>();
        }
    }
}
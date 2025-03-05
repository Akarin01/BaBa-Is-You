using KitaFramework;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class ProcedureLauncher : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            procedureOwner.SetData("NextScene", Config.MENU_SCENE_INDEX);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}
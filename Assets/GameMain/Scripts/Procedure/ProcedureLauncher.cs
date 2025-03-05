using KitaFramework;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class ProcedureLauncher : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            SceneManager.LoadScene("Menu");

            ChangeState<ProcedureMenu>(procedureOwner);
        }
    }
}
using BabaIsYou;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KitaFramework
{
    public class ProcedureChangeScene : ProcedureBase
    {
        private bool m_gotoMenu;

        private AsyncOperation m_changeSceneOperation;

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            int nextSceneIndex = (int)procedureOwner.GetData("NextScene");
            m_gotoMenu = nextSceneIndex == Config.MENU_SCENE_INDEX;
            m_changeSceneOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (!m_changeSceneOperation.isDone)
            {
                return;
            }

            if (m_gotoMenu)
            {
                ChangeState<ProcedureMenu>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }
    }
}
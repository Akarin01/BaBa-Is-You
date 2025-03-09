using KitaFramework;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_startGame;

        public void StartGame()
        {
            m_startGame = true;
        }

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_startGame = false;

            GameEntry.UI.OpenUI(UIFormId.Menu, this);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (m_startGame)
            {
                procedureOwner.SetData("NextScene", Config.FIRST_LEVEL_SCENE_INDEX);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }
    }
}
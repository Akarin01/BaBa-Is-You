using KitaFramework;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_startGame = false;

        public void StartGame()
        {
            m_startGame = true;
        }

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.UIManager.OpenUI<MenuForm>(this);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (m_startGame)
            {
                SceneManager.LoadScene("Level_1");
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }

        protected internal override void OnExit(IFsm<ProcedureManager> procedureOwner, bool isShutdown)
        {
            m_startGame = false;

            base.OnExit(procedureOwner, isShutdown);
        }
    }
}
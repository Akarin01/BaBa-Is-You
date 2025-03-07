using KitaFramework;

namespace BabaIsYou
{
    public class ProcedureMain : ProcedureBase
    {
        private bool m_isGameWon;

        private void WinGame(object sender, BaseEventArgs e)
        {
            m_isGameWon = true;
        }

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_isGameWon = false;

            GameEntry.EventManager.Subscribe(GameWinArgs.EventID, WinGame);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (m_isGameWon)
            {
                ChangeState<ProcedureWin>(procedureOwner);
            }
        }

        protected internal override void OnExit(IFsm<ProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.EventManager.Unsubscribe(GameWinArgs.EventID, WinGame);

            base.OnExit(procedureOwner, isShutdown);
        }
    }
}
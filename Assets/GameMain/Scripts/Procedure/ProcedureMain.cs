using KitaFramework;

namespace BabaIsYou
{
    public class ProcedureMain : ProcedureBase
    {
        private bool m_win = false;

        private void Win()
        {
            m_win = true;
        }

        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameMode.OnGameWin += Win;
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            if (m_win)
            {
                ChangeState<ProcedureWin>(procedureOwner);
            }
        }

        protected internal override void OnExit(IFsm<ProcedureManager> procedureOwner, bool isShutdown)
        {
            GameMode.OnGameWin -= Win;

            m_win = false;

            base.OnExit(procedureOwner, isShutdown);
        }
    }
}
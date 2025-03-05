using KitaFramework;
using UnityEngine.SceneManagement;
using System;

namespace BabaIsYou
{
    public class ProcedureWin : ProcedureBase
    {
        public enum Choice
        {
            None,
            Next,
            Restart,
            GotoMenu,
        }

        private Choice m_choice = Choice.None;

        public void NextLevel()
        {
            m_choice = Choice.Next;
        }

        public void Restart()
        {
            m_choice = Choice.Restart;
        }

        public void GotoMenu()
        {
            m_choice = Choice.GotoMenu;
        }


        protected internal override void OnEnter(IFsm<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.UIManager.OpenUI<WinForm>(this);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            switch (m_choice)
            {
                case Choice.None:
                    return;
                case Choice.Next:
                    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene("Menu");
                        ChangeState<ProcedureMenu>(procedureOwner);
                    }
                    else
                    {
                        SceneManager.LoadScene(nextSceneIndex);
                        ChangeState<ProcedureMain>(procedureOwner);
                    }
                    break;
                case Choice.Restart:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    ChangeState<ProcedureMain>(procedureOwner);
                    break;
                case Choice.GotoMenu:
                    SceneManager.LoadScene("Menu");
                    ChangeState<ProcedureMenu>(procedureOwner);
                    break;
                default:
                    break;
            }
        }

        protected internal override void OnExit(IFsm<ProcedureManager> procedureOwner, bool isShutdown)
        {
            m_choice = Choice.None;

            base.OnExit(procedureOwner, isShutdown);
        }
    }
}
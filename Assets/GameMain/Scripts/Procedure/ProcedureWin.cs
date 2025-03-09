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

        private Choice m_choice;

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

            m_choice = Choice.None;

            GameEntry.UI.OpenUI(UIFormId.Win, this);
        }

        protected internal override void OnUpdate(IFsm<ProcedureManager> procedureOwner, float deltaTime, float realDeltaTime)
        {
            base.OnUpdate(procedureOwner, deltaTime, realDeltaTime);

            int nextSceneIndex = -1;
            switch (m_choice)
            {
                case Choice.None:
                    return;
                case Choice.Next:
                    nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                    {
                        nextSceneIndex = Config.MENU_SCENE_INDEX;
                    }
                    break;
                case Choice.Restart:
                    nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    break;
                case Choice.GotoMenu:
                    nextSceneIndex = Config.MENU_SCENE_INDEX;
                    break;
                default:
                    break;
            }

            procedureOwner.SetData("NextScene", nextSceneIndex);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}
using KitaFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class WinForm : UIForm
    {
        private ProcedureWin m_procedureWin;

        public void OnNextBtnClicked()
        {
            m_procedureWin.NextLevel();

            Close(false);

            Debug.Log("Next");
        }

        public void OnRestartBtnClicked()
        {
            m_procedureWin.Restart();

            Close(false);

            Debug.Log("Restart");
        }

        public void OnMenuBtnClicked()
        {
            m_procedureWin.GotoMenu();

            Close(true);

            Debug.Log("Menu");
        }

        public override void OnInit()
        {
            base.OnInit();

            Debug.Log("WinForm Init!");
        }

        public override void OnOpen(object data)
        {
            base.OnOpen(data);

            m_procedureWin = (ProcedureWin)data;

            Debug.Log("WinForm Open!");
        }

        public override void OnClose(object data)
        {
            Debug.Log("WinForm Close!");

            m_procedureWin = null;

            base.OnClose(data);
        }

        public override void OnPause()
        {
            base.OnPause();

            Debug.Log("WinForm Pause!");
        }

        public override void OnResume()
        {
            base.OnResume();

            Debug.Log("WinForm Resume!");
        }

        public override void OnRelease()
        {
            base.OnRelease();

            Debug.Log("WinForm Release!");
        }
    }
}
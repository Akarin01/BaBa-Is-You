using UnityEngine;
using UnityEngine.SceneManagement;
using KitaFramework;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BabaIsYou
{
    public class MenuForm : UIForm
    {
        private ProcedureMenu m_procedureMenu;

        public void OnStartBtnClicked()
        {
            m_procedureMenu.StartGame();

            Close(false);

            Debug.Log("Start");
        }

        public void OnSettingBtnClicked()
        {
            GameEntry.UIManager.OpenUI<SettingForm>();
        }

        public void OnQuitBtnClicked()
        {
            Debug.Log("Quit");

            // ÍË³öÓÎÏ·
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public override void OnInit()
        {
            base.OnInit();

            Debug.Log("MenuForm Init!");
        }

        public override void OnOpen(object data)
        {
            base.OnOpen(data);

            m_procedureMenu = (ProcedureMenu)data;

            Debug.Log("MenuForm Open!");
        }

        public override void OnClose(object data)
        {
            Debug.Log("MenuForm Close!");

            m_procedureMenu = null;

            base.OnClose(data);
        }

        public override void OnPause()
        {
            base.OnPause();

            Debug.Log("MenuForm Pause!");
        }

        public override void OnResume()
        {
            base.OnResume();

            Debug.Log("MenuForm Resume!");
        }

        public override void OnRelease()
        {
            base.OnRelease();

            Debug.Log("MenuForm Release!");
        }
    }
}
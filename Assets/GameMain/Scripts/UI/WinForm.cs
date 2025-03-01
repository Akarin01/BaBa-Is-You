using KitaFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BabaIsYou
{
    public class WinForm : UIForm
    {
        public void OnNextBtnClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Close(false);
            Debug.Log("Next");
        }

        public void OnRestartBtnClicked()
        {
            Close(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restart");
        }

        public void OnMenuBtnClicked()
        {
            Close(true);
            SceneManager.LoadScene("Menu");
            Debug.Log("Menu");
        }

        public override void OnInit()
        {
            base.OnInit();

            Debug.Log("WinForm Init!");
        }

        public override void OnOpen()
        {
            base.OnOpen();

            Debug.Log("WinForm Open!");
        }

        public override void OnClose()
        {
            base.OnClose();

            Debug.Log("WinForm Close!");
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
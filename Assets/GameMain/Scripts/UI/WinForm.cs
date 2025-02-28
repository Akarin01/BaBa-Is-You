using KitaFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Close(false);
        Debug.Log("Restart");
    }

    public void OnMenuBtnClicked()
    {
        SceneManager.LoadScene("Menu");
        Close(false);
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
        Debug.Log("WinForm Close!");
    }

    public override void OnPause()
    {
        Debug.Log("WinForm Pause!");
    }

    public override void OnResume()
    {
        Debug.Log("WinForm Resume!");
    }

    public override void OnRelease()
    {
        base.OnRelease();

        Debug.Log("WinForm Release!");
    }
}

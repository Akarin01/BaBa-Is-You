using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuForm : UIForm
{
    public void OnStartBtnClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Close(false);
        Debug.Log("Start");
    }

    public void OnSettingBtnClicked()
    {
        GameEntry.UIManager.OpenUI<SettingForm>();
    }

    public void OnQuitBtnClicked()
    {
        Close(false);
        Debug.Log("Quit");
    }

    public override void OnOpen()
    {
        base.OnOpen();

        Debug.Log("MenuForm Open!");
    }

    public override void OnClose()
    {
        Debug.Log("MenuForm Close!");
    }

    public override void OnPause()
    {
        Debug.Log("MenuForm Pause!");
    }

    public override void OnResume()
    {
        Debug.Log("MenuForm Resume!");
    }
}

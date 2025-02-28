using KitaFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
        Debug.Log("Quit");

        // ÍË³öÓÎÏ·
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

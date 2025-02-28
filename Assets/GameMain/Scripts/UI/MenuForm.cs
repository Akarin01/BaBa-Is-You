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
        Close(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public override void OnOpen()
    {
        base.OnOpen();

        Debug.Log("MenuForm Open!");
    }

    public override void OnClose()
    {
        base.OnClose();

        Debug.Log("MenuForm Close!");
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

using UnityEngine;

public class SettingForm : UIForm
{
    public void OnQuitBtnClicked()
    {
        Close(false);
    }

    public override void OnOpen()
    {
        base.OnOpen();

        Debug.Log("SettingForm Open!");
    }

    public override void OnClose()
    {
        Debug.Log("SettingForm Close!");
    }

    public override void OnPause()
    {
        Debug.Log("SettingForm Pause!");
    }

    public override void OnResume()
    {
        Debug.Log("SettingForm Resume!");
    }
}

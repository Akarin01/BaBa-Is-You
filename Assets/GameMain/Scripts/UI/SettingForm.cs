using KitaFramework;
using UnityEngine;

namespace BabaIsYou
{
    public class SettingForm : UIForm
    {
        public void OnQuitBtnClicked()
        {
            Close(false);
        }

        public override void OnInit()
        {
            base.OnInit();

            Debug.Log("SettingForm Init!");
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

        public override void OnRelease()
        {
            base.OnRelease();

            Debug.Log("SettingForm Release!");
        }
    }
}
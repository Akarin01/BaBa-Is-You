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

        public override void OnInit(string groupName)
        {
            base.OnInit(groupName);

            Debug.Log("SettingForm Init!");
        }

        public override void OnOpen(object data)
        {
            base.OnOpen(data);

            Debug.Log("SettingForm Open!");
        }

        public override void OnClose(object data)
        {
            Debug.Log("SettingForm Close!");

            base.OnClose(data);
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
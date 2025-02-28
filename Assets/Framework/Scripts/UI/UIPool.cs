using UnityEngine;

namespace KitaFramework
{

    /// <summary>
    /// UI 池，管理 UI 对象的创建、回收、销毁
    /// </summary>
    public class UIPool : MonoBehaviour
    {
        public UIForm Spawn(string name)
        {
            UIForm uiForm = Resources.Load<UIForm>("UIForms/" + name);
            uiForm = Instantiate(uiForm, transform);
            uiForm.OnInit();
            return uiForm;
        }

        public void UnSpawn(UIForm uiForm)
        {
            Destroy(uiForm.gameObject);
        }
    }
}
using UnityEngine;

namespace KitaFramework
{
    public class UIFormObject : ObjectBase
    {
        public override void OnSpawn()
        {
            base.OnSpawn();

            Debug.Log("UIForm OnSpawn");
            ((UIForm)Target).gameObject.SetActive(true);
        }

        public override void OnUnspawn()
        {
            base.OnUnspawn();

            Debug.Log("UIForm OnUnspawn");
            ((UIForm)Target).gameObject.SetActive(false);
        }

        public override void Release()
        {
            base.Release();

            // 销毁 Target GameObject
            GameObject.Destroy(((UIForm)Target).gameObject);
        }
    }
}
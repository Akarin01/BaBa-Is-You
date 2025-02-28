using UnityEngine;

namespace KitaFramework
{
    public class UIFormObject : ObjectBase
    {
        public override void OnSpawn()
        {
            base.OnSpawn();

            ((UIForm)Target).gameObject.SetActive(true);
        }

        public override void OnUnspawn()
        {
            base.OnUnspawn();

            ((UIForm)Target).gameObject.SetActive(false);
        }
    }
}
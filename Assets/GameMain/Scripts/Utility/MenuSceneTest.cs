using UnityEngine;

namespace BabaIsYou
{
    public class MenuSceneTest : MonoBehaviour
    {
        private bool firstFrame = true;

        private void Update()
        {
            if (firstFrame)
            {
                GameEntry.UIManager.OpenUI<MenuForm>();
                firstFrame = false;
            }
        }
    }
}
using UnityEngine;
using KitaFramework;

namespace BabaIsYou
{
    public partial class GameEntry : MonoBehaviour
    {
        public static UIManager UIManager { get; private set; }
        public static ObjectPoolManager ObjectPoolManager { get; private set; }
        public static EventManager EventManager { get; private set; }

        private void InitBuildInManager()
        {
            UIManager = KitaFramework.FrameworkEntry.GetManager<UIManager>();
            ObjectPoolManager = KitaFramework.FrameworkEntry.GetManager<ObjectPoolManager>();
            EventManager = KitaFramework.FrameworkEntry.GetManager<EventManager>();
        }
    }
}
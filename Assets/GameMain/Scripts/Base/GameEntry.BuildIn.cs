using UnityEngine;
using KitaFramework;

namespace BabaIsYou
{
    public partial class GameEntry : MonoBehaviour
    {
        public static UIManager UI { get; private set; }
        public static ObjectPoolManager ObjectPool { get; private set; }
        public static EventManager Event { get; private set; }

        private void InitBuildInManager()
        {
            UI = KitaFramework.FrameworkEntry.GetManager<UIManager>();
            ObjectPool = KitaFramework.FrameworkEntry.GetManager<ObjectPoolManager>();
            Event = KitaFramework.FrameworkEntry.GetManager<EventManager>();
        }
    }
}
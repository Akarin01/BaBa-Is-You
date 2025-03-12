using UnityEngine;
using KitaFramework;

namespace BabaIsYou
{
    public partial class GameEntry : MonoBehaviour
    {
        public static UIManager UI { get; private set; }
        public static ObjectPoolManager ObjectPool { get; private set; }
        public static EventManager Event { get; private set; }
        public static DataTableManager DataTable { get; private set; }
        public static ResourceManager Resource { get; private set; }
        public static SceneManager Scene { get; private set; }

        private void InitBuildInManager()
        {
            UI = FrameworkEntry.GetManager<UIManager>();
            ObjectPool = FrameworkEntry.GetManager<ObjectPoolManager>();
            Event = FrameworkEntry.GetManager<EventManager>();
            DataTable = FrameworkEntry.GetManager<DataTableManager>();
            Resource = FrameworkEntry.GetManager<ResourceManager>();
            Scene = FrameworkEntry.GetManager<SceneManager>();
        }
    }
}
using UnityEngine;

namespace KitaFramework
{
    public class ResourceManager : FrameworkManager
    {
        [field: SerializeField] public SceneLoaderBase SceneLoader { get; private set; }
        [field: SerializeField] public AssetLoaderBase AssetLoader { get; private set; }


        protected override void Awake()
        {
            base.Awake();
        }

        public override void Shutdown()
        {
            SceneLoader.Shutdown();

            AssetLoader.Shutdown();
        }
    }
}
namespace KitaFramework
{
    public class ResourceManager : FrameworkManager
    {
        public ISceneLoader SceneLoader { get; private set; }
        public IAssetLoader AssetLoader { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            SceneLoader = new AddressableSceneLoader();
            AssetLoader = new AddressableAssetLoader();
        }

        public override void Shutdown()
        {
            SceneLoader.Shutdown();

            AssetLoader.Shutdown();
        }
    }
}
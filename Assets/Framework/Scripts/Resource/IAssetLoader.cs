namespace KitaFramework
{
    public interface IAssetLoader
    {
        public void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData);
        public void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData);
        public void Shutdown();
    }
}
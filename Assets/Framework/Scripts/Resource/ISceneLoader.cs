namespace KitaFramework
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);

        public void UnloadScene(string sceneAssetName, LoadSceneCallbacks unloadSceneCallbacks, object userData);

        public void Shutdown();
    }
}
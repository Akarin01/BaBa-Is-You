namespace KitaFramework
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);

        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);

        public void Shutdown();
    }
}
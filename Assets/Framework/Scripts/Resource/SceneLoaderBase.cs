using UnityEngine;

namespace KitaFramework
{
    public abstract class SceneLoaderBase : MonoBehaviour
    {
        public abstract void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);

        public abstract void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);

        public abstract void Shutdown();
    }
}
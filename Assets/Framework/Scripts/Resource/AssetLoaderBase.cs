using UnityEngine;

namespace KitaFramework
{
    public abstract class AssetLoaderBase : MonoBehaviour
    {
        public abstract void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData);
        public abstract void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData);
        public abstract void Shutdown();
    }
}
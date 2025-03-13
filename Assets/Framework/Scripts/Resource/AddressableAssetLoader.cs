using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace KitaFramework
{
    public class AddressableAssetLoader : IAssetLoader
    {
        private Dictionary<string, List<AsyncOperationHandle>> m_loadedAssets;

        public AddressableAssetLoader()
        {
            m_loadedAssets = new();
        }

        public void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            var handle = Addressables.LoadAssetAsync<TObject>(assetName);
            handle.Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Failed)
                    {
                        // 加载失败
                        loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                            handle.OperationException?.Message ?? $"Unknown error", userData);
                        Addressables.Release(handle);
                        return;
                    }

                    if (handle.Result is TObject asset)
                    {
                        // 加载成功
                        loadAssetCallbacks?.LoadAssetSuccessCallback?.Invoke(assetName, asset, userData);
                        if (!m_loadedAssets.ContainsKey(assetName))
                        {
                            m_loadedAssets.Add(assetName, new List<AsyncOperationHandle>());
                        }
                        m_loadedAssets[assetName].Add(handle);
                    }
                    else
                    {
                        loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                            $"{assetName} is not {typeof(TObject)}", userData);
                        Addressables.Release(handle);
                    }
                };
        }

        public void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData)
        {
            if (!m_loadedAssets.TryGetValue(assetName, out var handleList))
            {
                unloadAssetCallbacks?.UnloadAssetFailureCallback(assetName,
                    $"{ assetName } is not loaded", userData);
                return;
            }

            foreach (var handle in handleList)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            m_loadedAssets.Remove(assetName);
        }

        public void Shutdown()
        {
            foreach (var handleLinkedList in m_loadedAssets.Values)
            {
                foreach (var handle in handleLinkedList)
                {
                    if (handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                }

                handleLinkedList.Clear();
            }

            m_loadedAssets.Clear();
        }
    }
}
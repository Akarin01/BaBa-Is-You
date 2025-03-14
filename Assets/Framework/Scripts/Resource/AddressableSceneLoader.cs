using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace KitaFramework
{
    public class AddressableSceneLoader : SceneLoaderBase
    {
        private Dictionary<string, AsyncOperationHandle<SceneInstance>> m_sceneAssetNameHandlerMaps;

        public AddressableSceneLoader()
        {
            m_sceneAssetNameHandlerMaps = new();
        }

        public override void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData)
        {
            if (m_sceneAssetNameHandlerMaps.ContainsKey(sceneAssetName))
            {
                // 如果该场景已经被加载
                loadSceneCallbacks?.LoadSceneFailureCallback?.Invoke(sceneAssetName, $"Scene {sceneAssetName} is already loaded", userData);
                return;
            }
            var handler = Addressables.LoadSceneAsync(sceneAssetName,
                UnityEngine.SceneManagement.LoadSceneMode.Additive);

            handler.Completed += (handler) =>
            {
                if (handler.Status == AsyncOperationStatus.Failed)
                {
                    loadSceneCallbacks?.LoadSceneFailureCallback?.Invoke(sceneAssetName, 
                        handler.OperationException?.Message ?? "Unknown error", userData);

                    // 清理无效句柄
                    m_sceneAssetNameHandlerMaps.Remove(sceneAssetName);
                    Addressables.Release(handler);
                    return;
                }

                loadSceneCallbacks?.LoadSceneSuccessCallback?.Invoke(sceneAssetName, userData);
            };

            m_sceneAssetNameHandlerMaps.Add(sceneAssetName, handler);
        }

        public override void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData)
        {
            if (!m_sceneAssetNameHandlerMaps.ContainsKey(sceneAssetName))
            {
                unloadSceneCallbacks?.UnloadSceneFailureCallback?.Invoke(sceneAssetName, $"{sceneAssetName} is not loaded", userData);
                return;
            }

            var handler = m_sceneAssetNameHandlerMaps[sceneAssetName];

            Addressables.UnloadSceneAsync(handler, UnityEngine.SceneManagement.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects)
                .Completed += (handler) =>
                {
                    if (handler.Status == AsyncOperationStatus.Failed)
                    {
                        unloadSceneCallbacks?.UnloadSceneFailureCallback?.Invoke(sceneAssetName,
                            handler.OperationException?.Message ?? "Unknown error", userData);
                        return;
                    }

                    // 成功卸载，清理资源
                    m_sceneAssetNameHandlerMaps.Remove(sceneAssetName);
                    Addressables.Release(handler);
                    unloadSceneCallbacks?.UnloadSceneSuccessCallback?.Invoke(sceneAssetName, userData);
                };
        }

        public override void Shutdown()
        {
            // 卸载场景
            foreach (var handler in m_sceneAssetNameHandlerMaps.Values)
            {
                Addressables.UnloadSceneAsync(handler, UnityEngine.SceneManagement.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
            m_sceneAssetNameHandlerMaps.Clear();
        }
    }
}
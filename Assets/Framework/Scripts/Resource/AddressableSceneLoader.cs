using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace KitaFramework
{
    public class AddressableSceneLoader : ISceneLoader
    {
        private Dictionary<string, AsyncOperationHandle<SceneInstance>> m_sceneAssetNameHandlerMaps;

        public AddressableSceneLoader()
        {
            m_sceneAssetNameHandlerMaps = new();
        }

        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData)
        {
            if (m_sceneAssetNameHandlerMaps.ContainsKey(sceneAssetName))
            {
                // 如果该场景已经被加载
                throw new ArgumentException($"Scene {sceneAssetName} has been loaded");
            }
            var handler = Addressables.LoadSceneAsync(sceneAssetName,
                UnityEngine.SceneManagement.LoadSceneMode.Additive);

            handler.Completed += (handler) =>
            {
                if (handler.Status == AsyncOperationStatus.Failed)
                {
                    loadSceneCallbacks?.LoadSceneFailureCallback?.Invoke(sceneAssetName, $"LoadScene {sceneAssetName} failed", userData);
                    return;
                }

                loadSceneCallbacks?.LoadSceneSuccessCallback?.Invoke(sceneAssetName, userData);
            };

            m_sceneAssetNameHandlerMaps.Add(sceneAssetName, handler);
        }

        public void UnloadScene(string sceneAssetName, LoadSceneCallbacks unloadSceneCallbacks, object userData)
        {
            if (!m_sceneAssetNameHandlerMaps.ContainsKey(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} hasn't been loaded");
            }

            var handler = m_sceneAssetNameHandlerMaps[sceneAssetName];

            Addressables.UnloadSceneAsync(handler, UnityEngine.SceneManagement.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects)
                .Completed += (handler) =>
                {
                    if (handler.Status == AsyncOperationStatus.Failed)
                    {
                        unloadSceneCallbacks?.LoadSceneFailureCallback?.Invoke(sceneAssetName, $"LoadScene {sceneAssetName} failed", userData);
                        return;
                    }

                    unloadSceneCallbacks?.LoadSceneSuccessCallback?.Invoke(sceneAssetName, userData);

                    m_sceneAssetNameHandlerMaps.Remove(sceneAssetName);
                };
        }

        public void Shutdown()
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
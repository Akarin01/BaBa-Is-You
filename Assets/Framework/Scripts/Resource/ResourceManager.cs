using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public class ResourceManager : FrameworkManager
    {
        private Dictionary<string, AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>> m_sceneAssetNameHandlerMaps;

        protected override void Awake()
        {
            base.Awake();

            m_sceneAssetNameHandlerMaps = new();
        }

        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData)
        {
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
            m_sceneAssetNameHandlerMaps.Remove(sceneAssetName);

            Addressables.UnloadSceneAsync(handler, UnityEngine.SceneManagement.UnloadSceneOptions.None)
                .Completed += (handler) =>
                {
                    if (handler.Status == AsyncOperationStatus.Failed)
                    {
                        unloadSceneCallbacks?.LoadSceneFailureCallback?.Invoke(sceneAssetName, $"LoadScene {sceneAssetName} failed", userData);
                        return;
                    }

                    unloadSceneCallbacks?.LoadSceneSuccessCallback?.Invoke(sceneAssetName, userData);
                };
        }

        public override void Shutdown()
        {

        }
    }
}
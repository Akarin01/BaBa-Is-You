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

        public void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            Addressables.LoadAssetAsync<TObject>(assetName)
                .Completed += (handler) =>
                {
                    if (handler.Status != AsyncOperationStatus.Succeeded)
                    {
                        loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName, null, userData);
                        return;
                    }

                    loadAssetCallbacks?.LoadAssetSuccessCallback?.Invoke(assetName, handler.Result, userData);
                };
        }

        public override void Shutdown()
        {
            m_sceneAssetNameHandlerMaps.Clear();
        }
    }
}
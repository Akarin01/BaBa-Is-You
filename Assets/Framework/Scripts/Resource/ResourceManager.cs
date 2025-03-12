using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public class ResourceManager : FrameworkManager
    {
        private Dictionary<string, AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>> m_sceneAssetNameHandlerMaps;

        private Dictionary<string, AsyncOperationHandle> m_loadedAssetHandlers;
        private Dictionary<string, AsyncOperationHandle> m_loadingAssetHandlers;

        protected override void Awake()
        {
            base.Awake();

            m_sceneAssetNameHandlerMaps = new();
            m_loadedAssetHandlers = new();
            m_loadingAssetHandlers = new();
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
            if (m_loadedAssetHandlers.TryGetValue(assetName, out var handler))
            {
                // 如果该 asset 已经被加载，直接调用回调方法
                if (handler.Result is TObject)
                {
                    loadAssetCallbacks.LoadAssetSuccessCallback?.Invoke(assetName, m_loadedAssetHandlers[assetName].Result, userData);
                }
                else
                {
                    loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(assetName, $"Asset {assetName} is not {typeof(TObject)}", userData);
                }
                return;
            }
            if (m_loadingAssetHandlers.TryGetValue(assetName, out handler))
            {
                // 如果该 asset 正在被加载，添加回调方法
                if (handler.IsDone)
                {
                    // 已经加载完成，触发回调
                    loadAssetCallbacks.LoadAssetSuccessCallback?.Invoke(assetName, handler.Result, userData);
                }
                else
                {
                    // 还没加载完成，添加回调
                    handler.Completed +=
                    handler =>
                    {
                        if (handler.Status != AsyncOperationStatus.Succeeded)
                        {
                            loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(assetName, $"Fail to load asset {assetName}", userData);
                            return;
                        }
                        loadAssetCallbacks.LoadAssetSuccessCallback?.Invoke(assetName, handler.Result, userData);
                    };
                }
                return;
            }
            // 该 asset 未被加载，异步加载
            handler = Addressables.LoadAssetAsync<TObject>(assetName);
            m_loadingAssetHandlers.Add(assetName, handler);
            handler.Completed +=
                handler =>
                {
                    m_loadingAssetHandlers.Remove(assetName);

                    if (handler.Status != AsyncOperationStatus.Succeeded)
                    {
                        loadAssetCallbacks.LoadAssetFailureCallback?.Invoke(assetName, $"Fail to load asset {assetName}", userData);
                        return;
                    }
                    loadAssetCallbacks.LoadAssetSuccessCallback?.Invoke(assetName, handler.Result, userData);
                    m_loadedAssetHandlers.Add(assetName, handler);
                };
        }

        public void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData)
        {
            if (m_loadedAssetHandlers.TryGetValue(assetName, out var handler))
            {
                // 如果该 asset 已经被加载，卸载
                Addressables.Release(handler);
                m_loadedAssetHandlers.Remove(assetName);
                unloadAssetCallbacks.UnloadAssetSuccessCallback(assetName, userData);
                return;
            }
            if (m_loadingAssetHandlers.TryGetValue(assetName, out handler))
            {
                // 如果该 asset 正在被加载，卸载
                Addressables.Release(handler);
                m_loadingAssetHandlers.Remove(assetName);
                unloadAssetCallbacks.UnloadAssetSuccessCallback(assetName, userData);
                return;
            }
            // 未加载的 asset
            unloadAssetCallbacks.UnloadAssetFailureCallback(assetName, $"Asset {assetName} has not been loaded", userData);
        }

        public override void Shutdown()
        {
            // 卸载场景
            foreach (var handler in m_sceneAssetNameHandlerMaps.Values)
            {
                Addressables.UnloadSceneAsync(handler, UnityEngine.SceneManagement.UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
            m_sceneAssetNameHandlerMaps.Clear();

            // 释放已加载资产
            foreach (var handler in m_loadedAssetHandlers.Values)
            {
                Addressables.Release(handler);
            }
            m_loadedAssetHandlers.Clear();

            // 释放正在加载的资产
            foreach (var handler in m_loadingAssetHandlers.Values)
            {

                Addressables.Release(handler);
            }
            m_loadingAssetHandlers.Clear();
        }
    }
}
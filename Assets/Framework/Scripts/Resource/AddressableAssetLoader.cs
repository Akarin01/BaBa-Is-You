using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace KitaFramework
{
    public class AddressableAssetLoader : AssetLoaderBase
    {
        private class LoadingAssetInfo
        {
            public LoadAssetCallbacks LoadAssetCallbacks { get; private set; }
            public object UserData { get; private set; }

            public LoadingAssetInfo(LoadAssetCallbacks loadAssetCallbacks, object userData)
            {
                LoadAssetCallbacks = loadAssetCallbacks;
                UserData = userData;
            }
        }

        private Dictionary<string, AsyncOperationHandle> m_loadedAssets;
        private Dictionary<string, List<LoadingAssetInfo>> m_loadingAssets;
        private Dictionary<string, Coroutine> m_loadingCoroutines;

        public AddressableAssetLoader()
        {
            m_loadedAssets = new();
            m_loadingAssets = new();
            m_loadingCoroutines = new();
        }

        public override void LoadAsset<TObject>(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            if (m_loadedAssets.TryGetValue(assetName, out var handle))
            {
                // 已经加载过该资源
                if (handle.Result is not TObject asset)
                {
                    // 无法转换到指定类型
                    loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                        $"{assetName} is not {typeof(TObject)}", userData);
                    Addressables.Release(handle);
                }
                else
                {
                    // 转换成功
                    loadAssetCallbacks?.LoadAssetSuccessCallback?.Invoke(assetName,
                    asset, userData);
                }
                return;
            }
            if (m_loadingAssets.TryGetValue(assetName, out var loadingAssetInfoList))
            {
                // 正在加载该资源，添加加载完成的回调方法
                var loadingAssetInfo = new LoadingAssetInfo(loadAssetCallbacks, userData);
                loadingAssetInfoList.Add(loadingAssetInfo);

                return;
            }

            // 还没加载过该资源，加载
            m_loadingAssets.Add(assetName, new List<LoadingAssetInfo>
            {
                new LoadingAssetInfo(loadAssetCallbacks, userData)
            });

            var coroutine = StartCoroutine(LoadAssetCO<TObject>(assetName));
            m_loadingCoroutines.Add(assetName, coroutine);
        }

        public override void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData)
        {
            if (!m_loadedAssets.TryGetValue(assetName, out var handle))
            {
                unloadAssetCallbacks?.UnloadAssetFailureCallback?.Invoke(assetName,
                    $"Asset {assetName} is not loaded or already unloaded", userData);
                return;
            }

            Addressables.Release(handle);
            m_loadedAssets.Remove(assetName);
            unloadAssetCallbacks?.UnloadAssetSuccessCallback?.Invoke(assetName, userData);
        }

        public override void Shutdown()
        {
            foreach (var handle in m_loadedAssets.Values)
            {
                Addressables.Release(handle);
            }
            m_loadedAssets.Clear();

            foreach (var loadingAsset in m_loadingAssets)
            {
                string assetName = loadingAsset.Key;
                var loadingAssetInfoList = loadingAsset.Value;
                foreach (var loadingAssetInfo in loadingAssetInfoList)
                {
                    var loadAssetCallbacks = loadingAssetInfo.LoadAssetCallbacks;
                    var userData = loadingAssetInfo.UserData;
                    loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                        $"Loading {assetName} is interrupted", userData);
                }
                loadingAssetInfoList.Clear();
            }
            m_loadingAssets.Clear();

            foreach (var coroutine in m_loadingCoroutines.Values)
            {
                StopCoroutine(coroutine);
            }
            m_loadingCoroutines.Clear();
        }

        private IEnumerator LoadAssetCO<TObject>(string assetName)
        {
            var handle = Addressables.LoadAssetAsync<TObject>(assetName);
            yield return handle;

            // 加载完成
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                // 加载失败
                foreach (var loadingAssetInfo in m_loadingAssets[assetName])
                {
                    var loadAssetCallbacks = loadingAssetInfo.LoadAssetCallbacks;
                    var userData = loadingAssetInfo.UserData;
                    loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                        handle.OperationException?.Message ?? "Unknown error", userData);
                }
                Addressables.Release(handle);
                yield break;
            }
            // 加载成功
            if (handle.Result is not TObject asset)
            {
                // 无法转换到指定类型
                foreach (var loadingAssetInfo in m_loadingAssets[assetName])
                {
                    var loadAssetCallbacks = loadingAssetInfo.LoadAssetCallbacks;
                    var userData = loadingAssetInfo.UserData;
                    loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                        $"{assetName} is not {typeof(TObject)}", userData);
                }
                Addressables.Release(handle);
                yield break;
            }

            foreach (var loadingAssetInfo in m_loadingAssets[assetName])
            {
                var loadAssetCallbacks = loadingAssetInfo.LoadAssetCallbacks;
                var userData = loadingAssetInfo.UserData;

                loadAssetCallbacks?.LoadAssetSuccessCallback?.Invoke(assetName, asset, userData);
            }
            m_loadingAssets.Remove(assetName);
            m_loadingCoroutines.Remove(assetName);
            m_loadedAssets.Add(assetName, handle);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace KitaFramework
{
    public partial class AddressableAssetLoader : AssetLoaderBase
    {
        private Dictionary<string, AsyncOperationHandle> m_loadedAssets;
        private Dictionary<string, LoadingAssetInfo> m_loadingAssets;

        public AddressableAssetLoader()
        {
            m_loadedAssets = new();
            m_loadingAssets = new();
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
            if (m_loadingAssets.TryGetValue(assetName, out var loadingAssetInfo))
            {
                // 正在加载该资源，添加加载完成的回调方法
                loadingAssetInfo.AddCallback(loadAssetCallbacks, userData);

                return;
            }

            // 还没加载过该资源，加载
            loadingAssetInfo = new();
            loadingAssetInfo.AddCallback(loadAssetCallbacks, userData);
            m_loadingAssets.Add(assetName, loadingAssetInfo);

            loadingAssetInfo.Coroutine = StartCoroutine(LoadAssetCO<TObject>(assetName));
        }

        public override void UnloadAsset(string assetName, UnloadAssetCallbacks unloadAssetCallbacks, object userData)
        {
            if (m_loadedAssets.TryGetValue(assetName, out var handle))
            {
                // 资源已被加载，卸载
                Addressables.Release(handle);
                m_loadedAssets.Remove(assetName);
                unloadAssetCallbacks?.UnloadAssetSuccessCallback?.Invoke(assetName, userData);
                return;
            }

            if (m_loadingAssets.TryGetValue(assetName, out var loadingAssetInfo))
            {
                // 资源正在加载，打断加载
                StopCoroutine(loadingAssetInfo.Coroutine);
                Addressables.Release(loadingAssetInfo.Handle);
                loadingAssetInfo.InvokeFailureCallbacks(assetName, 
                    $"Loading {assetName} is interrupted");
                // 从 loadingAssets 中移除
                loadingAssetInfo.Release();
                m_loadingAssets.Remove(assetName);

                unloadAssetCallbacks?.UnloadAssetSuccessCallback?.Invoke(assetName, userData);
                return;
            }

            // 资源没被加载
            unloadAssetCallbacks?.UnloadAssetFailureCallback?.Invoke(assetName,
                    $"Asset {assetName} is not loaded or already unloaded", userData);
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
                var assetName = loadingAsset.Key;
                var loadingAssetInfo = loadingAsset.Value;
                // 打断加载
                StopCoroutine(loadingAssetInfo.Coroutine);
                Addressables.Release(loadingAssetInfo.Handle);
                loadingAssetInfo.InvokeFailureCallbacks(assetName, 
                    $"Loading {assetName} is interrupted");
                loadingAssetInfo.Release();
            }
            m_loadingAssets.Clear();
        }

        private IEnumerator LoadAssetCO<TObject>(string assetName)
        {
            var handle = Addressables.LoadAssetAsync<TObject>(assetName);
            m_loadingAssets[assetName].Handle = handle;
            yield return handle;

            // 加载完成
            var loadingAssetInfo = m_loadingAssets[assetName];
            m_loadingAssets.Remove(assetName);

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                // 加载失败
                loadingAssetInfo.InvokeFailureCallbacks(assetName, 
                    handle.OperationException?.Message ?? "Unknown error");
                loadingAssetInfo.Release();
                Addressables.Release(handle);
                yield break;
            }
            // 加载成功
            if (handle.Result is not TObject asset)
            {
                // 无法转换到指定类型
                loadingAssetInfo.InvokeFailureCallbacks(assetName,
                    $"{assetName} is not {typeof(TObject)}");
                loadingAssetInfo.Release();
                Addressables.Release(handle);
                yield break;
            }

            loadingAssetInfo.InvokeSuccessCallbacks(assetName, asset);
            loadingAssetInfo.Release();
            m_loadedAssets.Add(assetName, handle);
        }
    }
}
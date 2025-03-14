using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

namespace KitaFramework
{
    public partial class AddressableAssetLoader
    {
        private class LoadingAssetInfo
        {
            public Coroutine Coroutine { get; set; }
            public AsyncOperationHandle Handle { get; set; }

            private List<LoadAssetCallbacks> m_loadAssetCallbacksList = new();
            private List<object> m_userDataList = new();

            public void AddCallback(LoadAssetCallbacks loadAssetCallbacks, object userData)
            {
                m_loadAssetCallbacksList.Add(loadAssetCallbacks);
                m_userDataList.Add(userData);
            }

            public void InvokeSuccessCallbacks(string assetName, object asset)
            {
                for (int i = 0; i < m_loadAssetCallbacksList.Count; i++)
                {
                    var loadAssetCallbacks = m_loadAssetCallbacksList[i];
                    var userData = m_userDataList[i];
                    loadAssetCallbacks?.LoadAssetSuccessCallback?.Invoke(assetName,
                        asset, userData);
                }
            }

            public void InvokeFailureCallbacks(string assetName, string errorMsg)
            {
                for (int i = 0; i < m_loadAssetCallbacksList.Count; i++)
                {
                    var loadAssetCallbacks = m_loadAssetCallbacksList[i];
                    var userData = m_userDataList[i];

                    loadAssetCallbacks?.LoadAssetFailureCallback?.Invoke(assetName,
                            errorMsg, userData);
                }
            }
            
            public void Release()
            {
                Coroutine = null;

                m_loadAssetCallbacksList.Clear();
                m_userDataList.Clear();
            }
        }
    }
}
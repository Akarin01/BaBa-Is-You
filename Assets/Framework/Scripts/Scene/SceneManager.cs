using System.Collections.Generic;
using UnityEngine;
using System;

namespace KitaFramework
{
    public class SceneManager : FrameworkManager
    {
        public event EventHandler<UnloadSceneSuccessEventArgs> OnUnloadSceneSuccess;

        private List<string> m_loadedSceneAssetNames;
        private List<string> m_loadingSceneAssetNames;
        private List<string> m_unloadingSceneAssetNames;
        private LoadSceneCallbacks m_loadSceneCallbacks;
        private LoadSceneCallbacks m_unloadSceneCallbacks;
        private ISceneLoader m_sceneLoader;

        protected override void Awake()
        {
            base.Awake();

            m_loadedSceneAssetNames = new();
            m_loadingSceneAssetNames = new();
            m_unloadingSceneAssetNames = new();
            m_loadSceneCallbacks = new(LoadSceneSuccessCallback, LoadSceneFailureCallback);
            m_unloadSceneCallbacks = new(UnloadSceneSuccessCallback, UnloadSceneFailureCallback);
        }

        private void Start()
        {
            m_sceneLoader = FrameworkEntry.GetManager<ResourceManager>().SceneLoader;
        }

        public void LoadScene(string sceneAssetName, object userData = null)
        {
            if (IsSceneLoading(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} is loading");
            }
            if (IsSceneUnloading(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} is unloading");
            }
            if (IsSceneLoaded(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} has been loaded");
            }

            m_loadingSceneAssetNames.Add(sceneAssetName);
            m_sceneLoader.LoadScene(sceneAssetName, m_loadSceneCallbacks, userData);
        }

        public void UnloadScene(string sceneAssetName, object userData = null)
        {
            if (IsSceneLoading(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} is loading");
            }
            if (IsSceneUnloading(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} is unloading");
            }
            if (!IsSceneLoaded(sceneAssetName))
            {
                throw new ArgumentException($"Scene {sceneAssetName} has't been loaded");
            }

            m_loadedSceneAssetNames.Remove(sceneAssetName);
            m_unloadingSceneAssetNames.Add(sceneAssetName);
            m_sceneLoader.UnloadScene(sceneAssetName, m_unloadSceneCallbacks, userData);
        }


        public bool IsSceneLoaded(string sceneAssetName)
        {
            return m_loadedSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetLoadedSceneAssetNames()
        {
            return m_loadedSceneAssetNames.ToArray();
        }

        public bool IsSceneLoading(string sceneAssetName)
        {
            return m_loadingSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetLoadingSceneAssetNames()
        {
            return m_loadingSceneAssetNames.ToArray();
        }

        public bool IsSceneUnloading(string sceneAssetName)
        {
            return m_unloadingSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetUnloadingSceneAssetNames()
        {
            return m_unloadingSceneAssetNames.ToArray();
        }

        public override void Shutdown()
        {
            m_loadedSceneAssetNames.Clear();
            m_loadingSceneAssetNames.Clear();
            m_unloadingSceneAssetNames.Clear();

            m_sceneLoader = null;
            OnUnloadSceneSuccess = null;
        }

        private void LoadSceneSuccessCallback(string sceneAssetName, object userData)
        {
            m_loadingSceneAssetNames.Remove(sceneAssetName);
            m_loadedSceneAssetNames.Add(sceneAssetName);
        }

        private void LoadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData)
        {
            m_loadingSceneAssetNames.Remove(sceneAssetName);
            Debug.LogError($"Load scene {sceneAssetName} failed");
        }

        private void UnloadSceneSuccessCallback(string sceneAssetName, object userData)
        {
            m_unloadingSceneAssetNames.Remove(sceneAssetName);

            OnUnloadSceneSuccess?.Invoke(this, new UnloadSceneSuccessEventArgs(sceneAssetName, userData));
        }

        private void UnloadSceneFailureCallback(string sceneAssetName, string errorMsg, object userData)
        {
            m_unloadingSceneAssetNames.Remove(sceneAssetName);
            Debug.LogError($"Unload scene {sceneAssetName} failed");
        }
    }
}
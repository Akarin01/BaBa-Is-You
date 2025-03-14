using UnityEngine;
using System.Collections.Generic;
using System;

namespace KitaFramework
{
    public class UIManager : FrameworkManager
    {
        [SerializeField] private Transform m_uiFormInstancesRoot;

        private Dictionary<string, UIGroup> m_uiGroups;
        private IObjectPool<UIFormObject> m_objectPool;
        private AssetLoaderBase m_assetLoader;
        private HashSet<string> m_loadedAssetNames;

        protected override void Awake()
        {
            base.Awake();

            m_uiGroups = new();
            m_loadedAssetNames = new();
        }

        private void Start()
        {
            m_objectPool = FrameworkEntry.GetManager<ObjectPoolManager>()?.CreateObjectPool<UIFormObject>();
            m_assetLoader = FrameworkEntry.GetManager<ResourceManager>().AssetLoader;
        }

        public void OpenUI(string uiAssetName, string groupName, object data)
        {
            // 创建 UIForm 实例
            UIFormObject uiFormObject = m_objectPool.Spawn(uiAssetName);
            if (uiFormObject == null)
            {
                // 加载新的对象
                m_loadedAssetNames.Add(uiAssetName);
                m_assetLoader.LoadAsset<GameObject>(uiAssetName, 
                    new LoadAssetCallbacks(LoadUIFormSuccessCallback, LoadUIFormFailureCallback), 
                    new LoadUIFormData(groupName, data));
            }
            else
            {
                // 取出对象
                UIForm uiForm = (UIForm)uiFormObject.Target;
                uiForm.OnInit(groupName);

                // 添加到对应的 UIGroup
                AddUIForm(uiForm.GroupName, uiForm, data);
            }
        }

        public void CloseUI(UIForm uiForm, object data = null)
        {
            // 从对应的 UIGroup 移除 UIForm
            RemoveUIForm(uiForm.GroupName, uiForm, data);

            // 销毁 UIForm 实例
            m_objectPool.Unspawn(uiForm);
        }

        public override void Shutdown()
        {
            foreach (var uiGroup in m_uiGroups)
            {
                uiGroup.Value.Release();
            }
            m_uiGroups.Clear();

            FrameworkEntry.GetManager<ObjectPoolManager>()?.DestroyObjectPool<UIFormObject>();
            m_objectPool = null;

            foreach (var loadedAssetName in m_loadedAssetNames)
            {
                m_assetLoader.UnloadAsset(loadedAssetName, null, null);
            }
            m_loadedAssetNames.Clear();
            m_assetLoader = null;
        }

        private void AddUIForm(string groupName, UIForm uiForm, object data)
        {
            if (!m_uiGroups.ContainsKey(groupName))
            {
                m_uiGroups.Add(groupName, new UIGroup());
            }
            m_uiGroups[groupName].AddUIForm(uiForm, data);
        }

        private void RemoveUIForm(string groupName, UIForm uiForm, object data)
        {
            if (!m_uiGroups.ContainsKey(groupName))
            {
                Debug.LogError("不存在该 Group: " + groupName);
                return;
            }

            m_uiGroups[groupName].RemoveUIForm(uiForm, data);
        }

        private void LoadUIFormSuccessCallback(string assetName, object asset, object userData)
        {
            if (asset is not GameObject go)
            {
                m_assetLoader.UnloadAsset(assetName, null, null);
                Debug.LogError($"{nameof(asset)} is not {typeof(GameObject)}");
                return;
            }
            if (userData is not LoadUIFormData data)
            {
                Debug.LogError($"{nameof(userData)} is not {typeof(LoadUIFormData)}");
                return;
            }

            GameObject uiFormInstance = Instantiate(go, m_uiFormInstancesRoot);
            UIForm uiForm = uiFormInstance.GetComponent<UIForm>();
            uiForm.OnInit(data.GroupName);
            UIFormObject uiFormObject = new UIFormObject(uiForm, assetName);
            m_objectPool.Register(uiFormObject, true);

            AddUIForm(uiForm.GroupName, uiForm, data.Data);
        }

        private void LoadUIFormFailureCallback(string assetName, string errorMsg, object userData)
        {
            Debug.Log(errorMsg);
        }
    }
}
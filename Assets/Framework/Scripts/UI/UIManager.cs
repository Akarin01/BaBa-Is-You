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
        private ResourceManager m_resourceManager;
        private HashSet<string> m_loadedUIFormAssetNames;

        protected override void Awake()
        {
            base.Awake();

            m_uiGroups = new();
            m_loadedUIFormAssetNames = new();
        }

        private void Start()
        {
            m_objectPool = FrameworkEntry.GetManager<ObjectPoolManager>()?.CreateObjectPool<UIFormObject>();
            m_resourceManager = FrameworkEntry.GetManager<ResourceManager>();
        }

        public void OpenUI(string uiAssetName, string groupName, object data)
        {
            // ���� UIForm ʵ��
            UIFormObject uiFormObject = m_objectPool.Spawn(uiAssetName);
            if (uiFormObject == null)
            {
                // �����µĶ���
                m_resourceManager.LoadAsset<UIForm>(uiAssetName, 
                    new LoadAssetCallbacks(LoadUIFormSuccessCallback, LoadUIFormFailureCallback), 
                    new LoadUIFormData(groupName, data));
                m_loadedUIFormAssetNames.Add(uiAssetName);
            }
            else
            {
                // ȡ������
                UIForm uiForm = (UIForm)uiFormObject.Target;
                uiForm.OnInit(groupName);

                // ��ӵ���Ӧ�� UIGroup
                AddUIForm(uiForm.GroupName, uiForm, data);
            }
        }

        public void CloseUI(UIForm uiForm, object data = null)
        {
            // �Ӷ�Ӧ�� UIGroup �Ƴ� UIForm
            RemoveUIForm(uiForm.GroupName, uiForm, data);

            // ���� UIForm ʵ��
            m_objectPool.Unspawn(uiForm);
        }

        public override void Shutdown()
        {
            foreach (var uiGroup in m_uiGroups)
            {
                uiGroup.Value.Release();
            }
            m_uiGroups.Clear();

            foreach (var assetNames in m_loadedUIFormAssetNames)
            {
                m_resourceManager.UnloadAsset(assetNames, null, null);
            }

            FrameworkEntry.GetManager<ObjectPoolManager>()?.DestroyObjectPool<UIFormObject>();
            m_objectPool = null;
            m_resourceManager = null;
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
                Debug.LogError("�����ڸ� Group: " + groupName);
                return;
            }

            m_uiGroups[groupName].RemoveUIForm(uiForm, data);
        }

        private void LoadUIFormSuccessCallback(string assetName, object asset, object userData)
        {
            if (asset is not UIForm uiForm)
            {
                m_resourceManager.UnloadAsset(assetName, null, null);
                Debug.LogError($"{nameof(asset)} is not {typeof(UIForm)}");
                return;
            }
            if (userData is not LoadUIFormData data)
            {
                Debug.LogError($"{nameof(userData)} is not {typeof(LoadUIFormData)}");
                return;
            }

            uiForm = Instantiate(uiForm, m_uiFormInstancesRoot);
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
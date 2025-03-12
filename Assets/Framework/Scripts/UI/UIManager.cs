using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace KitaFramework
{
    public class UIManager : FrameworkManager
    {
        [SerializeField] private Transform m_uiFormInstancesRoot;

        private Dictionary<string, UIGroup> m_uiGroups;
        private IObjectPool<UIFormObject> m_objectPool;
        private ResourceManager m_resourceManager;
        private HashSet<string> m_loadingUIForms;

        protected override void Awake()
        {
            base.Awake();

            m_uiGroups = new();
            m_loadingUIForms = new();
        }

        private void Start()
        {
            m_objectPool = FrameworkEntry.GetManager<ObjectPoolManager>()?.CreateObjectPool<UIFormObject>();
            m_resourceManager = FrameworkEntry.GetManager<ResourceManager>();
        }

        public void OpenUI(string uiPath, string groupName, object data)
        {
            // 创建 UIForm 实例
            string name = uiPath;
            UIFormObject uiFormObject = m_objectPool.Spawn(name);
            if (uiFormObject == null && !m_loadingUIForms.Contains(uiPath))
            {
                // 加载新的对象
                m_loadingUIForms.Add(uiPath);
                m_resourceManager.LoadAsset<UIForm>(name, 
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
            FrameworkEntry.GetManager<ObjectPoolManager>()?.DestroyObjectPool<UIFormObject>();

            m_uiGroups.Clear();
            m_objectPool = null;
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
            m_loadingUIForms.Remove(assetName);

            UIForm uiForm = asset as UIForm;
            LoadUIFormData data = userData as LoadUIFormData;

            uiForm = Instantiate(uiForm, m_uiFormInstancesRoot);
            uiForm.OnInit(data.GroupName);
            UIFormObject uiFormObject = new UIFormObject(uiForm, assetName);
            m_objectPool.Register(uiFormObject, true);

            AddUIForm(uiForm.GroupName, uiForm, data.Data);
        }

        private void LoadUIFormFailureCallback(string assetName, object asset, object userData)
        {
            m_loadingUIForms.Remove(assetName);

            Debug.Log($"UIForm {assetName} fail to load");
        }
    }
}
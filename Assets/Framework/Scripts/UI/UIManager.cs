using UnityEngine;
using System.Collections.Generic;
using BabaIsYou;

namespace KitaFramework
{
    public class UIManager : FrameworkManager
    {
        [SerializeField] private Transform m_uiFormInstancesRoot;

        private Dictionary<string, UIGroup> m_uiGroups;
        private IObjectPool<UIFormObject> m_objectPool;

        protected override void Awake()
        {
            base.Awake();

            m_uiGroups = new();
        }

        private void Start()
        {
            m_objectPool = FrameworkEntry.GetManager<ObjectPoolManager>()?.CreateObjectPool<UIFormObject>();
        }

        public void OpenUI(UIFormID id, object data = null)
        {
            IDataTable<DRUIForm> dtUIForm = FrameworkEntry.GetManager<DataTableManager>()?.GetDataTable<DRUIForm>(null);
            DRUIForm drUIForm = dtUIForm.GetDataRow((int)id);

            // ���� UIForm ʵ��
            string name = drUIForm.UIName;
            UIFormObject uiFormObject = m_objectPool.Spawn(name);
            UIForm uiForm = null;
            if (uiFormObject == null)
            {
                // ע���µĶ���
                uiForm = Resources.Load<UIForm>(drUIForm.Path);
                uiForm = Instantiate(uiForm, m_uiFormInstancesRoot);
                uiForm.OnInit(drUIForm.GroupName);
                uiFormObject = new UIFormObject();
                uiFormObject.Init(uiForm, name);
                m_objectPool.Register(uiFormObject, true);
            }
            else
            {
                // ȡ������
                uiForm = (UIForm)uiFormObject.Target;
            }

            // ��ӵ���Ӧ�� UIGroup
            AddUIForm(uiForm.GroupName, uiForm, data);
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
                Debug.LogError("�����ڸ� Group: " + groupName);
                return;
            }

            m_uiGroups[groupName].RemoveUIForm(uiForm, data);
        }
    }
}
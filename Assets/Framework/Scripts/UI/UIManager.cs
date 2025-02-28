using UnityEngine;
using System.Collections.Generic;

namespace KitaFramework
{
    public class UIManager : FrameworkManager, IUIManager
    {
        [SerializeField] private Transform m_uiFormInstancesRoot;

        private Dictionary<string, UIGroup> m_uiGroups;
        private IObjectPool<UIFormObject> m_objectPool;

        protected override void Awake()
        {
            base.Awake();

            GameEntry.RegisterManager(this);
        }

        private void Start()
        {
            m_objectPool = GameEntry.ObjectPoolManager.CreateObjectPool<UIFormObject>();
        }

        public void OpenUI<T>() where T : UIForm
        {
            // ���� UIForm ʵ��
            string name = typeof(T).Name;
            UIFormObject uiFormObject = m_objectPool.Spawn(name);
            UIForm uiForm = null;
            if (uiFormObject == null)
            {
                // ע���µĶ���
                uiForm = Resources.Load<UIForm>("UIForms/" + name);
                uiForm = Instantiate(uiForm, m_uiFormInstancesRoot);
                uiForm.OnInit();
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
            AddUIForm(uiForm.GroupName, uiForm);
        }

        public void CloseUI(UIForm uiForm)
        {
            // �Ӷ�Ӧ�� UIGroup �Ƴ� UIForm
            RemoveUIForm(uiForm.GroupName, uiForm);

            // ���� UIForm ʵ��
            m_objectPool.Unspawn(uiForm);
        }

        public override void Shutdown()
        {
            base.Shutdown();

            foreach (var uiGroup in m_uiGroups)
            {
                uiGroup.Value.Release();
            }

            m_uiGroups.Clear();
        }

        protected override void Init()
        {
            m_uiGroups = new();
        }

        private void AddUIForm(string groupName, UIForm uiForm)
        {
            if (!m_uiGroups.ContainsKey(groupName))
            {
                m_uiGroups.Add(groupName, new UIGroup());
            }
            m_uiGroups[groupName].AddUIForm(uiForm);
        }

        private void RemoveUIForm(string groupName, UIForm uiForm)
        {
            if (!m_uiGroups.ContainsKey(groupName))
            {
                Debug.LogError("�����ڸ� Group: " + groupName);
                return;
            }

            m_uiGroups[groupName].RemoveUIForm(uiForm);
        }
    }
}
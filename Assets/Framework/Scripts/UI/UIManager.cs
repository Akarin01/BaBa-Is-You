using UnityEngine;
using System.Collections.Generic;

namespace KitaFramework
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform m_uiFormInstancesRoot;

        private const string DEFAULT_GROUP = "Default";

        private Dictionary<string, UIGroup> m_uiGroups;
        private IObjectPool<UIFormObject> m_objectPool;

        private void Awake()
        {
            Init();

            GameEntry.RegisterUIManager(this);
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
            AddUIForm(DEFAULT_GROUP, uiForm);
        }

        public void CloseUI(UIForm uiform)
        {
            // �Ӷ�Ӧ�� UIGroup �Ƴ� UIForm
            RemoveUIForm(DEFAULT_GROUP, uiform);

            // ���� UIForm ʵ��
            m_objectPool.Unspawn(uiform);
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

        private void Init()
        {
            m_uiGroups = new();
        }
    }
}
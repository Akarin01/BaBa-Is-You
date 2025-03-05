using System.Collections.Generic;

namespace KitaFramework
{
    public class UIGroup
    {
        private LinkedList<UIForm> m_uiForms = new();

        public void AddUIForm(UIForm uiForm, object data)
        {
            if (m_uiForms.Count != 0)
            {
                // 暂停当前最上层的 UIForm
                UIForm topUIForm = m_uiForms.First.Value;
                topUIForm.OnPause();
            }
            m_uiForms.AddFirst(uiForm);
            uiForm.OnOpen(data);
        }

        public void RemoveUIForm(UIForm uiForm, object data)
        {
            var node = m_uiForms.First;
            while (node != null)
            {
                UIForm form = node.Value;
                if (form == uiForm)
                {
                    // 找到对应的 UIForm 实例
                    form.OnClose(data);

                    if (node.Next != null)
                    {
                        // 恢复最上层的 UIForm
                        form = node.Next.Value;
                        form.OnResume();
                    }

                    m_uiForms.Remove(node);

                    return;
                }
                node = node.Next;
            }
        }

        public void Release()
        {
            foreach (var uiForm in m_uiForms)
            {
                uiForm.OnRelease();
            }

            m_uiForms.Clear();
        }
    }
}
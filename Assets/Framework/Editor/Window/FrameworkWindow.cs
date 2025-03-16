using UnityEditor;

namespace KitaFramework
{
    public abstract class FrameworkWindow : EditorWindow
    {
        private bool m_isCompiling;

        protected virtual void OnGUI()
        {
            if (!m_isCompiling && EditorApplication.isCompiling)
            {
                // 编译开始
                OnCompileStart();
                m_isCompiling = true;
            }
            else if (m_isCompiling && !EditorApplication.isCompiling)
            {
                // 编译完成
                OnCompileComplete();
                m_isCompiling = false;
            }
        }

        protected virtual void OnCompileStart()
        {

        }

        protected virtual void OnCompileComplete()
        {

        }
    }
}
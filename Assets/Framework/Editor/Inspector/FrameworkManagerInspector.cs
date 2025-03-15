using UnityEditor;

namespace KitaFramework
{
    public abstract class FrameworkManagerInspector : Editor
    {
        private bool m_isCompiling;

        public override void OnInspectorGUI()
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
                m_isCompiling= false;
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
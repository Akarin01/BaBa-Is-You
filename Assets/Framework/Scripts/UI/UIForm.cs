using BabaIsYou;
using System.Collections;
using UnityEngine;

namespace KitaFramework
{

    /// <summary>
    /// UI 窗体基类，负责管理 UI 生命周期逻辑
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIForm : MonoBehaviour
    {
        [field: SerializeField] public string GroupName { get; set; } = Constant.DEFAULT_GROUP;

        private const float FADE_TIME = 0.3f;

        private CanvasGroup m_canvasGroup;
        private UIManager m_manager;

        public virtual void OnInit()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            m_manager = FrameworkEntry.GetManager<UIManager>();
        }

        public virtual void OnOpen()
        {
            StopAllCoroutines();
            m_canvasGroup.alpha = 0;
            StartCoroutine(FadeToAlpha(1f, FADE_TIME));
        }

        public virtual void OnClose()
        {

        }

        public virtual void OnPause()
        {

        }

        public virtual void OnResume()
        {

        }

        public virtual void OnRelease()
        {

        }

        private IEnumerator FadeToAlpha(float alpha, float duration)
        {
            float time = 0f;
            float originalAlpha = m_canvasGroup.alpha;
            while (time < duration)
            {
                time += Time.deltaTime;
                m_canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
                yield return new WaitForEndOfFrame();
            }
            m_canvasGroup.alpha = alpha;
        }

        private IEnumerator CloseCo()
        {
            yield return FadeToAlpha(0f, FADE_TIME);
            m_manager.CloseUI(this);
        }

        protected void Close(bool immediate)
        {
            if (immediate)
            {
                m_manager.CloseUI(this);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(CloseCo());
            }
        }
    }
}
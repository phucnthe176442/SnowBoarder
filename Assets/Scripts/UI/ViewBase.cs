using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace snow_boarder.UI
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public class ViewBase : MonoBehaviour
    {
        [SerializeField] Transform animatedGroup;
        [SerializeField] float duration = 0.2f;
        [SerializeField] Ease ease = Ease.OutBack;

        protected Canvas canvas;
        protected CanvasGroup canvasGroup;
        public ViewControllerBase Controller { get; private set; }

        internal void Initialize(ViewControllerBase c)
        {
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            Controller = c;

            canvasGroup.alpha = 0;
            if (!animatedGroup)
            {
                animatedGroup = new GameObject("AnimatedGroup").AddComponent<RectTransform>();
                animatedGroup.SetParent(transform);
                animatedGroup.ResetLocal();
            }

            animatedGroup.localScale = Vector3.zero;

            Setup();
        }

        internal void SetOrder(int order) { canvas.sortingOrder = order; }

        public virtual void Close() { Controller.Dismiss(this, true); }

        internal void Show(bool animated, object data = null)
        {
            BeforeShow(data);
            ShowAnim(animated, AfterShown);
        }

        internal void Resume(bool animated)
        {
            BeforeResume();
            ShowAnim(animated, AfterResumed);
        }

        internal void Dismiss(bool animated)
        {
            BeforeDismiss();
            HideAnim(animated,
                () =>
                {
                    gameObject.SetActive(false);
                    AfterDismissed();
                });
        }

        internal void Pause(bool animated)
        {
            BeforePause();
            HideAnim(animated, AfterPaused);
        }

        private void ShowAnim(bool animated, System.Action onCompleted)
        {
            DOTween.Kill(this, true);

            canvasGroup.interactable = false;
            gameObject.SetActive(true);

            if (animated)
            {
                DOTween.To(() => canvasGroup.alpha, x => { canvasGroup.alpha = x; }, 1, duration).SetUpdate(true).SetTarget(this).Play();

                animatedGroup.DOScale(Vector3.one, duration)
                    .SetEase(ease)
                    .SetUpdate(true)
                    .SetTarget(this)
                    .OnComplete(() =>
                    {
                        canvasGroup.alpha = 1;
                        animatedGroup.localScale = Vector3.one;
                        canvasGroup.interactable = true;
                        onCompleted?.Invoke();
                    })
                    .Play();
            }
            else
            {
                canvasGroup.alpha = 1;
                animatedGroup.localScale = Vector3.one;
                canvasGroup.interactable = true;
                onCompleted?.Invoke();
            }
        }

        private void HideAnim(bool animated, System.Action onCompleted)
        {
            DOTween.Kill(this, true);

            canvasGroup.interactable = false;

            if (animated)
            {
                DOTween.To(() => canvasGroup.alpha, x => { canvasGroup.alpha = x; }, 0, duration * 0.5f).SetUpdate(true).SetTarget(this).Play();

                animatedGroup.DOScale(Vector3.zero, duration)
                    .SetTarget(this)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        canvasGroup.alpha = 0;
                        animatedGroup.localScale = Vector3.zero;
                        onCompleted?.Invoke();
                    })
                    .Play();
            }
            else
            {
                canvasGroup.alpha = 0;
                animatedGroup.localScale = Vector3.zero;
                onCompleted?.Invoke();
            }
        }

        #region Override methods

        protected virtual void Setup() { }

        protected virtual void BeforeShow(object data = null) { }

        protected virtual void AfterShown() { }

        protected virtual void BeforeDismiss() { }

        protected virtual void AfterDismissed() { }

        protected virtual void BeforePause() { }

        protected virtual void AfterPaused() { }

        protected virtual void BeforeResume() { }

        protected virtual void AfterResumed() { }

        #endregion
    }
}
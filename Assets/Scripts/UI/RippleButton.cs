using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace snow_boarder.UI
{
    public class RippleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] Ease ease = Ease.OutQuint;
        [SerializeField] float scale = 0.9f;
        [SerializeField] public bool isInteractable = true;
        [SerializeField] protected Button.ButtonClickedEvent m_OnClick;
        [SerializeField] private AudioClip sfxClick;
        [SerializeField] private bool playSfx = true;

        public Button.ButtonClickedEvent OnClicked => m_OnClick;

        protected Vector3 originScale = Vector3.one;

        private void Start()
        {
            originScale = transform.localScale;
        }

        private void OnDisable()
        {
            ResetScale();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;

            if (playSfx) AudioManager.Instance.PlaySFX(sfxClick);
            DoScale();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetScale();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;

            m_OnClick.Invoke();
        }

        void DoScale()
        {
            DOTween.Kill(this);
            transform.DOScale(originScale * scale, 0.15f).SetEase(ease).SetUpdate(true).SetTarget(this).Play();
        }

        void ResetScale()
        {
            DOTween.Kill(this);
            transform.localScale = originScale;
        }
    }
}
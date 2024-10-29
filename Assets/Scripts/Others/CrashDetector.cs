using UnityEngine;

namespace snow_boarder
{
    public class CrashDetector : MonoBehaviour
    {
        [SerializeField] private Trigger trigger;
        [SerializeField] private ParticleSystem crashEffect;
        [SerializeField] private AudioClip crashSFX;

        private bool _hasCrashed = false;
        private PlayerController _controller;

        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            trigger.OnTriggerEnterEvent += OnTriggerEnterEvnt;
        }

        private void OnDisable()
        {
            trigger.OnTriggerEnterEvent -= OnTriggerEnterEvnt;
        }

        private void OnTriggerEnterEvnt(GameObject @object)
        {
            if (_hasCrashed) return;
            Debug.Log("Crashed!!!");
            _hasCrashed = true;
            _controller.DisableControls();
            crashEffect.Play();
            AudioManager.Instance.PlaySFX(crashSFX);
            GameManager.Instance.EndGame();
        }
    }
}

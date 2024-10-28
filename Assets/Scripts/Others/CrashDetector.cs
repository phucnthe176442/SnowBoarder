using UnityEngine;
using UnityEngine.SceneManagement;

namespace snow_boarder
{
    public class CrashDetector : MonoBehaviour
    {

        [SerializeField] private float delayTime = 1f;
        [SerializeField] ParticleSystem crashEffect;

        private bool _hasCrashed = false;

        [SerializeField] AudioClip crashSFX;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Ground" && !_hasCrashed)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                var controller = player.GetComponent<PlayerController>();

                var oldScore = PlayerPrefs.GetFloat("highest");
                var newScore = controller.RotationTime;

                if (newScore > oldScore) PlayerPrefs.SetFloat("highest", newScore);
                _hasCrashed = true;
                controller.DisableControls();
                Debug.Log("Ouch!");
                crashEffect.Play();
                AudioManager.Instance.PlaySFX(crashSFX);
                Invoke(nameof(ReloadScene), delayTime);
            }
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}

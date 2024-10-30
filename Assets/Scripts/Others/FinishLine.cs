using UnityEngine;

namespace snow_boarder
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem finishEffect;
        [SerializeField] private AudioClip finishSfx;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                finishEffect.Play();
                GameManager.Instance.CameraTarget.GetComponent<PlayerController>().DisableControls();
                AudioManager.Instance.PlaySFX(finishSfx);
                GameManager.Instance.OnWin();
            }
        }
    }
}

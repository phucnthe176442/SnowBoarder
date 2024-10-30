using UnityEngine;

namespace snow_boarder
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private float value = 1f;
        [SerializeField] private AudioClip sfx;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.Instance.Score += value;
            GameManager.Instance.Star += value;
            AudioManager.Instance.PlaySFX(sfx);
            Destroy(gameObject);
        }
    }
}
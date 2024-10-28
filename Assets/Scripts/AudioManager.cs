using snow_boarder.Core;
using UnityEngine;

namespace snow_boarder
{
    public class AudioManager : SingletonDontDestroy<AudioManager>
    {
        [Header("--------------AudioSource--------------")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource SFXSource;

        [Header("--------------AudioClip--------------"), Space]
        public AudioClip background;

        private float _lastTimePlayOneShot;

        public void Start()
        {
            PlayMusic(background);
        }

        public void PlayMusic(AudioClip music)
        {
            musicSource.clip = music;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (Time.time - _lastTimePlayOneShot < 0.1f) return;
            _lastTimePlayOneShot = Time.time;
            SFXSource.PlayOneShot(clip);
        }
    }
}

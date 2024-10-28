using snow_boarder.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace snow_boarder
{
    public class AudioManager : SingletonDontDestroy<AudioManager>
    {
        public AudioMixer mixer;
        [Header("--------------AudioSource--------------")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource SFXSource;

        [Header("--------------AudioClip--------------"), Space]
        public AudioClip background;

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat("musicVolume", 1f);
            set
            {
                PlayerPrefs.SetFloat("musicVolume", value);
                mixer.SetFloat("music", Mathf.Log10(value) * 20f);
            }
        }
        public float SFXVolume
        {
            get => PlayerPrefs.GetFloat("sfxVolume", 1f);
            set
            {
                PlayerPrefs.SetFloat("sfxVolume", value);
                mixer.SetFloat("sfx", Mathf.Log10(value) * 20f);
            }
        }
        private float _lastTimePlayOneShot;

        public void Start()
        {
            PlayMusic(background);
            mixer.SetFloat("music", Mathf.Log10(MusicVolume) * 20f);
            mixer.SetFloat("sfx", Mathf.Log10(SFXVolume) * 20f);
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

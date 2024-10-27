using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("--------------AudioSource--------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--------------AudioClip--------------")]
    public AudioClip background;
    public AudioClip clickButton;
    public AudioClip playerDied;
    public AudioClip playerJump;
    public AudioClip pickItem;

    private float _lastTimePlayOneShot;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (Time.time - _lastTimePlayOneShot < 0.1f) return;
        _lastTimePlayOneShot = Time.time;
        SFXSource.PlayOneShot(clip);
    }
}

using UnityEngine;
using snow_boarder;

public class VolumeSetting : MonoBehaviour
{

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.MusicVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SFXVolume = value;
    }
}


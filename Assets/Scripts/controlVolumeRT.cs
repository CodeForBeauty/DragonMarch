using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlVolumeRT : MonoBehaviour
{
    public AudioSource musicSource;

    void Update()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicSource.volume = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlVolume : MonoBehaviour
{
    public AudioSource[] sfxSources;
    public AudioSource[] musicSources;

    void Start()
    {
        foreach (AudioSource audioSource in sfxSources)
        {
            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
            }
            else
            {
                audioSource.volume = 1;
            }
        }
        foreach (AudioSource audioSource in musicSources)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
            else
            {
                audioSource.volume = 1;
            }
        }
    }
}

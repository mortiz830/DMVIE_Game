using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string SoundPref = "SoundPref";
    private int firstPlayInt;
    private float soundSliderFloat;
    public AudioSource bgm;
    public AudioSource[] sfx;

    void Awake()
    {
        ContinueSettings();
    }

    public void ContinueSettings()
    {
        soundSliderFloat = PlayerPrefs.GetFloat(SoundPref);

        bgm.volume = soundSliderFloat;

        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = soundSliderFloat;
        }
    }

}


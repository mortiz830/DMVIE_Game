using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string SoundPref = "SoundPref";
    private int firstPlayInt;
    public Slider soundSlider;
    private float soundSliderFloat;
    public AudioSource bgm;
    public AudioSource[] sfx;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        
        if(firstPlayInt == 0)
        {
            soundSliderFloat = 1f;
            soundSlider.value = soundSliderFloat;
            PlayerPrefs.SetFloat(SoundPref, soundSliderFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            soundSliderFloat = PlayerPrefs.GetFloat(SoundPref);
            soundSlider.value = soundSliderFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(SoundPref, soundSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        bgm.volume = soundSlider.value;

        for(int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = soundSlider.value;
        }
    }

}

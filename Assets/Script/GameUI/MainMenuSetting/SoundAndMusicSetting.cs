using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundAndMusicSetting : MonoBehaviour
{
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider sfxSlider;
    private void Start()
    {
        audioMixer.SetFloat("musicPara", Mathf.Log10(1)*20);
        audioMixer.SetFloat("sfxPara", Mathf.Log10(1)*20);
    }
    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("musicPara", Mathf.Log10(musicVolume)*20);
    }
    public void SetSFXVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfxPara", Mathf.Log10(sfxVolume)*20);
    }
}

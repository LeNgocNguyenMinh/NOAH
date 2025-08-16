using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundAndMusicSetting : MonoBehaviour
{
    [SerializeField]private AudioMixer audioMixer;
    private AudioSource musicSrc;
    private AudioSource sfxSrc;
    public AudioClip gameMusic;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider sfxSlider;
    private static bool hasPlayedMusic = false;
    private void Start()
    {
        musicSrc = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        sfxSrc = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        audioMixer.SetFloat("musicPara", Mathf.Log10(1)*20);
        audioMixer.SetFloat("sfxPara", Mathf.Log10(1)*20);
        musicSrc.clip = gameMusic;
        musicSrc.Play();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Instance;
    private AudioSource musicSrc;
    private AudioSource sfxSrc;
    [Header("----------Player Audio Clips----------")]
    public AudioClip playerShootSound;
    public AudioClip playerWalkSound;
    public AudioClip playerDashSound;
    public AudioClip playerDeathSound;
    public AudioClip coinSound;
    public AudioClip expSound;
    [Header("----------FD Audio Clips----------")]
    public AudioClip dragonRoar;
    public AudioClip dragonFire;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }   
    }
    private void Start()
    {
        musicSrc = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        sfxSrc = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
    }
    public void PlayerShootSoundPlay()
    {
        PlaySFX(playerShootSound);
    }
    public void PlayerWalkSoundPlay()
    {
        PlaySFX(playerWalkSound);
    }
    public void PlayerDashSoundPlay()
    {
        PlaySFX(playerDashSound);
    }
    public void PlayerDeathSoundPlay()
    {
        PlaySFX(playerDeathSound);
    }
    public void CoinCollectPlay()
    {
        PlaySFX(coinSound);
    }
    public void EXPCollectPlay()
    {
        PlaySFX(expSound);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }
    public void DragonRoarPlay()
    {
        PlaySFX(dragonRoar);
    }
    public void DragonFirePlay()
    {
        PlaySFX(dragonFire);
    }
}

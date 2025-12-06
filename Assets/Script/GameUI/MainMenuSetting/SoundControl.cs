using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Instance;
    [SerializeField]private AudioSource musicSrc;
    [SerializeField]private AudioSource sfxSrc;
    [Header("----------Game Music----------")]
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;
    [Header("----------Player Audio Clips----------")]
    public AudioClip playerShootSound;
    public AudioClip PlayerMeleeSound;
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
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
        if(sceneName == "Level1")
        {
            Debug.Log("Level1MusicPlay");
            InGameMusicPlay();
        }
        else if(sceneName == "MainMenu")
        {
            Debug.Log("mainMenuMusicPlay");
            MainMenuMusicPlay();
        }
    }
    public void PlayerShootSoundPlay()
    {
        PlaySFX(playerShootSound);
    }
    public void PlayerMeleeSoundPlay()
    {
        PlaySFX(PlayerMeleeSound);
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
        sfxSrc.clip = clip;
        sfxSrc.loop = false;
        sfxSrc.Play();
    }
    public void DragonRoarPlay()
    {
        PlaySFX(dragonRoar);
    }
    public void DragonFirePlay()
    {
        PlaySFX(dragonFire);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSrc.clip = clip;
        musicSrc.loop = true;
        musicSrc.Play();
    }
    public void InGameMusicPlay()
    {
        PlayMusic(inGameMusic);
    }
    public void MainMenuMusicPlay()
    {
        PlayMusic(mainMenuMusic);
    }
}

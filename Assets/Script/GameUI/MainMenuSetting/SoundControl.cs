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
    [SerializeField]public AudioClip mainMenuMusic;
    [SerializeField]public AudioClip inGameMusic;
    [SerializeField]public AudioClip bossFightMusic;
    [Header("----------Player Audio Clips----------")]
    [SerializeField]public AudioClip playerShootSound;
    [SerializeField]public AudioClip PlayerMeleeSound;
    [SerializeField]public AudioClip playerWalkSound;
    [SerializeField]public AudioClip playerDashSound;
    [SerializeField]public AudioClip playerDeathSound;
    [SerializeField]public AudioClip coinSound;
    [SerializeField]public AudioClip expSound;
    [Header("----------FD Audio Clips----------")]
    [SerializeField]public AudioClip dragonRoar;
    [SerializeField]public AudioClip dragonFire;
    [Header("----------FruitKing Audio Clips----------")]
    [SerializeField]public AudioClip fkBossThrow;
    [SerializeField]public AudioClip fkBossShoot;
    [Header("----------AncientOne Audio Clips----------")]
    [SerializeField]public AudioClip aoBossHitGround;
    [SerializeField]public AudioClip aoBossShoot;
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
        if(sceneName == "MainMenu")
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
    public void AOBossHitGroundSoundPlay()
    {
        PlaySFX(aoBossHitGround);
    }
    public void AOBossShootSoundPlay()
    {
        PlaySFX(aoBossShoot);
    }
    public void FKBossShootSoundPlay()
    {
        PlaySFX(fkBossShoot);
    }
    public void FKBossThrowSoundPlay()
    {
        PlaySFX(fkBossThrow);
    }
    public void BossFightMusicPlay()
    {
        PlayMusic(bossFightMusic);
    }
}

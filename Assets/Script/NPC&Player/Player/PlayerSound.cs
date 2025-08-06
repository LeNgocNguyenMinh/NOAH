using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound Instance;
    private SoundControl soundControl;
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
        soundControl = FindObjectOfType<SoundControl>().GetComponent<SoundControl>();
    }
    public void PlayShootSound()
    {
        soundControl.PlayerShootSoundPlay();
    }
    public void PlayDashSound()
    {
        soundControl.PlayerDashSoundPlay();
    }
    public void PlayWalkSound()
    {
        soundControl.PlayerWalkSoundPlay();
    }
    public void PlayDeathSound()
    {
        soundControl.PlayerDeathSoundPlay();
    }
}

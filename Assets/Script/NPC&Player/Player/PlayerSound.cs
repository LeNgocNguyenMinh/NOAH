using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound Instance;
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
    public void PlayShootSound()
    {
        SoundControl.Instance.PlayerShootSoundPlay();
    }
    public void PlayDashSound()
    {
        SoundControl.Instance.PlayerDashSoundPlay();
    }
    public void PlayWalkSound()
    {
        SoundControl.Instance.PlayerWalkSoundPlay();
    }
    public void PlayDeathSound()
    {
        SoundControl.Instance.PlayerDeathSoundPlay();
    }
}

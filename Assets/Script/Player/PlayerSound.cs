using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private SoundControl soundControl;
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

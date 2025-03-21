using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDSoundPlay : MonoBehaviour
{
    private SoundControl soundControl;
    public void playDragonRoarSound()
    {
        soundControl = FindObjectOfType<SoundControl>().GetComponent<SoundControl>();
        soundControl.DragonRoarPlay();
    }
    public void playDragonFireSound()
    {
        soundControl = FindObjectOfType<SoundControl>().GetComponent<SoundControl>();
        soundControl.DragonFirePlay();
    }
}

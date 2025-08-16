using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    [SerializeField]private Toggle fullScreenTog, vsyncTog;
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else{
            vsyncTog.isOn = true;
        }
    }

   public void ApplyGraphics()
   {
        Screen.fullScreen = fullScreenTog.isOn;
        if(vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else{
            QualitySettings.vSyncCount = 0;
        }
   }
}

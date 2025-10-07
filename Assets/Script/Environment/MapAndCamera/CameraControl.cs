using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    [SerializeField]private CinemachineVirtualCamera virtualCamera;
    [SerializeField]private float smallSize;
    [SerializeField]private float normalSize;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetUpHomeCam()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = smallSize;
        }
    }
    public void SetUpOutsideCam()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = normalSize;
        }
    }    
}

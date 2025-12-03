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
    public CinemachineConfiner confiner;

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
    public void UpdateCameraBoundry(PolygonCollider2D newMapBoundry)
    {
        confiner.m_BoundingShape2D = newMapBoundry;
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

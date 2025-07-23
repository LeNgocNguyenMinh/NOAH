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
    /* private CinemachineVirtualCamera _cinemachineVirtual;

    private PlayerControl _playerControl;
    private void Start()
    {
        _playerControl = GameObject.FindObjectOfType<PlayerControl>();
        _cinemachineVirtual = gameObject.GetComponent<CinemachineVirtualCamera>();
        _cinemachineVirtual.Follow = _playerControl.transform;
        StartCoroutine(OnSetCamera());
    }

    public IEnumerator OnSetCamera()
    {
        yield return new WaitForSeconds(0.2f);
        _playerControl = GameObject.FindObjectOfType<PlayerControl>();
        _cinemachineVirtual = gameObject.GetComponent<CinemachineVirtualCamera>();
        _cinemachineVirtual.Follow = _playerControl.transform;
    } */
    
}

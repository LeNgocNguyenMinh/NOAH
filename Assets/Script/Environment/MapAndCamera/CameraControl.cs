using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtual;

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
    }
    
}

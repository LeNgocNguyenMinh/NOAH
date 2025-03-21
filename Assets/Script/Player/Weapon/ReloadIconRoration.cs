using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadIconRoration : MonoBehaviour
{
    [SerializeField]private float rotateSpeed;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed*Time.deltaTime);

    }
}

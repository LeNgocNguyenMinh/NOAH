using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMask : MonoBehaviour
{
    //Code xoay điểm rơi
    float rotateSpeed = 350;
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed*Time.deltaTime);
    }
}

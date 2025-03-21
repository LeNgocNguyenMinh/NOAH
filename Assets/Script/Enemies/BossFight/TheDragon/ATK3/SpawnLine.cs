using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour
{
    [SerializeField]private Transform[] transformsList;
    public Transform[] GetTransformsList()
    {
        return transformsList;
    }
    public void DestroyLine()
    {
        Destroy(gameObject);
    }
}

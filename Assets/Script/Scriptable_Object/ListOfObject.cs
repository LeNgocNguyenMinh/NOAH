using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListObjects", menuName = "ListObjects")]
public class ListOfObject : ScriptableObject
{
    public List<Object> objects;
}
[System.Serializable]
public class Object
{
    public string objectID;
    public Vector3[] initPos;
    public GameObject objectPrefab;
}

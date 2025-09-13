using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBehaviourGeneral : MonoBehaviour
{
    [SerializeField]private GameObject currentObject;
    public void DestroyStone()
    {
        Destroy(currentObject);
    }
}

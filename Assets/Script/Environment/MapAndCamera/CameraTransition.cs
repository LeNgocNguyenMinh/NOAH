using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public static CameraTransition Instance;
    [SerializeField]private PolygonCollider2D[] allColliders;
    private MapTransition mapTransition;
    private PolygonCollider2D currentCollider;
    

    void Start()
    {
        currentCollider = allColliders[0];
    }
    void Update()
    {
        foreach (var collider in allColliders)
        {
            if (collider.OverlapPoint(Player.Instance.transform.position) && collider != currentCollider)
            {
                currentCollider = collider;
                mapTransition = FindObjectOfType<MapTransition>().GetComponent<MapTransition>();
                mapTransition.UpdateCameraBoundry(collider);
            }
        }
    }
}

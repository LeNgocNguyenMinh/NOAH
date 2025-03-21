using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField]private PolygonCollider2D[] allColliders;
    private MapTransition mapTransition;
    private Transform playerTransform;
    private PolygonCollider2D currentCollider;

    void Start()
    {
        currentCollider = allColliders[0];
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }
    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        foreach (var collider in allColliders)
        {
            if (collider.OverlapPoint(playerTransform.position) && collider != currentCollider)
            {
                currentCollider = collider;
                mapTransition = FindObjectOfType<MapTransition>().GetComponent<MapTransition>();
                mapTransition.UpdateCameraBoundry(collider);
            }
        }
    }
}

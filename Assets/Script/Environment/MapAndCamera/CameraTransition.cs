using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public static CameraTransition Instance;
    [SerializeField]private PolygonCollider2D[] allColliders;
    private PolygonCollider2D currentCollider;
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
        currentCollider = allColliders[0];
    }
    void Update()
    {
        foreach (var collider in allColliders)
        {
            if (collider.OverlapPoint(Player.Instance.transform.position) && collider != currentCollider)
            {
                currentCollider = collider;
                CameraControl.Instance.UpdateCameraBoundry(collider);
            }
        }
    }
}

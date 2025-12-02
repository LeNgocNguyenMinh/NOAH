using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField]private PolygonCollider2D mapBoundry;
    [SerializeField]private Direction direction;
    [SerializeField]private Transform teleportTransform;
    [SerializeField]private bool teleportPoint;
    enum Direction { Up, Down, Left, Right}
    [SerializeField]private float additivePos;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            CameraControl.Instance.confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition();
        }
    }
    public void UpdateCameraBoundry(PolygonCollider2D newMapBoundry)
    {
        CameraControl.Instance.confiner.m_BoundingShape2D = newMapBoundry;
    }
    private void UpdatePlayerPosition()
    {
        Vector3 newPos = Player.Instance.transform.position;
        if(teleportPoint == true)
        {
            newPos = teleportTransform.position;
        }
        switch (direction)
        {
            case Direction.Up:
                newPos.y += additivePos;
                break;
            case Direction.Down:
                newPos.y -= additivePos;
                break;
            case Direction.Left:
                newPos.x -= additivePos;
                break;
            case Direction.Right:
                newPos.x += additivePos;
                break;
        }
        if(teleportPoint == true)
        { 
            SceneTransition.Instance.SceneIn();
        }
        Player.Instance.transform.position = newPos;
    }
}

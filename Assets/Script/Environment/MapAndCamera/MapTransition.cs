using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField]private PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;
    [SerializeField]private Direction direction;
    enum Direction { Up, Down, Left, Right}
    [SerializeField]private float additivePos;
    private GameObject playerObject;
    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    } 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            playerObject = FindObjectOfType<PlayerControl>().gameObject;
            confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(playerObject);
        }
    }
    public void UpdateCameraBoundry(PolygonCollider2D newMapBoundry)
    {
        confiner.m_BoundingShape2D = newMapBoundry;
    }
    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;
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
        player.transform.position = newPos;
    }
}

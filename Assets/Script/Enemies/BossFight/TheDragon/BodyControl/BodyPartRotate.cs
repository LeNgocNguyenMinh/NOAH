using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float speed;
    private Vector2 direction;
    [SerializeField]private Transform target;
    private void Update()
    {
        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}

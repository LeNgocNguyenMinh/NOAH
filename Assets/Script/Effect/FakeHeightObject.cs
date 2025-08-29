using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FakeHeightObject : MonoBehaviour
{
    public UnityEvent onGroundHitEven;
    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    public float gravity = -10f;
    public Vector2 groundVelocity;
    public float verticalVelocity;
    public float lastInitVerticalVelocity;

    public bool isGrounded;

    void Update()
    {
        UpdatePosition();
        CheckGroundHit();
    }
    public void Initialize(Vector2 groundVelocity, float verticalVelocity)
    {
        isGrounded = false;
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
        lastInitVerticalVelocity = verticalVelocity;
    }
    public void UpdatePosition()
    {
        if(!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
            trnsBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        }
        transform.position += (Vector3)groundVelocity * Time.deltaTime;
    }
    void CheckGroundHit()
    {
        if(trnsBody.position.y <= trnsObject.position.y && !isGrounded)
        {
            trnsBody.position = trnsObject.position;
            isGrounded = true;
            GroundHit();
           
        }
    }
    public void GroundHit()
    {
        onGroundHitEven.Invoke();
    }
    public void Stick()
    {
        groundVelocity = Vector2.zero;
    }
    public void Bounce(float divisionFactor)
    {
        Initialize(groundVelocity, lastInitVerticalVelocity/divisionFactor);
    }
}

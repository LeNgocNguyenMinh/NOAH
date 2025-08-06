using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem walkParticle;

    [Range(0,10)][SerializeField] int occurAfterVelocity;
    [Range(0, 0.2f)][SerializeField] float dustFormationPeriod;
    private Rigidbody2D playerRb;

    float counter;
    private Transform player;
    private void Update()
    {

        counter += Time.deltaTime;
        if(Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity || Mathf.Abs(playerRb.velocity.y) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                walkParticle.Play();
                counter = 0;
            }
        }
    }
    public void Awake()
    {
        player = FindObjectOfType<PlayerControl>().transform;
        playerRb = player.GetComponent<Rigidbody2D>();
    }
}

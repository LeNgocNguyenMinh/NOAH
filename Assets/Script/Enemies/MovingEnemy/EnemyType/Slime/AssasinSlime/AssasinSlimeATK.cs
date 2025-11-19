using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AssasinSlimeATK : MonoBehaviour
{
    private Vector3 bulletSpawnPoint;
    [SerializeField]private Transform mainBody;
    [SerializeField]private GameObject blade;
    public void Attack()
    {
        bulletSpawnPoint = new Vector3(
                                    Player.Instance.transform.position.x,
                                    Player.Instance.transform.position.y - .5f,
                                    0f);
        GameObject tmp = Instantiate(blade, bulletSpawnPoint, Quaternion.identity);
        if(FlipSprite())
        {
            tmp.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private bool FlipSprite()
    {
        if(mainBody.position.x < Player.Instance.transform.position.x)
        {
            return false;
        }
        return true;
    }
}
    
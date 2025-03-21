using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDIntro : MonoBehaviour
{
    [SerializeField]private GameObject fireRoar;
    [SerializeField]private LineRenderer introLine;
    [SerializeField]private float rotationSpeed;
    [SerializeField]private float moveSpeed;
    [SerializeField]private GameObject head;
    private Vector3[] positions;
    private Vector3 direction;
    private int introLineIndex = 0;
    private Animator animator;
    private bool introReady = false;
    private bool introMoveFinish = false;
    [SerializeField]private FDATK1 fdATK1;
    void Update()
    {
        //Check if finish intro line
        if (!introMoveFinish)
        {
            IntroMove();
        }
    }
    private void IntroPrepare()
    {
        fireRoar.SetActive(false);
        positions = new Vector3[introLine.positionCount];
        introLine.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].z = 0;
        }
        head.transform.position = positions[0]; 
        introReady = true;
    }
    private void IntroMove()
    {
        if(!introReady)
        {
            IntroPrepare();
        }
        Vector3 targetPosition = positions[introLineIndex];
        direction = targetPosition - head.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        head.transform.position = Vector3.MoveTowards(head.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(head.transform.position, targetPosition) < 0.01f)
        {
            introLineIndex++; 

            if (introLineIndex >= positions.Length)
            {
                introMoveFinish = true;
                animator = GetComponent<Animator>();
                animator.SetTrigger("headRoar");
            }
        }
    }
    public void StartFireRoar()
    {
        fireRoar.SetActive(true);
        animator.SetTrigger("fireRoar");
    }
    //Finish Intro, start atk1
    public void EndFireRoar()
    {
        fireRoar.SetActive(false);
        //StartATK1
        animator.SetTrigger("headATK1Start");
        fdATK1.startATK1 = true;
        fdATK1.atk1LineFinish = true;
    }
    //Intro finish  
}

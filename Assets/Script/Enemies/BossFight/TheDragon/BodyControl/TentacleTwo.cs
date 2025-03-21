using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleTwo : MonoBehaviour
{
    private int length;
    [SerializeField]private LineRenderer lineRend;
    private Vector3[] segmentPoses;
    private Vector3[] segmentV;
    [SerializeField]private Transform targetDir;
    [SerializeField]private float targetDist;
    [SerializeField]private float smoothSpeed;
    [SerializeField]private Transform[] bodyParts;
    private bool startDeclare = false;
    void Update()
    {
        length = bodyParts.Length + 1;
        if(!startDeclare)
        {
            lineRend.positionCount = length;
            segmentPoses = new Vector3[length];  
            segmentV = new Vector3[length];  
            startDeclare = true;
        }
        segmentPoses[0] = targetDir.position;
        for(int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            bodyParts[i-1].transform.position = segmentPoses[i];
        }
        lineRend.SetPositions(segmentPoses);
    }
}

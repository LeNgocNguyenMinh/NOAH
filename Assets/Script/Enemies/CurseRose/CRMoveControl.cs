using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRMoveControl : MonoBehaviour
{
    [SerializeField]private GameObject projectPrefab;   

    public void ShootProject(Vector3 target)
    {
        ProjectileCurveMove projectileCurveMove = Instantiate(projectPrefab, transform.position, Quaternion.identity).GetComponent<ProjectileCurveMove>();
        projectileCurveMove.SetTarget(target);
    }
}

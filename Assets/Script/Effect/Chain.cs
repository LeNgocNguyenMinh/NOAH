using System.Collections.Generic;
using UnityEngine;

public class Chain
{
    //List of body's points
    public List<Vector2> joints;
    // Size of each link
    private float linkSize;
    // List of angles for each joint
    public List<float> angles;
    // Angle constraint in radians
    private float angleConstraint;
    private float minDistance;
    private float maxDistance;

    public Chain(Vector2 origin, int jointCount, float linkSize) 
        : this(origin, jointCount, linkSize, Mathf.PI * 2f) { }

    public Chain(Vector2 origin, int jointCount, float linkSize, float angleConstraint)
        : this(origin, jointCount, linkSize, angleConstraint, linkSize * 0.8f, linkSize * 1.2f) { }

  
    public Chain(Vector2 origin, int jointCount, float linkSize, float angleConstraint, float minDistance, float maxDistance)
    {
        this.linkSize = linkSize;
        this.angleConstraint = angleConstraint;
        this.minDistance = minDistance;
        this.maxDistance = maxDistance;
        
        joints = new List<Vector2>();
        angles = new List<float>();

        joints.Add(origin);
        angles.Add(0f);

        for (int i = 1; i < jointCount; i++)
        {
            joints.Add(joints[i - 1] + new Vector2(0, this.linkSize));
            angles.Add(0f);
        }
    }

    public void Resolve(Vector2 target)
    {
        const int maxIterations = 3;
        float tolerance = 0.01f;
        
        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            
            for (int i = 0; i < joints.Count - 1; i++)
            {
                Vector2 currentDir = (joints[i + 1] - joints[i]).normalized;
                Vector2 targetDir = (target - joints[i]).normalized;
                
              
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) - Mathf.Atan2(currentDir.y, currentDir.x);
    
                angle = Mathf.Clamp(angle, -angleConstraint, angleConstraint);
                
                for (int j = i + 1; j < joints.Count; j++)
                {
                    Vector2 offset = joints[j] - joints[i];
                    float cos = Mathf.Cos(angle);
                    float sin = Mathf.Sin(angle);
                    float newX = offset.x * cos - offset.y * sin;
                    float newY = offset.x * sin + offset.y * cos;
                    joints[j] = joints[i] + new Vector2(newX, newY);
                }
            }
            
    
            if (Vector2.Distance(joints[0], target) < tolerance)
                break;
        }
        
        // Cập nhật angles sau khi resolve
        UpdateAngles();
    }

    public void FreeMove(Vector2 target)
    {
        Vector2 newHead = Vector2.Lerp(joints[0], target, Time.deltaTime * 8f);
        Vector2 moveDirection = (newHead - joints[0]).normalized;
        
        for (int i = 0; i < joints.Count; i++)
        {
            if (i == 0)
            {
                joints[i] = newHead;
            }
            else
            {
                float delayFactor = i * 0.1f;
                Vector2 targetPos = joints[i - 1] - moveDirection * linkSize;
                joints[i] = Vector2.Lerp(joints[i], targetPos, Time.deltaTime * (10f - delayFactor));
            }
        }
        ConstrainDistances();
        UpdateAngles();
    }
    private void ConstrainDistances()
    {
        for (int i = 0; i < joints.Count - 1; i++)
        {
            Vector2 currentDirection = joints[i + 1] - joints[i];
            float currentDistance = currentDirection.magnitude;
            if (currentDistance < minDistance || currentDistance > maxDistance)
            {
                float targetDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
                Vector2 direction = currentDirection.normalized;
                joints[i + 1] = joints[i] + direction * targetDistance;
            }
        }
    }
    private void UpdateAngles()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            if (i < joints.Count - 1)
            {
                Vector2 dir = (joints[i + 1] - joints[i]).normalized;
                angles[i] = Mathf.Atan2(dir.y, dir.x);
            }
            else
            {
                // Góc cuối cùng giữ nguyên hướng với khớp trước
                angles[i] = angles[i - 1];
            }
        }
    }
    public void Display()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < joints.Count - 1; i++)
        {
            Gizmos.DrawLine(joints[i], joints[i + 1]);
        }

        Gizmos.color = new Color(42f / 255f, 44f / 255f, 53f / 255f);
        foreach (var joint in joints)
        {
            Gizmos.DrawSphere(joint, 0.1f);
        }
    }
}

public static class Vector2Extensions
{
    public static float ToAngle(this Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }
}
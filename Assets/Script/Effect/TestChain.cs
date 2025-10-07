using UnityEngine;
using System.Collections.Generic;

public class TestChain : MonoBehaviour
{
    private Chain spine;
    private Camera mainCamera;
    private Vector2 smoothTarget;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera != null)
        {
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 5f;
            mainCamera.transform.position = new Vector3(0, 0, -10);
        }
        
        // Tạo chain với ràng buộc khoảng cách rõ ràng
        // linkSize = 0.3f, minDistance = 0.24f, maxDistance = 0.36f
        spine = new Chain(transform.position, 10, 0.3f, Mathf.PI / 6, 0.24f, 0.36f);
        smoothTarget = spine.joints[0];
    }

    void Update()
    {
        Resolve();
    }

    void Resolve()
    {
        Vector2 headPos = spine.joints[0];
        
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                       -mainCamera.transform.position.z));
        Vector2 mousePos = new Vector2(mouseWorld.x, mouseWorld.y);
        
        // Làm mượt target position
        smoothTarget = Vector2.Lerp(smoothTarget, mousePos, Time.deltaTime * 8f);
        
        // Sử dụng FreeMove với ràng buộc khoảng cách
        spine.FreeMove(smoothTarget);
    }

    void OnDrawGizmos()
    {
        if (spine == null) return;
        
        spine.Display();
        Display();
        
        // Vẽ target
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(smoothTarget, 0.1f);
    }

    void Display()
    {
        Gizmos.color = new Color(0.8f, 0.2f, 0.2f);
        
        List<Vector2> bodyPoints = new List<Vector2>();

        // Nửa bên phải
        for (int i = 0; i < spine.joints.Count; i++)
        {
            bodyPoints.Add(new Vector2(GetPosX(i, Mathf.PI / 2, 0), GetPosY(i, Mathf.PI / 2, 0)));
        }

        int lastIndex = spine.joints.Count - 1;
        bodyPoints.Add(new Vector2(GetPosX(lastIndex, Mathf.PI, 0), GetPosY(lastIndex, Mathf.PI, 0)));

        // Nửa bên trái
        for (int i = spine.joints.Count - 1; i >= 0; i--)
        {
            bodyPoints.Add(new Vector2(GetPosX(i, -Mathf.PI / 2, 0), GetPosY(i, -Mathf.PI / 2, 0)));
        }

        // Đỉnh đầu
        bodyPoints.Add(new Vector2(GetPosX(0, -Mathf.PI / 6, 0), GetPosY(0, -Mathf.PI / 6, 0)));
        bodyPoints.Add(new Vector2(GetPosX(0, 0, 0), GetPosY(0, 0, 0)));
        bodyPoints.Add(new Vector2(GetPosX(0, Mathf.PI / 6, 0), GetPosY(0, Mathf.PI / 6, 0)));

        // Điểm phụ
        bodyPoints.Add(new Vector2(GetPosX(0, Mathf.PI / 2, 0), GetPosY(0, Mathf.PI / 2, 0)));
        bodyPoints.Add(new Vector2(GetPosX(1, Mathf.PI / 2, 0), GetPosY(1, Mathf.PI / 2, 0)));

        // Vẽ đường viền
        for (int i = 0; i < bodyPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(bodyPoints[i], bodyPoints[i + 1]);
        }

        // Vẽ mắt
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(new Vector2(GetPosX(0, Mathf.PI / 2, -0.1f), GetPosY(0, Mathf.PI / 2, -0.1f)), 0.05f);
        Gizmos.DrawSphere(new Vector2(GetPosX(0, -Mathf.PI / 2, -0.1f), GetPosY(0, -Mathf.PI / 2, -0.1f)), 0.05f);
    }

    float BodyWidth(int i)
    {
        switch (i)
        {
            case 0: return 0.2f;
            case 1: return 0.22f;
            default: return Mathf.Max(0.08f, 0.15f - i * 0.01f);
        }
    }

    float GetPosX(int i, float angleOffset, float lengthOffset)
    {
        return spine.joints[i].x + Mathf.Cos(spine.angles[i] + angleOffset) * (BodyWidth(i) + lengthOffset);
    }

    float GetPosY(int i, float angleOffset, float lengthOffset)
    {
        return spine.joints[i].y + Mathf.Sin(spine.angles[i] + angleOffset) * (BodyWidth(i) + lengthOffset);
    }
}
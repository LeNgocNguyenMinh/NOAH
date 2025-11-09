using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField, Range(0f, 1f)] private float heightFactor = 1f; // 1 = nửa hình tròn hoàn chỉnh

    private void OnDrawGizmos()
    {
        if (pointA == null || pointB == null) return;

        Vector2 a = pointA.position;
        Vector2 b = pointB.position;
        Vector2 center = (a + b) / 2f;

        Vector2 dir = b - a;
        float dist = dir.magnitude;
        float radius = dist / 2f;
        float angle = Mathf.Atan2(dir.y, dir.x);

        bool flipArc = b.x < a.x; // nếu B nằm bên trái A → lật cung để luôn cong lên trên

        Gizmos.color = Color.yellow;
        int segments = 30;
        Vector2 prevPos = Vector2.zero;

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float theta;

            if (flipArc)
                theta = Mathf.PI + Mathf.PI * t; // vẽ nửa trên khi B ở bên trái
            else
                theta = Mathf.PI - (Mathf.PI * t); // vẽ nửa trên khi B ở bên phải

            Vector2 local = new Vector2(
                Mathf.Cos(theta) * radius,
                Mathf.Sin(theta) * radius * heightFactor
            );

            Vector2 rotated = RotatePoint(local, angle);
            Vector2 worldPos = center + rotated;

            if (i > 0)
                Gizmos.DrawLine(prevPos, worldPos);

            prevPos = worldPos;
        }

        // Vẽ điểm đầu và cuối
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(a, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(b, 0.1f);
    }

    private Vector2 RotatePoint(Vector2 point, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        return new Vector2(
            point.x * cos - point.y * sin,
            point.x * sin + point.y * cos
        );
    }
}

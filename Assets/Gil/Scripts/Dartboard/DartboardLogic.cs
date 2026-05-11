using UnityEngine;

public class DartboardLogic : MonoBehaviour
{
    [Header("Radios")]
    [SerializeField] private float bullseyeRadius = 0.02f;
    [SerializeField] private float outerBullRadius = 0.05f;

    [SerializeField] private float tripleInnerRadius = 0.20f;
    [SerializeField] private float tripleOuterRadius = 0.23f;

    [SerializeField] private float doubleInnerRadius = 0.32f;
    [SerializeField] private float doubleOuterRadius = 0.35f;

    [SerializeField] private float boardRadius = 0.35f;

    private int[] sectors =
    {
        20, 1, 18, 4, 13,
        6, 10, 15, 2, 17,
        3, 19, 7, 16, 8,
        11, 14, 9, 12, 5
    };

    public int GetScore(Vector3 hitPoint)
    {
        Vector3 localPoint = transform.InverseTransformPoint(hitPoint);

        Vector2 point2D = new Vector2(localPoint.x, localPoint.y);

        float distance = point2D.magnitude;

        if (distance <= bullseyeRadius)
        {
            Logger.Instance.Log("Bullseye 50", this);
            return 50;
        }

        if (distance <= outerBullRadius)
        {
            Logger.Instance.Log("Outer Bull 25", this);
            return 25;
        }

        if (distance > boardRadius)
        {
            Logger.Instance.Log("Fallo pipipi", this);
            return 0;
        }

        float angle = Mathf.Atan2(point2D.y, point2D.x) * Mathf.Rad2Deg;

        angle = (angle + 360f) % 360f;

        angle += 9f;

        int sectorIndex = Mathf.FloorToInt(angle / 18f);

        sectorIndex = Mathf.Clamp(sectorIndex, 0, 19);

        int baseScore = sectors[sectorIndex];

        if (distance >= tripleInnerRadius &&
            distance <= tripleOuterRadius)
        {
            Logger.Instance.Log($"Triple {baseScore * 3}", this);
            return baseScore * 3;
        }

        if (distance >= doubleInnerRadius &&
            distance <= doubleOuterRadius)
        {
            Logger.Instance.Log($"Double {baseScore * 2}", this);
            return baseScore * 2;
        }
        Logger.Instance.Log($"Normal {baseScore}", this);
        return baseScore;
    }

    void OnDrawGizmos()
    {
        DrawCircle(bullseyeRadius, Color.red);
        DrawCircle(outerBullRadius, Color.yellow);

        DrawCircle(tripleInnerRadius, Color.blue);
        DrawCircle(tripleOuterRadius, Color.blue);

        DrawCircle(doubleInnerRadius, Color.green);
        DrawCircle(doubleOuterRadius, Color.green);

        DrawCircle(boardRadius, Color.white);
    }


    private void DrawCircle(float radio, Color color)
    {
        Gizmos.color = color;

    int segments = 100;
    Vector3 previousPoint = Vector3.zero;

    for (int i = 0; i <= segments; i++)
    {
        float angle = i * Mathf.PI * 2f / segments;

        float x = Mathf.Cos(angle) * radio;
        float y = Mathf.Sin(angle) * radio;

        Vector3 localPoint = new Vector3(x, y, 0);

        Vector3 worldPoint = transform.TransformPoint(localPoint);

        if (i > 0)
        {
            Gizmos.DrawLine(previousPoint, worldPoint);
        }

        previousPoint = worldPoint;
    }
    }
}

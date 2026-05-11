using UnityEngine;

public class ColisionUI : MonoBehaviour
{
    public RectTransform jugador;
    public Transform contenedorObstaculos;

  void Update()
{
    foreach (Transform t in contenedorObstaculos)
    {
        RectTransform obs = t.GetComponent<RectTransform>();
        if (obs == null) continue;

        ObstaculoVentas scriptObs = obs.GetComponent<ObstaculoVentas>();

        if (Overlap(jugador, obs))
        {
            if (scriptObs != null && !scriptObs.fueAtravesado)
            {
                scriptObs.fueAtravesado = true;
                VentasManager.Instance.ModificarVentas(10f);

                if (t.TryGetComponent(out UnityEngine.UI.Image img)) 
                    img.color = Color.green;
            }
        }
    }
}
void OnDrawGizmos()
{
    if (jugador == null || contenedorObstaculos == null) return;

    Gizmos.color = Color.green;
    DrawRect(jugador);

    Gizmos.color = Color.red;
    foreach (Transform t in contenedorObstaculos)
    {
        RectTransform rt = t.GetComponent<RectTransform>();
        if (rt != null)
            DrawRect(rt);
    }
}

void DrawRect(RectTransform rt)
{
    Vector3[] corners = new Vector3[4];
    rt.GetWorldCorners(corners);

    for (int i = 0; i < 4; i++)
    {
        Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
    }
}

 bool Overlap(RectTransform a, RectTransform b)
{
    Rect rectA = GetRect(a);
    Rect rectB = GetRect(b);

    return rectA.Overlaps(rectB);
}

Rect GetRect(RectTransform rt)
{
    Vector3[] corners = new Vector3[4];
    rt.GetWorldCorners(corners);

    return new Rect(
        corners[0].x,
        corners[0].y,
        corners[2].x - corners[0].x,
        corners[2].y - corners[0].y
    );
}
}
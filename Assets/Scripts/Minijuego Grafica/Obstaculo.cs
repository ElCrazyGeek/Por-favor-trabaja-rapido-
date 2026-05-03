using UnityEngine;

public class ObstaculoVentas : MonoBehaviour
{
    public float velocidad = 500f;
    public bool fueAtravesado = false;
    public float limiteFalloX = -600f;

    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.left * velocidad * Time.deltaTime;

        if (!fueAtravesado && rect.anchoredPosition.x < limiteFalloX)
        {
            fueAtravesado = true;
            VentasManager.Instance.ModificarVentas(-10f);
        }

        if (rect.anchoredPosition.x < -1200f)
        {
            Destroy(gameObject);
        }
    }
}
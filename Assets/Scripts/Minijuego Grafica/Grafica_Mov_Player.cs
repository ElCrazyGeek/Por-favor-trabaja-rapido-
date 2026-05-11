using UnityEngine;

public partial class MovimientoGrafica : MonoBehaviour
{
    public RectTransform rect;
    public float fuerzaSalto = 350f;
    public float gravedad = -900f;

    float velocidadY;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            velocidadY = fuerzaSalto;
        }

        velocidadY += gravedad * Time.deltaTime;
        rect.anchoredPosition += Vector2.up * velocidadY * Time.deltaTime;

        // Limitar dentro del área
        float limite = 300f;
        if (rect.anchoredPosition.y > limite)
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, limite);

        if (rect.anchoredPosition.y < -limite)
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -limite);
    }
}
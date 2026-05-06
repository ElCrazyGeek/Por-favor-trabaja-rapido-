using UnityEngine;

public class Documento_grapadora : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform grapadora;

    public float velocidadBase = 250f;
    public float velocidadHuida = 500f;
    public float distanciaHuida = 120f;

    public float limiteX = 500f;
    public float limiteY = 250f;

    private Vector2 direccion;

    void Start()
    {
        NuevaDireccion();
        InvokeRepeating(nameof(NuevaDireccion), 0.8f, 1.2f);
    }

    void Update()
    {
        float distancia = Vector2.Distance(
            rect.anchoredPosition,
            grapadora.anchoredPosition
        );

        Vector2 dirFinal = direccion;

 
        if (distancia < distanciaHuida)
        {
            dirFinal = (rect.anchoredPosition - grapadora.anchoredPosition).normalized;
        }

        float velocidad = (distancia < distanciaHuida) ? velocidadHuida : velocidadBase;

        rect.anchoredPosition += dirFinal * velocidad * Time.deltaTime;


        Vector2 pos = rect.anchoredPosition;

        if (Mathf.Abs(pos.x) > limiteX)
        {
            pos.x = Mathf.Sign(pos.x) * limiteX;
            direccion.x *= -1;
        }

        if (Mathf.Abs(pos.y) > limiteY)
        {
            pos.y = Mathf.Sign(pos.y) * limiteY;
            direccion.y *= -1;
        }

        rect.anchoredPosition = pos;
    }

    void NuevaDireccion()
    {
        direccion = Random.insideUnitCircle.normalized;
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SistemaGrapado : MonoBehaviour
{
    [Header("Referencias")]
    public RectTransform puntaGrapadora;
    public RectTransform zonaValida;

    [Header("UI")]
    public TextMeshProUGUI textoIntentos;

    public Slider barraTiempo;
    public Image fillTiempo; // 👈 IMPORTANTE (el Fill del slider)

    public Gradient gradienteTiempo;
    public float umbralPeligro = 0.25f;

    public float velocidadParpadeo = 8f;
    public float intensidadShake = 5f;

    public GameObject panelVictoria;
    public GameObject panelDerrota;

    [Header("Gameplay")]
    public int intentosMax = 3;
    public float tiempoTotal = 4f;
    public float tiempoVentana = 0.25f;

    private int intentosActuales;
    private float timerTotal;
    private float timerVentana;

    private bool ventanaActiva = false;
    private bool activo = true;

    private Vector3 posicionOriginalBarra;

    void Start()
    {
        intentosActuales = intentosMax;
        timerTotal = tiempoTotal;

        panelVictoria.SetActive(false);
        panelDerrota.SetActive(false);

        posicionOriginalBarra = fillTiempo.rectTransform.anchoredPosition;

        ActualizarUI();
    }

    void Update()
    {
        if (!activo) return;

        // ⏳ Tiempo
        timerTotal -= Time.deltaTime;
        float porcentaje = timerTotal / tiempoTotal;

        barraTiempo.value = porcentaje;

        // 🎨 Color con Gradient
        fillTiempo.color = gradienteTiempo.Evaluate(porcentaje);

        // ⚠️ Peligro (parpadeo + shake)
        if (porcentaje <= umbralPeligro)
        {
            float t = Mathf.Abs(Mathf.Sin(Time.time * velocidadParpadeo));
            fillTiempo.color = Color.Lerp(Color.red, Color.white, t);

            Vector2 shake = Random.insideUnitCircle * intensidadShake;
            fillTiempo.rectTransform.anchoredPosition = posicionOriginalBarra + (Vector3)shake;
        }
        else
        {
            fillTiempo.rectTransform.anchoredPosition = posicionOriginalBarra;
        }

        if (timerTotal <= 0)
        {
            Perder("Tiempo agotado");
        }

        // 🎯 Detección real (overlap)
        bool enZona = GetWorldRect(puntaGrapadora)
                      .Overlaps(GetWorldRect(zonaValida));

        if (enZona && !ventanaActiva)
        {
            ventanaActiva = true;
            timerVentana = tiempoVentana;
        }

        if (ventanaActiva)
        {
            timerVentana -= Time.deltaTime;

            if (timerVentana <= 0)
            {
                ventanaActiva = false;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (ventanaActiva)
            {
                Ganar();
            }
            else
            {
                Fallo();
            }
        }
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        float x = corners[0].x;
        float y = corners[0].y;
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        return new Rect(x, y, width, height);
    }

    void Fallo()
    {
        intentosActuales--;
        ActualizarUI();

        if (intentosActuales <= 0)
        {
            Perder("Sin intentos");
        }
    }

    void Ganar()
    {
        activo = false;
        panelVictoria.SetActive(true);
        Debug.Log("GANASTE");
    }

    void Perder(string razon)
    {
        activo = false;
        panelDerrota.SetActive(true);
        Debug.Log("PERDISTE: " + razon);
    }

    void ActualizarUI()
    {
        textoIntentos.text = "Intentos: " + intentosActuales;
    }
}
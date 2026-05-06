using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SistemaGrapado : MonoBehaviour
{
    [Header("Referencias")]
    public RectTransform puntaGrapadora;
    public RectTransform zonaValida;

    [Header("UI")]
    public TextMeshProUGUI textoIntentos;
    public Image barraTiempo;
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    [Header("Gameplay")]
    public int intentosMax = 3;
    public float tiempoTotal = 4f;
    public float tiempoVentana = 0.25f;
    public float tolerancia = 40f;

    private int intentosActuales;
    private float timerTotal;
    private float timerVentana;

    private bool ventanaActiva = false;
    private bool activo = true;

    void Start()
    {
        intentosActuales = intentosMax;
        timerTotal = tiempoTotal;

        panelVictoria.SetActive(false);
        panelDerrota.SetActive(false);

        ActualizarUI();
    }

    void Update()
{
    if (!activo) return;

    timerTotal -= Time.deltaTime;
    barraTiempo.fillAmount = timerTotal / tiempoTotal;

    if (timerTotal <= 0)
    {
        Perder("Tiempo agotado");
    }

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

    if (Input.GetMouseButtonDown(0))
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
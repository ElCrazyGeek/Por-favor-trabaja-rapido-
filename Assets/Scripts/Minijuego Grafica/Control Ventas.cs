using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class VentasManager : MonoBehaviour
{
    public static VentasManager Instance;

    public float ventasActuales = 50f;
    public float tiempoRestante = 20f;

    [Header("UI")]
    public Slider barraVentas;
    public TextMeshProUGUI textoTiempo;
    public Image fondoTotalBarra;
    public Gradient gradienteColor;

    [Header("Final")]
    public GameObject panelFinal;
    public TextMeshProUGUI textoFinal;
    public CameraShake sacudidor;
    public AudioSource musicaFondo;

    bool juegoTerminado = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (juegoTerminado) return;

        ActualizarUI();
        ActualizarTiempo();
    }

    void ActualizarUI()
    {
        barraVentas.value = ventasActuales;

        float porcentaje = ventasActuales / 100f;

        if (fondoTotalBarra != null)
            fondoTotalBarra.color = gradienteColor.Evaluate(porcentaje);
    }

    void ActualizarTiempo()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante > 0)
        {
            textoTiempo.text = "Tiempo: " + tiempoRestante.ToString("F1");
        }
        else
        {
            FinalizarJuego();
        }
    }

    public void ModificarVentas(float cantidad)
    {
        ventasActuales = Mathf.Clamp(ventasActuales + cantidad, 0, 100);

        // Feedback visual rápido (sacudida leve)
        if (sacudidor != null)
        {
            StartCoroutine(sacudidor.Shake(0.15f, 0.05f));
        }
    }

    void FinalizarJuego()
    {
        juegoTerminado = true;
        StartCoroutine(SecuenciaFinal());
    }

    IEnumerator SecuenciaFinal()
    {
        if (musicaFondo != null) musicaFondo.Stop();

        yield return new WaitForSeconds(1f);

        panelFinal.SetActive(true);

        if (ventasActuales >= 70f)
        {
            textoFinal.text = "COMPLETADO";
            textoFinal.color = Color.green;
        }
        else
        {
            textoFinal.text = "¡DESPEDIDO!";
            textoFinal.color = Color.red;
        }

        if (sacudidor != null)
        {
            StartCoroutine(sacudidor.Shake(0.5f, 0.2f));
        }
    }
}
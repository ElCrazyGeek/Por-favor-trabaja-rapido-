using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class VentasManager : MonoBehaviour
{
    public static VentasManager Instance;

    public float ventasActuales = 50f; // Empieza a la mitad para dar margen
    public float tiempoRestante = 20f;
    
    [Header("Referencias UI")]
    public Slider barraVentas;
    public TextMeshProUGUI textoTiempo;
    public Image fondoTotalBarra;
    public Gradient gradienteColor;
    
    [Header("Final del Juego")]
    public GameObject panelFinal; 
    public TextMeshProUGUI textoFinal;
    public CameraShake sacudidor; 
    public AudioSource musicaFondo;

    private bool juegoTerminado = false;

    void Awake() { Instance = this; }

    void Update()
    {
        if (juegoTerminado) return;

        // 1. Actualizar el Slider (Asegúrate que Max Value sea 100 en el Inspector)
        barraVentas.value = ventasActuales;

        // 2. Actualizar el color del fondo
        float porcentaje = (100 -ventasActuales) / 100f;
        if(fondoTotalBarra != null)
            fondoTotalBarra.color = gradienteColor.Evaluate(porcentaje);

        // 3. Manejo del tiempo
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            textoTiempo.text = "Tiempo restante: " + tiempoRestante.ToString("F1") + "s";
        }
        else
        {
            FinalizarJuego();
        }
    }

    public void ModificarVentas(float cantidad)
    {
        ventasActuales = Mathf.Clamp(ventasActuales + cantidad, 0, 100);
    }

    void FinalizarJuego()
    {
        juegoTerminado = true;
        Time.timeScale = 0;
        StartCoroutine(SecuenciaFinal());
    }

    IEnumerator SecuenciaFinal()
    {
        if (musicaFondo != null) musicaFondo.Stop(); 
        
        // Usamos WaitForSecondsRealtime por si pausaste el Time.timeScale
        yield return new WaitForSecondsRealtime(1f); 

        panelFinal.SetActive(true);

        if (ventasActuales >= 70f)
        {
            textoFinal.text = "COMPLETADO";
            textoFinal.color = Color.green;
            if(sacudidor != null) StartCoroutine(sacudidor.Shake(0.3f, 0.1f));
        }
        else
        {
            textoFinal.text = "¡DESPEDIDO!";
            textoFinal.color = Color.red;
            if(sacudidor != null) StartCoroutine(sacudidor.Shake(0.8f, 0.4f));
        }
    }
}
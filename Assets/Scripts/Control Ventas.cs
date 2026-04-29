using UnityEngine;
using TMPro; // Si usas TextMeshPro
using UnityEngine.UI;

public class VentasManager : MonoBehaviour
{
    public static VentasManager Instance; // Singleton para acceder desde otros scripts

    public float ventasActuales = 1f;
    public float tiempoRestante = 20f;
    public Slider barraVentas;
    public TextMeshProUGUI textoTiempo;

    private bool juegoTerminado = false;

    void Awake() { Instance = this; }

    void Update()
    {
        if (juegoTerminado) return;

        // Manejo del tiempo
        if (tiempoRestante >= 0)
        {
            tiempoRestante -= Time.deltaTime;

            textoTiempo.text = "Tiempo restante: " + tiempoRestante.ToString("F1") + "s";
        }
        else
        {
            Time.timeScale = 0;
            FinalizarJuego();
        }

        // Actualizar Interfaz
        barraVentas.value = ventasActuales / 100f;
    }

    public void ModificarVentas(float cantidad)
    {
        ventasActuales = Mathf.Clamp(ventasActuales + cantidad, 0, 100);
    }

    void FinalizarJuego()
    {
        juegoTerminado = true;
        if (ventasActuales >= 70f) Debug.Log("¡Meta de ventas alcanzada!");
        else Debug.Log("Despedido... ventas insuficientes.");
    }
}
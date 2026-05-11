using UnityEngine;
using TMPro;

public class GamePapeles : MonoBehaviour
{
    public int papelesNecesarios = 3;
    private int papelesActuales = 0;
    
    public float tiempoRestante = 30f;
    public bool juegoTerminado = false;

    [Header("Elementos de Interfaz")]
    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoTiempo;
    public GameObject objetoHUD; // <-- Arrastra aquí el objeto "HUD" que creaste

    [Header("Paneles de Estado")]
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    void Update()
    {
        if (!juegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarInterfaz();

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                Derrota();
            }
        }
    }

    void ActualizarInterfaz()
    {
        if (textoContador != null) textoContador.text = "Papeles: " + papelesActuales + "/" + papelesNecesarios;
        if (textoTiempo != null) textoTiempo.text = "Tiempo: " + tiempoRestante.ToString("F1") + "s";
    }

    public void SumarPapel()
    {
        if (juegoTerminado) return;
        papelesActuales++;
        if (papelesActuales >= papelesNecesarios) Ganar();
    }

    public void QuitarTiempo(float cantidad)
    {
        if (juegoTerminado) return;
        tiempoRestante -= cantidad;
    }

    void Ganar()
    {
        juegoTerminado = true;
        if (panelVictoria != null) panelVictoria.SetActive(true);
        
        // ESCONDER LA INTERFAZ
        if (objetoHUD != null) objetoHUD.SetActive(false);
    }

    void Derrota()
    {
        juegoTerminado = true;
        if (panelDerrota != null) panelDerrota.SetActive(true);

        // ESCONDER LA INTERFAZ
        if (objetoHUD != null) objetoHUD.SetActive(false);
    }
}
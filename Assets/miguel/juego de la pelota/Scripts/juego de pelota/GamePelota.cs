using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePelota : MonoBehaviour
{
    [Header("Configuración")]
    public int puntosParaGanar = 3;
    public float tiempoRestante = 60f;
    public float retrasoParaSiguienteNivel = 3f; // Segundos de espera antes de cambiar
    private int puntosActuales = 0;
    public bool juegoTerminado = false;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoTiempo;
    public GameObject panelVictoria; 
    public GameObject panelDerrota;  

    void Update()
    {
        if (juegoTerminado) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            textoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRestante).ToString();
        }
        else
        {
            FinalizarJuego(false);
        }
    }

    public void SumarPunto()
    {
        if (juegoTerminado) return;

        puntosActuales++;
        textoPuntos.text = "Puntos: " + puntosActuales;

        if (puntosActuales >= puntosParaGanar)
        {
            FinalizarJuego(true);
        }
    }

    void FinalizarJuego(bool victoria)
    {
        juegoTerminado = true;

        if (victoria)
        {
            panelVictoria.SetActive(true);
            // Llama a la función SiguienteNivel después de X segundos
            Invoke("SiguienteNivel", retrasoParaSiguienteNivel);
        }
        else
        {
            panelDerrota.SetActive(true);
            Time.timeScale = 0f; // Solo pausamos si pierde para que pueda picar el botón
        }
    }

    public void SiguienteNivel()
    {
       Time.timeScale = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
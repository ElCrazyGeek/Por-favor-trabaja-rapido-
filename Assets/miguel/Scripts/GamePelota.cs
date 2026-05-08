using UnityEngine;
using TMPro; // Necesitas tener instalado TextMeshPro
using UnityEngine.SceneManagement;

public class GamePelota : MonoBehaviour
{
    public int puntosParaGanar = 5;
    public float tiempoRestante = 60f;
    
    private int puntosActuales = 0;
    private bool juegoTerminado = false;

    public TextMeshProUGUI textoPuntos; // Arrastra un texto de UI aquí
    public TextMeshProUGUI textoTiempo;  // Arrastra otro texto de UI aquí
    public GameObject panelVictoria;    // Un panel que se active al ganar

    void Update()
    {
        if (juegoTerminado) return;

        // Manejo del tiempo
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarInterfaz();
        }
        else
        {
            TerminarJuego(false); // Perdió por tiempo
        }
    }

    public void SumarPunto()
    {
        if (juegoTerminado) return;

        puntosActuales++;
        ActualizarInterfaz();

        if (puntosActuales >= puntosParaGanar)
        {
            TerminarJuego(true); // Ganó
        }
    }

    void ActualizarInterfaz()
    {
        textoPuntos.text = "Puntos: " + puntosActuales;
        textoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRestante).ToString();
    }

    void TerminarJuego(bool victoria)
    {
        juegoTerminado = true;
        if (victoria)
        {
            Debug.Log("¡Ganaste!");
            panelVictoria.SetActive(true);
        }
        else
        {
            Debug.Log("Juego Terminado - Se acabó el tiempo");
            // Aquí podrías reiniciar la escena: SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
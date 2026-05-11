using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager_Sellos : MonoBehaviour
{
    // ... (Mantén tus referencias de Spawner, UI y Slider igual) ...
    [Header("Referencias UI")]
    public Documento_Spawner spawner;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoFeedback;
    public TextMeshProUGUI textoGritoJefe; 
    public Slider barraDesempeno; 

    [Header("UI de Fin de Juego")]
    public GameObject panelFin; 
    public TextMeshProUGUI textoTituloPrincipal;

    [Header("Reglas de Errores")]
    public TextMeshProUGUI textoErrores; // Opcional: Para mostrar "Errores: 0/3"
    private int erroresEnEstaRonda = 0;
    private int limiteErroresRonda = 2; // Empieza en 2 para la Ronda 1

    private int rondaActual = 1;
    public float tiempoRestante = 30f;
    private bool juegoTerminado = false;
    private int aciertosEnEstaRonda = 0;
    private int documentosProcesadosEnRonda = 0;
    public int documentosTotalesRonda = 5;
    public int cuotaMinimaAciertos = 3;
    private float desempenoActual = 0f;

    void Start()
    {
        panelFin.SetActive(false);
        ActualizarLimiteErrores();
        ActualizarGrafica();
    }

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
            EvaluarRonda();
        }
    }

    void ActualizarLimiteErrores()
    {
        // Regla: Ronda 1 y 4 = 2 errores | Ronda 2 y 3 = 3 errores
        if (rondaActual == 1 || rondaActual == 4) limiteErroresRonda = 2;
        else limiteErroresRonda = 3;

        if (textoErrores != null) 
            textoErrores.text = $"Fallos: {erroresEnEstaRonda}/{limiteErroresRonda}";
    }

    public void RegistrarAcierto(bool fueCorrecto)
    {
        if (juegoTerminado) return;
        documentosProcesadosEnRonda++;

        if (fueCorrecto)
        {
            aciertosEnEstaRonda++;
            // Desempeño escala (más difícil cada ronda)
            float ganancia = Mathf.Max(0.05f, 0.25f - (rondaActual * 0.05f));
            desempenoActual = Mathf.Clamp01(desempenoActual + ganancia);
            StartCoroutine(MostrarFeedback(true));
        }
        else
        {
            erroresEnEstaRonda++;
            float penalizacion = 0.10f + (rondaActual * 0.05f);
            desempenoActual = Mathf.Clamp01(desempenoActual - penalizacion);
            StartCoroutine(MostrarFeedback(false));


            if (erroresEnEstaRonda > limiteErroresRonda)
            {
                FinDelJuego("DESPEDIDO (DEMASIADOS ERRORES)");
                return;
            }
        }

        if (textoErrores != null) 
            textoErrores.text = $"Fallos: {erroresEnEstaRonda}/{limiteErroresRonda}";

        ActualizarGrafica();

        if (documentosProcesadosEnRonda >= documentosTotalesRonda)
        {
            EvaluarRonda();
        }
    }

    void SiguienteRonda()
    {
        rondaActual++;
        aciertosEnEstaRonda = 0;
        documentosProcesadosEnRonda = 0;
        erroresEnEstaRonda = 0; // Reseteamos errores
        desempenoActual = 0f; 

        ActualizarLimiteErrores(); // Ajustamos el límite según la ronda
        ActualizarGrafica();

        documentosTotalesRonda += Random.Range(2, 5);
        cuotaMinimaAciertos = Mathf.CeilToInt(documentosTotalesRonda * 0.5f);

        spawner.GenerarNuevaOleada(documentosTotalesRonda); 
    }

    void EvaluarRonda()
    {
        if (aciertosEnEstaRonda >= cuotaMinimaAciertos)
        {
            if (rondaActual >= 4) FinDelJuego("COMPLETADO");
            else StartCoroutine(SecuenciaGritoSiguienteRonda());
        }
        else
        {
            FinDelJuego("DESPEDIDO (CUOTA BAJA)");
        }
    }

    System.Collections.IEnumerator SecuenciaGritoSiguienteRonda()
    {
        string mensaje = (rondaActual == 1) ? "¡SIGUIENTE!" : (rondaActual == 2) ? "¡MÁS!" : "¡MÁS! ¡MÁS! ¡MÁS!";
        textoGritoJefe.text = mensaje;
        textoGritoJefe.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textoGritoJefe.gameObject.SetActive(false);
        SiguienteRonda();
    }

    void ActualizarGrafica()
    {
        if (barraDesempeno != null) barraDesempeno.value = desempenoActual;
    }

    public void FinDelJuego(string estado)
    {
        juegoTerminado = true;
        panelFin.SetActive(true);
        textoTituloPrincipal.text = estado;
        textoTituloPrincipal.color = (estado == "COMPLETADO") ? Color.green : Color.red;
        Time.timeScale = 0; 
    }

    System.Collections.IEnumerator MostrarFeedback(bool correcto)
    {
        textoFeedback.text = correcto ? "CORRECTO" : "INCORRECTO";
        textoFeedback.color = correcto ? Color.green : Color.red;
        yield return new WaitForSeconds(0.3f);
        textoFeedback.text = "";
    }
}
using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class managerGlobal : MonoBehaviour
{
    public static managerGlobal instance;

    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoInicio;

    [SerializeField] public bool mostrarTexto;

    private Vector3 posicionInicialTexto;

    public bool puedeJugar;

    
    
   

    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject panelDerrota;
    [SerializeField] private GameObject panelVictoria;
    
    void Awake()
    {
        instance = this;
        posicionInicialTexto = textoInicio.transform.localPosition;
    }

    public void empezoMinijuego()
    {
        panelUI.SetActive(true);
         mostrarTexto = true;
        puedeJugar = true;
    }

    // Update is called once per frame
    public void textoInicial(string textoInicial)
    { 
        textoInicio.text = textoInicial;
    }

    public void actualizarTiempo(float tiempoRestante)
    {
        textoTiempo.text = Mathf.Ceil(tiempoRestante).ToString();
    }

    public void ganoMinijuego()
    {
        
        textoInicio.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        panelVictoria.SetActive(true);
        puedeJugar = false;
    }

    public void perdioMinijuego()
    {
       
        textoInicio.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        panelDerrota.SetActive(true);
        puedeJugar = false;
    }

     public IEnumerator mostrarTextoInicial(string texto)
    {
        textoInicial(texto);
        yield return new WaitForSeconds(1f);
        StartCoroutine(salidaTexto());
    }

    IEnumerator salidaTexto()
    {
        textoInicio.transform.localPosition = posicionInicialTexto;

        float duracion = 0.3f;
        float tiempo = 0f;
        

        Vector3 posicionActual = textoInicio.transform.localPosition;
        Vector3 posicionFinal = posicionActual + new Vector3(1200f, 1200f, 0);

        while(tiempo < duracion)
        {
            tiempo+= Time.deltaTime;
            float t = tiempo / duracion;
            t = t*t*t;
            textoInicio.transform.localPosition = Vector3.Lerp(posicionActual, posicionFinal, t);
            yield return null;
        }

          
        textoInicio.text = "";
        textoInicio.gameObject.SetActive(false);
        textoInicio.transform.localPosition = posicionInicialTexto;
        mostrarTexto = false;


    }


 

   

    
}

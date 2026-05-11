using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class managerGlobal : MonoBehaviour
{
    public static managerGlobal instance;

    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoInicio;
     [SerializeField] public TextMeshProUGUI textoTotal;


    [SerializeField] public bool mostrarTexto;

    private Vector3 posicionInicialTexto;

    public bool puedeJugar;

    
    
   
   
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject panelDerrota;
    [SerializeField] public GameObject panelVictoria;
    [SerializeField] public GameObject panelVictoriaFinal;
    [SerializeField] private CanvasGroup globalCanvasGroup;
    

    [Header ("Archivos de Audio")]
     [SerializeField] private AudioClip sfxSwoosh;
     [SerializeField] private AudioClip sfxStart;
     [SerializeField] private AudioClip sfxVictoria;
     [SerializeField] private AudioClip sfxDerrota;


    
    void Awake()
    {
        instance = this;
        posicionInicialTexto = textoInicio.transform.localPosition;
    }


    void Update()
    {
        if (puedeJugar)
        {
            audioManager.instance.reproducirMusica(1);
        } else
        {
            audioManager.instance.reproducirMusica(2);
        }
    }





    public void empezoMinijuego()
    {
        panelUI.SetActive(true);
         mostrarTexto = true;
        puedeJugar = true;
        textoInicio.gameObject.SetActive(true);
        textoTiempo.gameObject.SetActive(true);
        globalCanvasGroup.blocksRaycasts = false;
        
        StartCoroutine(ReproducirInicio());
       
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
        if (puedeJugar)
        {
        audioManager.instance.reproducirSFX(sfxVictoria);
        }

        ManagerMinijuegos.instance.juegosCompletados++;

        if(ManagerMinijuegos.instance.juegosCompletados >= ManagerMinijuegos.instance.totalMinijuegos)
        {
            panelDerrota.SetActive(false);
            panelVictoriaFinal.SetActive(true);
        } else
        { 
            panelVictoria.SetActive(true);
             textoTotal.gameObject.SetActive(true);
        }

       
        textoInicio.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        
        puedeJugar = false;
        globalCanvasGroup.blocksRaycasts = true;
    }

    public void perdioMinijuego()
    {
        if (puedeJugar)
        {  
       audioManager.instance.reproducirSFX(sfxDerrota);
        }
        textoTotal.gameObject.SetActive(true);
        textoInicio.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        panelDerrota.SetActive(true);
        puedeJugar = false;
        globalCanvasGroup.blocksRaycasts = true;

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

        audioManager.instance.reproducirSFX(sfxSwoosh);

          
        textoInicio.text = "";
        textoInicio.gameObject.SetActive(false);
        textoInicio.transform.localPosition = posicionInicialTexto;
        mostrarTexto = false;

      

    }

      IEnumerator ReproducirInicio()
        {
            yield return null;
                audioManager.instance.reproducirSFX(sfxStart);
        }

 


 

   

    
}

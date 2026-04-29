using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class managerGlobal : MonoBehaviour
{
    public static managerGlobal instance;

    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoInicio;

    [SerializeField] public bool mostrarTexto;

    

    
    
   

    [SerializeField] private GameObject panelUI;
    
    void Awake()
    {
        instance = this;
    }

    public void empezoMinijuego()
    {
        panelUI.SetActive(true);
         mostrarTexto = true;
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
       
    }

    public void perdioMinijuego()
    {
       
    }

    public void ocutarTextoInicial()
    {
        mostrarTexto = false;
        textoInicio.text = "";
        textoInicio.gameObject.SetActive(false);
    }

    
}

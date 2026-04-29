using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class managerGlobal : MonoBehaviour
{
    public static managerGlobal instance;

    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoIncio;
   

    [SerializeField] private GameObject panelUI;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void iniciarMinijuego(string textoInicial)
    {
        panelUI.SetActive(true);
        textoIncio.text = textoInicial;
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

    
}

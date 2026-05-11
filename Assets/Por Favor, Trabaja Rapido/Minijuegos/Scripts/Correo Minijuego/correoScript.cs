using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class correoScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textoPregunta;
    [SerializeField] public TextMeshProUGUI textoSi;
    [SerializeField] public TextMeshProUGUI textoNo;

    [SerializeField] private int contadorFase;
    [SerializeField] private GameObject canvasMinijuego;
         [SerializeField] private AudioClip sfxBoton;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         cambiarFase();
        if (contadorFase == 7)
        {
            contadorFase = 999; 
            canvasMinijuego.SetActive(false);
            managerGlobal.instance.ganoMinijuego();
        }
    }

    public void cambiarFase()
    {
        switch (contadorFase)
        {
            case 0:
            textoPregunta.text= "¿Desea Mandar el Correo?";
            textoSi.text = "Si";
            textoNo.text = "No";
            break;
            case 1:
            textoPregunta.text= "¿Esta Seguro?";
            textoSi.text = "Si";
            textoNo.text = "No";
            break;
            case 2:
            textoPregunta.text= "¿De verdad?";
            textoSi.text = "Si";
            textoNo.text = "No";
            break;
            case 3:
            textoPregunta.text= "¿Quiere mandar un correo?";
            textoSi.text = "Si";
            textoNo.text = "No";
            break;
            case 4:
            textoPregunta.text= "¿Completamente Seguro?";
            textoSi.text = "No";
            textoNo.text = "Si";
            break;
            case 5:
            textoPregunta.text= "¿De verdad?";
            textoSi.text = "Si";
            textoNo.text = "No";
            break;
            case 6:
            textoPregunta.text= "¿Tiene intencion de mandar un correo?";
            textoSi.text = "No";
            textoNo.text = "Si";
            break;
        }
    }

    public void botonSi()
        {
            audioManager.instance.reproducirSFX(sfxBoton);
            switch (contadorFase)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    contadorFase++;
                    break;

                case 4:
                case 6:
                    managerGlobal.instance.perdioMinijuego();
                    canvasMinijuego.SetActive(false);
                    break;

                case 5:
                    contadorFase++;
                    break;
            }

            ActualizarUI();
        }

   public void botonNo()
{
    audioManager.instance.reproducirSFX(sfxBoton);

    switch (contadorFase)
    {
        case 0:
        case 1:
        case 2:
        case 3:
        case 5:
            managerGlobal.instance.perdioMinijuego();
            canvasMinijuego.SetActive(false);
            break;

        case 4:
        case 6:
            contadorFase++;
            break;
    }

    ActualizarUI();
}

void ActualizarUI()
{
    contadorFase = Mathf.Clamp(contadorFase, 0, 7);
}



}

using UnityEngine;
using UnityEngine.UI;

public class Documentos : MonoBehaviour
{
   public enum TipoDocumento { Normal, Despido }
    public TipoDocumento tipo = TipoDocumento.Normal;

    public bool Valido;
    private Color color;
    public Color colorA;
    public Color colorR;
    public Color colorD;
    private Image imagenFondo;
    private bool seEstaYendo = false;
    private float velocidadVuelo = 1200f;
    private Vector3 direccionRecogida = new Vector3(1.2f, 0.5f, 0);

void Start() 
    {
        imagenFondo = GetComponent<Image>();
        
        // 1. Decidir si es un documento de despido (ej. 15% de probabilidad)
        if (Random.value < 0.15f) 
        {
            tipo = TipoDocumento.Despido;
        }
        else 
        {
            tipo = TipoDocumento.Normal;
            // Si es normal, decidimos si es válido o no
            Valido = (Random.value > 0.5f);
        }

        ConfigurarDocumento();
    }

    void Update()
{
    if (seEstaYendo)
    {
        // Movimiento hacia el lado
        transform.Translate(direccionRecogida * velocidadVuelo * Time.deltaTime, Space.World);
        
        // Rotación
        transform.Rotate(Vector3.forward * 150f * Time.deltaTime);

        // Efecto de encogimiento
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 4f * Time.deltaTime);
    }
}

public void ConfigurarDocumento()
    {
        Color colorFinal;

        if (tipo == TipoDocumento.Despido)
        {
            colorFinal = colorD;
        }
        else
        {
            colorFinal = Valido ? colorA : colorR;
        }

        if (imagenFondo != null)
        {
            colorFinal.a = 1f; 
            imagenFondo.color = colorFinal;
        }
    }
    public void SerRecogido() // Puedes renombrarla a "SerRecogido" si prefieres
    {
        seEstaYendo = true;
        
        // Desactivamos el Raycast para que no puedas volver a sellarlo mientras se va
        if(GetComponent<Image>() != null) 
            GetComponent<Image>().raycastTarget = false;

        // Lo destruimos cuando ya está fuera de vista
        Destroy(gameObject, 0.8f);
    }

}
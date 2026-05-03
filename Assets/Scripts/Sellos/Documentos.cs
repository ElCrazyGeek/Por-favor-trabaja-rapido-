using UnityEngine;
using UnityEngine.UI;

public class Documentos : MonoBehaviour
{
    public bool Valido;
    private Color color;
    public Color colorA;
    public Color colorR;
    
    private Image imagenFondo;

    void Start() // Cambiado de Awake a Start
    {
        imagenFondo = GetComponent<Image>();
        
        // Generamos un valor aleatorio y lo asignamos
        Valido = (Random.value > 0.5f);

        ConfigurarDocumento();
    }

    public void ConfigurarDocumento()
    {
        // Usamos colores un poco más vivos para que se note el cambio
        if (Valido)
        {
            color = colorA;
        }
        else
        {
            color = colorR;
        }

        if (imagenFondo != null)
        {
            imagenFondo.color = color;
        }
    }

    // Dentro de Documentos.cs
public void SalirDeEscena()
{
    // Una forma simple: activar una bandera para que en el Update se mueva solo
    seEstaYendo = true;
    Destroy(gameObject, 2f); // Se destruye en 2 segundos
}

bool seEstaYendo = false;
void Update()
{
    if(seEstaYendo)
    {
        // Se mueve hacia la derecha rápidamente
        transform.Translate(Vector3.right * 1000f * Time.deltaTime);
    }
}
}
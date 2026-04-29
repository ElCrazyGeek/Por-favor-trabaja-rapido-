using UnityEngine;

public partial class MovimientoGrafica : MonoBehaviour
{
    public Rigidbody Jugador;
    public float fuerzaSalto = 5f;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Saltar();
        }
    }

    void Saltar()
    {

        Jugador.linearVelocity = Vector2.up * fuerzaSalto;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si chocamos con un objetivo
        if (other.CompareTag("Objetivo"))
        {
            ObstaculoVentas objetivo = other.GetComponent<ObstaculoVentas>();

            // Si el objetivo existe y aún no ha sido atravesado
            if (objetivo != null && !objetivo.fueAtravesado)
            {
                objetivo.fueAtravesado = true; // Lo marcamos para que no cuente como fallo después
                VentasManager.Instance.ModificarVentas(15f); // Sumamos puntos
                
                // Feedback visual: verde al tocarlo
                other.GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("¡Venta exitosa!");
            }
        }
    }

    
}
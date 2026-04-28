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

    
}
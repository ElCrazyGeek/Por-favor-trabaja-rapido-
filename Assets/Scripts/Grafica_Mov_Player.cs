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
        if (Input.GetMouseButtonDown(0))
        {
            Saltar();
        }
    }

    void Saltar()
    {

        Jugador.linearVelocity = Vector2.up * fuerzaSalto;
    }
}
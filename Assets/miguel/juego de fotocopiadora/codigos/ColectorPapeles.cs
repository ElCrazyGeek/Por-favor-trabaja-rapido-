using UnityEngine;

public class ColectorPapeles : MonoBehaviour
{
    private int verdesRecolectados = 0;
    public int metaVerdes = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PapelVerde"))
        {
            verdesRecolectados++;
            Debug.Log("¡Papel verde atrapado! Llevas: " + verdesRecolectados);
            Destroy(other.gameObject); // El papel desaparece al atraparlo

            if (verdesRecolectados >= metaVerdes)
            {
                Debug.Log("¡Ganaste el minijuego de la fotocopiadora!");
                // Aquí llamarás a la victoria de tu nuevo GameManager
            }
        }
        else if (other.CompareTag("PapelMalo"))
        {
            Debug.Log("¡Cuidado! Ese no era verde.");
            Destroy(other.gameObject);
        }
    }
}
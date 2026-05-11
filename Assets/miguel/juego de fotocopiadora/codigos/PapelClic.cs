using UnityEngine;

public class PapelClic : MonoBehaviour
{
    private GamePapeles gameManager;
    public bool esMalo = false;

    void Start() { gameManager = Object.FindAnyObjectByType<GamePapeles>(); }

    void OnMouseDown()
    {
        if (gameManager == null || gameManager.juegoTerminado) return;

        if (CompareTag("PapelVerde"))
        {
            gameManager.SumarPapel();
        }
        else if (esMalo) // Si es el papel rojo/malo
        {
            gameManager.QuitarTiempo(5f); // Quita 5 segundos
        }
        
        Destroy(gameObject);
    }
}
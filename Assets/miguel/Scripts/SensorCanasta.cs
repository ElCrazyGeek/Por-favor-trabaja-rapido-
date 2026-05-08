using UnityEngine;

public class SensorCanasta : MonoBehaviour
{
   private GamePelota gameManager; 

    void Start()
    {
        // El script buscará automáticamente el objeto que tenga el componente GamePelota
        gameManager = Object.FindAnyObjectByType<GamePelota>();

        if (gameManager == null)
        {
            Debug.LogError("¡No encontré el GamePelota en la escena!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (gameManager != null)
            {
                gameManager.SumarPunto();
                other.tag = "Untagged"; // Para que no cuente doble
                Destroy(other.gameObject, 0.5f);
            }
        }
    }
}
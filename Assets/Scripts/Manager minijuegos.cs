using System.Collections.Generic;
using UnityEngine;

public class Managerminijuegos : MonoBehaviour
{
    [Header("Configuración")]
    public Transform spawnPoint;
    public List<GameObject> miniGamePrefabs;
    public GameObject panelDeBotones;

    private GameObject currentActiveGame; 

    public void LaunchGame(int index)
    {
        // 1. Limpieza: Si hay un juego corriendo, lo destruimos
        if (currentActiveGame != null)
        {
            Destroy(currentActiveGame);
        }

        // 2. Validación
        if (index < 0 || index >= miniGamePrefabs.Count)
        {
            Debug.LogWarning("Índice de juego no válido");
            return;
        }

        // 3. Instanciar el nuevo juego
        currentActiveGame = Instantiate(miniGamePrefabs[index], spawnPoint.position, Quaternion.identity);
        
        // Opcional: Hacerlo hijo del spawnPoint para mantener orden en la jerarquía
        currentActiveGame.transform.SetParent(spawnPoint);

        if (panelDeBotones != null) 
    {
        panelDeBotones.SetActive(false); 
    }
    }
}
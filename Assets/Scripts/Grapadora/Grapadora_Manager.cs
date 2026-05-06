using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SistemaGrapado : MonoBehaviour
{
    public Transform spawnPoint;
    public List<GameObject> miniGamePrefabs;
    
    // 1. Agregamos una referencia al panel que contiene tus botones
    public GameObject panelDeBotones; 

    private GameObject currentActiveGame;
    
    public void LaunchGame(int index)
    {
        if (currentActiveGame != null) Destroy(currentActiveGame);

        if (index < 0 || index >= miniGamePrefabs.Count) return;

        currentActiveGame = Instantiate(miniGamePrefabs[index], spawnPoint.position, Quaternion.identity);
        currentActiveGame.transform.SetParent(spawnPoint);

        // 2. Apagamos el menú de botones para que no estorbe
        if (panelDeBotones != null)
        {
            panelDeBotones.SetActive(false);
        }
    }

    // 3. Método para regresar al menú (llámalo cuando el minijuego termine)
    public void ShowMenu()
    {
        if (currentActiveGame != null) Destroy(currentActiveGame);
        panelDeBotones.SetActive(true);
    }
}
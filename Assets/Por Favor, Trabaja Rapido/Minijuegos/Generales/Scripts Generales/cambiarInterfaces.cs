using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiarInterfaces : MonoBehaviour
{

    public static cambiarInterfaces instance;

    public GameObject prefab;
    
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void volverMenu()
    {
        
        SceneManager.LoadScene("Menu Principal");
    }

    public void continuar()
    {
         Destroy(prefab);
         prefab = null;
    }

    public void obtenerPrefab(GameObject prefabMinijuego)
    {
        prefab = prefabMinijuego;
    }   

    
}

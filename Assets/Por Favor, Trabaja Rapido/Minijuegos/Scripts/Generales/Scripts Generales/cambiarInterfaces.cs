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
        managerGlobal.instance.textoTotal.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu Principal");
       

    }

    public void continuar()
    {
        if (ManagerMinijuegos.instance.juegosCompletados >= ManagerMinijuegos.instance.totalMinijuegos)
        {
            return;
        }

         managerGlobal.instance.textoTotal.gameObject.SetActive(false);
         Destroy(prefab);
         prefab = null;
          ManagerMinijuegos.instance.SiguienteMinijuego();
          managerGlobal.instance.panelVictoria.SetActive(false);
    }

    public void obtenerPrefab(GameObject prefabMinijuego)
    {
        prefab = prefabMinijuego;
    }   

    
}

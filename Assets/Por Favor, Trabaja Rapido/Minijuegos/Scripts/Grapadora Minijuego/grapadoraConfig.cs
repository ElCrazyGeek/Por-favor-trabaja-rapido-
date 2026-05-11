using UnityEngine;

public class grapadoraConfig : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    void Start()
    {
        
        managerGlobal.instance.empezoMinijuego();
        cambiarInterfaces.instance.obtenerPrefab(gameObject);

         StopAllCoroutines();
        
        StartCoroutine(managerGlobal.instance.mostrarTextoInicial("¡Engrapa!"));

    }

    // Update is called once per frame
    void Update()
    {
         if (managerGlobal.instance.puedeJugar)
        {
        tiempoMaximo-=Time.deltaTime;
        }

        if (tiempoMaximo <= 0)
        {
            managerGlobal.instance.perdioMinijuego();
            
        }

        managerGlobal.instance.actualizarTiempo(tiempoMaximo);
    }
}

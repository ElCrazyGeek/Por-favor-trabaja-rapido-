using Unity.VisualScripting;
using UnityEngine;

public class lamparaConfig : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    void Start()
    {
        managerGlobal.instance.empezoMinijuego();
        cambiarInterfaces.instance.obtenerPrefab(gameObject);
        IluminacionManager.instance.ActivarOscuridad();

        StopAllCoroutines();
        
        StartCoroutine(managerGlobal.instance.mostrarTextoInicial("¡Busca!"));
    }

    // Update is called once per frame
    void Update()
    {
         if (managerGlobal.instance.puedeJugar)
        {
        tiempoMaximo-=Time.deltaTime;
        }


        if(tiempoMaximo <= 0f)
        {
             managerGlobal.instance.perdioMinijuego();
             IluminacionManager.instance.RestaurarLuz();
        }

        managerGlobal.instance.actualizarTiempo(tiempoMaximo);
    }
}

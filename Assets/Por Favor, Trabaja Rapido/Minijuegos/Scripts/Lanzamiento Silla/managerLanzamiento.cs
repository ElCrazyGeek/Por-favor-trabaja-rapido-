using Unity.VisualScripting;
using UnityEngine;

public class managerLanzamiento : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;

    [SerializeField] private float posicionMeta;

    [SerializeField] private Transform meta;
     void Start()
    {
        managerGlobal.instance.empezoMinijuego();
        cambiarInterfaces.instance.obtenerPrefab(gameObject);

        posicionMeta = Random.Range(-5,5);
        meta.localPosition = new Vector3(meta.localPosition.x+posicionMeta,meta.localPosition.y,meta.localPosition.z);


        StopAllCoroutines();
        
        StartCoroutine(managerGlobal.instance.mostrarTextoInicial("¡Lanza!"));
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

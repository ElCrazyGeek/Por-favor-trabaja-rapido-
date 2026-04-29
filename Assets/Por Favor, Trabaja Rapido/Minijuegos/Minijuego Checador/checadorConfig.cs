using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class checadorConfig : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
   
    void Start()
    {
        managerGlobal.instance.empezoMinijuego();
        StartCoroutine(mostrarTextoInicial());
    }

    // Update is called once per frame
    void Update()
    {
        tiempoMaximo-=Time.deltaTime;


        if(tiempoMaximo <= 0f)
        {
            managerGlobal.instance.perdioMinijuego();
        }

        managerGlobal.instance.actualizarTiempo(tiempoMaximo);
        
        if(managerGlobal.instance.mostrarTexto)
        {
            managerGlobal.instance.textoInicial("¡Checa!");
        } else
        {
            managerGlobal.instance.ocutarTextoInicial();
        }
        
    }


    IEnumerator mostrarTextoInicial()
    {
         managerGlobal.instance.textoInicial("¡Checa!");
        yield return new WaitForSeconds(1f);
        managerGlobal.instance.ocutarTextoInicial();
    }


    
}

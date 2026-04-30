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
        StopAllCoroutines();
        StartCoroutine(managerGlobal.instance.mostrarTextoInicial("¡Checa!"));
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
        }

        managerGlobal.instance.actualizarTiempo(tiempoMaximo);
        
      
    }


   


    
}

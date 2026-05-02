using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class checadorConfig : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo; 

       [Header ("Archivos de Audio")]
   [SerializeField] private AudioClip sfxDerrota;
   private bool yaSono;

   
   
    void Start()
    {
        managerGlobal.instance.empezoMinijuego();
        cambiarInterfaces.instance.obtenerPrefab(gameObject);

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
            if(!yaSono){
            audioManager.instance.reproducirSFX(sfxDerrota);
            yaSono = true;
            
        }
             managerGlobal.instance.perdioMinijuego();
           
            
        }

        managerGlobal.instance.actualizarTiempo(tiempoMaximo);
        
      
    }


   


    
}

using UnityEngine;
using System;
using Unity.VisualScripting;

public class checadorConfig : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoMaximo-=Time.deltaTime;


        if(tiempoMaximo <= 0f)
        {
            managerGlobal.instance.perdioMinijuego();
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class lanzamientoSilla : MonoBehaviour
{
    [SerializeField] private float fuerza;
    [SerializeField] private int direccion;

    [SerializeField] private GameObject silla;

    [SerializeField] private Rigidbody rbSilla;

    [SerializeField] private Image barraFuerza;

    

    private bool isHolding;

   [SerializeField] private float fuerzaMaxima;
    [SerializeField] private float velocidadAumento;

    [SerializeField] private GameObject canvasMinijuego;

     [SerializeField] private int fallos;
     private bool fallo;
     private bool seComprobo;

    [Header ("Reinicio de la silla")]
    [SerializeField] private bool lanzo;
    private Vector3 posicionInicialSilla;
     [SerializeField] private float velocidad;
     [SerializeField] private float tiempoReinicio;


    
    
    
    void Start()
    {
        direccion =180;
        fuerza=0;
        posicionInicialSilla = silla.transform.position;
        tiempoReinicio=1f;
    }

    // Update is called once per frame
    void Update()
    {
       silla.transform.localRotation = Quaternion.Euler(0, direccion, 0);
       rellenarBarraFuerza();

        if (isHolding)
        {
            aumentarFuerza();
        }

        velocidad = rbSilla.linearVelocity.magnitude;


        if(velocidad<0.7 && lanzo)
        {
            tiempoReinicio-=Time.deltaTime;
        }

        if (tiempoReinicio <= 0)
        {
             silla.transform.position = posicionInicialSilla;
             
            fallos++;
            tiempoReinicio=1f;
            lanzo=false;
        }

        if(fallos == 3)
        {
            managerGlobal.instance.perdioMinijuego();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WinCondition"))
        {
            managerGlobal.instance.ganoMinijuego();
            Debug.Log("Ganaste el minijuego");
            canvasMinijuego.SetActive(false);
            
           
        }
    }


    
    


  

   public void cambiarDireccionDerecha()
    {
        direccion+=5;
        direccion = Mathf.Clamp(direccion, 110, 250);
        Debug.Log("Cambiando direccion a la derecha");
    }
    
    public void cambiarDireccionIzquierda()
    {
        direccion-=5;
        direccion = Mathf.Clamp(direccion, 110, 250);
        Debug.Log("Cambiando direccion a la izquierda");
    }   

    public void lanzarSilla()
    {
       rbSilla.AddForce((silla.transform.forward + silla.transform.up * 0.2f) * fuerza, ForceMode.Impulse);
       lanzo=true;
        
    }

    public void aumentarFuerza()
    {
        fuerza= Mathf.PingPong(Time.time * velocidadAumento, fuerzaMaxima);
        
    }


    void rellenarBarraFuerza()
    {
        barraFuerza.fillAmount = fuerza / fuerzaMaxima;
        barraFuerza.color = Color.Lerp(Color.green, Color.red, fuerza / fuerzaMaxima);
    }

    public void pointerDown()
    {
        isHolding=true;
        
    }


    public void pointerUp()
    {
        isHolding=false;
        lanzarSilla();  
       
    }

  
    


}

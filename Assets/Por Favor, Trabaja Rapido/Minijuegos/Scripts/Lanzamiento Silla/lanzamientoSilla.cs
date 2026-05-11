using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class lanzamientoSilla : MonoBehaviour
{
    [SerializeField] private float fuerza;
    [SerializeField] private float direccion;

    [SerializeField] private GameObject silla;

    [SerializeField] private Rigidbody rbSilla;

    [SerializeField] private Image barraFuerza;

    [SerializeField] private GameObject[] tachaFallos;

    

    private bool isHolding;
    private int holdingState;

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


     [Header ("Archivos de Audio")]

     [SerializeField] private AudioClip sfxSillaRodando;
     [SerializeField] private AudioClip sfxSillaChoco;
     [SerializeField] private AudioClip sfxBotonGirar;
     [SerializeField] private AudioClip sfxBotonEmpujar;

     private float cooldoownSFX=0.1f;
     private float ultimoSonido;

     private bool yaSono;
     


    
    
    
    void Start()
    {
        direccion =180;
        fuerza=0;
        posicionInicialSilla = silla.transform.position;
        tiempoReinicio=1f;
        holdingState=0;

        for(int i = 0; i < tachaFallos.Length; i++)
        {
            tachaFallos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       silla.transform.localRotation = Quaternion.Euler(0, direccion, 0);
       rellenarBarraFuerza();
       

           if (holdingState == 1)
            {
                cambiarDireccionDerecha();
            } else if (holdingState == 2)
            {
                cambiarDireccionIzquierda();
            }

        if (isHolding)
        {
            aumentarFuerza();

        
        }

        velocidad = rbSilla.linearVelocity.magnitude;

        if(lanzo && velocidad > 0.7)
        {
            if (!yaSono)
            {
            audioManager.instance.reproducirSFX(sfxSillaRodando);
                yaSono=true;
            }
        }

        if(velocidad<0.7 && lanzo && managerGlobal.instance.puedeJugar)
        {
            tiempoReinicio-=Time.deltaTime;
        }
        

        if (tiempoReinicio <= 0 && managerGlobal.instance.puedeJugar)
        {
             silla.transform.position = posicionInicialSilla;
             
            fallos++;
            tiempoReinicio=1f;
            lanzo=false;
            yaSono=false;
            direccion=180;
            fuerza=0;
        }

        actualizarFallos();

        if(fallos == 3)
        {
            managerGlobal.instance.perdioMinijuego();
            canvasMinijuego.SetActive(false);
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


    void OnCollisionEnter(Collision collision)
    {

        if(Time.time >= ultimoSonido + cooldoownSFX)
        {
        audioManager.instance.reproducirSFX(sfxSillaChoco);
            ultimoSonido=Time.time;
        }
    }

    private void actualizarFallos()
    {
        if (fallos > 0)
        {
            tachaFallos[0].SetActive(true);
        } 

         if(fallos > 1)
        {
            tachaFallos[1].SetActive(true);
        } 
        
         if(fallos > 2)
        {
            tachaFallos[2].SetActive(true);
        }
    }








    public void cambiarDireccionDerecha()
    {
        direccion+=0.8f;
        direccion = Mathf.Clamp(direccion, 110, 250);
        Debug.Log("Cambiando direccion a la derecha");
    }
    
    public void cambiarDireccionIzquierda()
    {
        direccion-=0.8f;
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

        audioManager.instance.reproducirSFX(sfxBotonGirar);
       
    }

    public void girandoDerecha()
    {
        holdingState=1;
    }

    public void girandoIzquierda()
    {   
     holdingState=2;
    }

    public void sinGirar()
    {
        holdingState=0;
        audioManager.instance.reproducirSFX(sfxBotonGirar);
    }

  
    


}

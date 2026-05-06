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



    
    
    
    void Start()
    {
        direccion =180;
        fuerza=0;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WinCondition"))
        {
            managerGlobal.instance.ganoMinijuego();
            Debug.Log("Ganaste el minijuego");
        }
    }
    


  

   public void cambiarDireccionDerecha()
    {
        direccion+=10;
        direccion = Mathf.Clamp(direccion, 110, 250);
        Debug.Log("Cambiando direccion a la derecha");
    }
    
    public void cambiarDireccionIzquierda()
    {
        direccion-=10;
        direccion = Mathf.Clamp(direccion, 110, 250);
        Debug.Log("Cambiando direccion a la izquierda");
    }   

    public void lanzarSilla()
    {
       rbSilla.AddForce((silla.transform.forward + silla.transform.up * 0.2f) * fuerza, ForceMode.Impulse);
    
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
    }

  
    


}

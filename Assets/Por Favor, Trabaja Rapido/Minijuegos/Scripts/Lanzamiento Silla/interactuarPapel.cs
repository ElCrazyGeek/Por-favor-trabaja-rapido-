using UnityEngine;

public class interactuarPapel : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnHold()
    {
        
    }

     public void OnClick()
    {
        if (managerGlobal.instance.puedeJugar)
        {
        managerGlobal.instance.ganoMinijuego();
        }

        if (IluminacionManager.instance.apagado)
        {
        IluminacionManager.instance.RestaurarLuz();
        }


        Debug.Log("Se hizo clic");
    }

     public void OnCancel()
    {
        
    }
}

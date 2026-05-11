
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class movimientoLampara : MonoBehaviour, IInteractable
{
    [SerializeField] private float velocidadArrastre; 
    [SerializeField] private Transform camara;
    [SerializeField] private float sensibilidad;
    [SerializeField] private Vector2 limitesX= new Vector2(-10f,10f);
    [SerializeField] private Vector2 limitesZ= new Vector2(-10f,10f);
    private Vector2 deltaMouse;
    [SerializeField] private bool estaArrastrando;
    [SerializeField] private InputActionReference mouseDeltaAction;
    private Vector3 posicionObjetivo;
    private bool primerArrastre;

    [SerializeField] private Collider superficieCollider;

    private Vector3 posicionInicial;

    private Vector2 mouseAnterior;

    
    void Start()
    {
        //Debug.Log(camara.name);
        estaArrastrando = false;
        posicionObjetivo= camara.position;
        posicionInicial=camara.position;
    }

    // Update is called once per frame
    void Update()
    {
      if(!estaArrastrando) return;

        if (estaArrastrando)
        {
      //Debug.Log("Arrastrando");
            
        }

      Vector2 mouseActual = Mouse.current.position.ReadValue();

      Vector2 deltaMouse = mouseActual - mouseAnterior;
      //Debug.Log("Delta: "+deltaMouse);
      /*Vector2 deltaCalculado = mouseActual-mouseAnterior;

      if (deltaCalculado.magnitude > 0) {
        Debug.Log($"[MOUSE] Delta: {deltaCalculado} | PosActual: {mouseActual}");
    }*/

      mouseAnterior = mouseActual;

      posicionObjetivo.x-= deltaMouse.x * sensibilidad;
      posicionObjetivo.z-= deltaMouse.y * sensibilidad;

      //posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x,limitesX.x,limitesX.y);
      //posicionObjetivo.z = Mathf.Clamp(posicionObjetivo.z,limitesZ.x,limitesZ.y);

      Bounds bounds=superficieCollider.bounds;

      posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x,bounds.min.x,bounds.max.x);
      posicionObjetivo.z = Mathf.Clamp(posicionObjetivo.z,bounds.min.z,bounds.max.z);

        //Debug.Log("Antes:   "+camara.position);
      camara.position = posicionObjetivo;
        //Debug.Log("Despues:   "+camara.position);
            //camara.position= Vector3.Lerp(camara.position,posicionObjetivo,velocidadArrastre*Time.deltaTime);
        
    }

    public void OnClick(){}
    public void OnHold()
    {
             Debug.Log("Inicio Hold");
        Debug.Log("Camara: " + camara.position);
        Debug.Log("Objetivo:    "+ posicionObjetivo);
        
        estaArrastrando = true;
        mouseAnterior = Mouse.current.position.ReadValue();

        posicionObjetivo=camara.position;

    }

    public void OnCancel()
    {
        estaArrastrando = false;
        posicionObjetivo = camara.position;
    }
}
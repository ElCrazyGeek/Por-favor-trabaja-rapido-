using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    [SerializeField] private Camera camara;
    [SerializeField] private LayerMask Interaccion;

    [SerializeField] private float tiempoHold;

    private bool isHolding;

    private float inicioClick;
    private bool manteniendoClick;
    private IInteractable objetoActual; 

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inicioClick = Time.time;
            manteniendoClick = true;
            isHolding = false;
            
            objetoActual = ObtenerInteractuable();

            if(objetoActual == null)
            {
                manteniendoClick=false; 
            }
        }

        if (context.canceled)
        {
            float duracionClick = Time.time - inicioClick;
            manteniendoClick = false;

            if (objetoActual != null)
            {

                 if(!isHolding)
                {          
                    objetoActual.OnClick();
                } 
                else
                {
                    objetoActual.OnCancel();
                }
            }

            objetoActual = null; 
        }
    }

    void Update()
    {
        if (manteniendoClick && objetoActual != null)
        {  
            float tiempoActual = Time.time - inicioClick;

            if(tiempoActual >= tiempoHold)
            {
                isHolding = true; 
            }
            objetoActual.OnHold();
        }
    }

    private IInteractable ObtenerInteractuable()
{
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Ray ray = camara.ScreenPointToRay(mousePos);

    if (Physics.Raycast(ray, out RaycastHit hit, 100f, Interaccion))
    {
        Debug.Log("Raycast chocó con: " + hit.collider.gameObject.name); // <--- AGREGA ESTO
        return hit.collider.GetComponent<IInteractable>();
    }
    
    Debug.Log("El Raycast no chocó con nada en la capa Interaccion"); // <--- AGREGA ESTO
    return null;
}
}
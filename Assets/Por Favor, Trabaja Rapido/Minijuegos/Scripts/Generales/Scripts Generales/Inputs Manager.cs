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
        
        if(objetoActual == null) manteniendoClick = false;
    }

    
    if (context.canceled)
    {
        if (objetoActual != null)
        {
            if (!isHolding)
            {
                
                Debug.Log("Click detectado!");
                objetoActual.OnClick();
            }
            else
            {
                
                Debug.Log("Hold terminado/cancelado");
                objetoActual.OnCancel();
            }
        }

      
        manteniendoClick = false;
        isHolding = false;
        objetoActual = null;
    }
}

    void Update()
    {
        if (manteniendoClick && objetoActual != null)
        {  
            float tiempoActual = Time.time - inicioClick;

            if(tiempoActual >= tiempoHold && !isHolding)
            {
                isHolding = true; 
            objetoActual.OnHold();
            }
        }
    }

    private IInteractable ObtenerInteractuable()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = camara.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 30f, Interaccion))
        {
            MonoBehaviour[] componentes = hit.collider.GetComponentsInParent<MonoBehaviour>();

            foreach(MonoBehaviour comp in componentes)
            {
                if(comp is IInteractable interactuable)
                {
                    return interactuable;
                }
            }

            
        }
        return null;
    }
}
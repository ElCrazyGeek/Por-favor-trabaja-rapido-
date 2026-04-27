using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    [SerializeField] private Camera camara;
    [SerializeField] private LayerMask Interaccion;

    private float inicioClick;
    private bool manteniendoClick;
    private IInteractable objetoActual; 

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inicioClick = Time.time;
            manteniendoClick = true;
            
            objetoActual = ObtenerInteractuable();
        }

        if (context.canceled)
        {
            float duracionClick = Time.time - inicioClick;
            manteniendoClick = false;

            if (objetoActual != null && duracionClick < 0.3f)
            {
                objetoActual.OnClick();
            }

            objetoActual = null; 
        }
    }

    void Update()
    {
        if (manteniendoClick && objetoActual != null)
        {  
            objetoActual.OnHold();
        }
    }

    private IInteractable ObtenerInteractuable()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = camara.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 30f, Interaccion))
        {
            return hit.collider.GetComponent<IInteractable>();
        }
        return null;
    }
}
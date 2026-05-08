using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public bool isPressed {get; private set;}
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPressed = true;
            Debug.Log("Presionado");
        }
        if (context.canceled)
        {
            isPressed = false;
            Debug.Log("NAO NAO");
        }
    }
}

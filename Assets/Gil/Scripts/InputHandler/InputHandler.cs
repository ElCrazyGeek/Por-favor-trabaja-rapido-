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
            Logger.Instance.Log("Presionado", this);
        }
        if (context.canceled)
        {
            isPressed = false;
            Logger.Instance.Log("NAO NAOOO", this);
        }
    }
}

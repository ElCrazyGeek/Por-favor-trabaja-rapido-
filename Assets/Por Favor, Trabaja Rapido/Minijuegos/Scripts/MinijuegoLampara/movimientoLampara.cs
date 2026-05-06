
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientoLampara : MonoBehaviour, IInteractable
{
    [SerializeField] private float velocidadArrastre; 
    [SerializeField] private float sensibilidad;

    [SerializeField] private Vector2 limitesX= new Vector2(-10f,10f);

    private Vector2 ultimaPosicionMouse;
    [SerializeField] private bool estaArrastrando;

    [SerializeField] private PlayerInput playerInput;
    void Start()
    {
        estaArrastrando = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estaArrastrando)
        {
            Vector2 posicionMouseActual = Mouse.current.position.ReadValue();
            float diferenciaX = posicionMouseActual.x - ultimaPosicionMouse.x;
            float nuevoX = transform.position.x + (diferenciaX * sensibilidad * Time.deltaTime);
            nuevoX = Mathf.Clamp(nuevoX, limitesX.x, limitesX.y);
            float xSuave = Mathf.Lerp(transform.localPosition.x, nuevoX, Time.deltaTime * 10f);
            transform.position = new Vector3(xSuave, transform.localPosition.y, transform.localPosition.z);
            ultimaPosicionMouse = posicionMouseActual;
        }

    }

    void activarArrastre()
    {
       
    }

    public void OnClick(){}

    public void OnHold()
    {
        estaArrastrando = true;
        ultimaPosicionMouse = Mouse.current.position.ReadValue();
    }

    public void OnCancel()
    {
        estaArrastrando = false;
    }
}

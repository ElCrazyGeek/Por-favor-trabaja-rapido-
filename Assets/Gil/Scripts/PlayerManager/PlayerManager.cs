using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler input;
    private LogicaTachuela logicaTachuela;

    void Awake()
    {
        input = GetComponent<InputHandler>();
        logicaTachuela = GetComponent<LogicaTachuela>();
    }

    // Update is called once per frame
    void Update()
    {
        logicaTachuela.CargarTachuela(input.isPressed);
    }
}

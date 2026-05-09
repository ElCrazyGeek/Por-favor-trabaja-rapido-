using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler input;
    private LogicaTachuela logicaTachuela;
    private UILogic ui;

    void Awake()
    {
        input = GetComponent<InputHandler>();
        logicaTachuela = GetComponent<LogicaTachuela>();
        ui = GameObject.Find("UILogic").GetComponent<UILogic>();
    }

    // Update is called once per frame
    void Update()
    {
        logicaTachuela.CargarTachuela(input.isPressed);
        ui.UICargarBarra(logicaTachuela.actForce, logicaTachuela.minForce, logicaTachuela.maxForce);
    }
}

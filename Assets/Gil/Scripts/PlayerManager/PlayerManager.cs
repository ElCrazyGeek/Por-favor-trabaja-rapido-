using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler input;
    private LogicaTachuela logicaTachuela;
    private UILogic ui;
    private TrayectoriaDardo trayectoria;
    void Awake()
    {
        input = GetComponent<InputHandler>();
        logicaTachuela = GetComponent<LogicaTachuela>();
        ui = GameObject.Find("UILogic").GetComponent<UILogic>();
        trayectoria = GetComponent<TrayectoriaDardo>();
    }

    // Update is called once per frame
    void Update()
    {
        logicaTachuela.CargarTachuela(input.isPressed);
        trayectoria.DibujarTrayectoria(logicaTachuela.spawnTachuela.right * logicaTachuela.actForce, logicaTachuela.spawnTachuela);
        ui.UICargarBarra(logicaTachuela.actForce, logicaTachuela.minForce, logicaTachuela.maxForce);
    }
}

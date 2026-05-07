using UnityEngine;

public class Lanzador : MonoBehaviour , IInteractable
{
    public void OnClick() {
        // En este sistema, OnClick podría ser un tiro rápido con fuerza mínima
        Dardos_Manager.instance.IniciarCarga();
        Dardos_Manager.instance.Lanzar();
    }

    public void OnHold() {
        Dardos_Manager.instance.IniciarCarga();
    }

    public void OnCancel() {
        Dardos_Manager.instance.Lanzar();
    }
}
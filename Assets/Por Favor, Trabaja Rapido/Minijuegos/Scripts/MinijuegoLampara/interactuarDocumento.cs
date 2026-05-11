using UnityEngine;

public class interactuarDocumento : MonoBehaviour, IInteractable
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        managerGlobal.instance.ganoMinijuego();
    }

    public void OnHold(){}
    public void OnCancel(){}
}

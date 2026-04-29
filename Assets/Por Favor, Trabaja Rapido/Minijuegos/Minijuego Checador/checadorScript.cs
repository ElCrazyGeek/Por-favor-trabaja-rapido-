using UnityEngine;

public class checadorScript : MonoBehaviour, IInteractable
{
   [SerializeField] private float tiempoMantenido;
   [SerializeField] private float tiempoMaximo;

   [SerializeField] private bool chequeado;
   [SerializeField] private bool estaManteniendo;

   private Material materialInstancia;
    void Start()
    {
        materialInstancia = GetComponent<Renderer>().material;
        materialInstancia.SetFloat("_isActive", 1f);
        chequeado = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(tiempoMantenido >= tiempoMaximo && !chequeado)
        {
            materialInstancia.SetFloat("_isActive", 0f);
            Debug.Log("Checado");
            managerGlobal.instance.ganoMinijuego();
            chequeado = true;
        }

        
        

        if (estaManteniendo && !chequeado)
        {
            tiempoMantenido += Time.deltaTime;
        } else
        {
            tiempoMantenido-=Time.deltaTime;
        }

        tiempoMantenido= Mathf.Clamp(tiempoMantenido,0f,tiempoMaximo);

        
    }


    public void OnClick() {}

    public void OnHold()
    {
      estaManteniendo=true;
    }

    public void OnCancel()
    {
        estaManteniendo=false;
    }
}

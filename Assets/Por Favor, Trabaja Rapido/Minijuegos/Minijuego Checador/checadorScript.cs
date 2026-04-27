using UnityEngine;

public class checadorScript : MonoBehaviour, IInteractable
{
   [SerializeField] private float tiempoMantenido;
   [SerializeField] private float tiempoMaximo;

   [SerializeField] private bool chequeado;

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
        if(tiempoMantenido > tiempoMaximo && !chequeado)
        {
            materialInstancia.SetFloat("_isActive", 0f);
            Debug.Log("Checado");
            chequeado = true;
        }
    }


    public void OnClick() {}

    public void OnHold()
    {
        tiempoMantenido += Time.deltaTime;
    }
}

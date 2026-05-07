using UnityEngine;

public class checadorScript : MonoBehaviour, IInteractable
{
   [SerializeField] private float tiempoMantenido;
   [SerializeField] private float tiempoMaximo;

   [SerializeField] private bool chequeado;
   [SerializeField] private bool estaManteniendo;

   private Material materialInstancia;

   [Header ("Archivos de Audio")]
   [SerializeField] private AudioClip sfxFeedback;
   [SerializeField] private AudioClip sfxCargando;
   [SerializeField] private AudioClip sfxDescargando;
   [SerializeField] private AudioClip sfxVictoria;

   enum estadoAudio {Ninguno, Cargando, Descargando};
    private estadoAudio estadoActualAudio = estadoAudio.Ninguno;
   
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
            audioManager.instance.detenerLoop(sfxCargando);
            audioManager.instance.detenerLoop(sfxDescargando);
            audioManager.instance.reproducirSFX(sfxVictoria);
            chequeado = true;
        }

    
        

        

        if (estaManteniendo && !chequeado && managerGlobal.instance.puedeJugar)
        {
            tiempoMantenido += Time.deltaTime;
                
             
        } else
        {
            tiempoMantenido-=Time.deltaTime;

             
        }

        tiempoMantenido= Mathf.Clamp(tiempoMantenido,0f,tiempoMaximo);

    if(!managerGlobal.instance.puedeJugar || chequeado)
        {
            CambiarAudio(estadoAudio.Ninguno);
        } else if (estaManteniendo && !chequeado)
        {
            CambiarAudio(estadoAudio.Cargando);
        
        } else if (tiempoMantenido > 0.05f)
        {
            CambiarAudio(estadoAudio.Descargando);
        } else
        {
            CambiarAudio(estadoAudio.Ninguno);
        }
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

    void CambiarAudio(estadoAudio nuevoEstado)
{
    if (estadoActualAudio == nuevoEstado) return;

    estadoActualAudio = nuevoEstado;

    audioManager.instance.detenerLoop(sfxCargando);
    audioManager.instance.detenerLoop(sfxDescargando);

    if (nuevoEstado == estadoAudio.Cargando)
        audioManager.instance.reproducirLoop(sfxCargando);

    else if (nuevoEstado == estadoAudio.Descargando)
        audioManager.instance.reproducirLoop(sfxDescargando);
}
}

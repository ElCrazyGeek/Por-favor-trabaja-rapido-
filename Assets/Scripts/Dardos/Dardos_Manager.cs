using UnityEngine;
using Unity.Cinemachine;

public class Dardos_Manager : MonoBehaviour, IInteractable
{
    public static Dardos_Manager instance;

    [Header("Configuración")]
    public GameObject prefabDardo;
    public Transform spawnPoint; 
    public Transform centroDiana; 

    [Header("Ajustes de Lanzamiento")]
    public float fuerzaMinima = 5f;
    public float fuerzaMaxima = 25f;
    public float tiempoCargaMaximo = 2f; 
    
    private float tiempoPresionado;
    private bool cargando = false;

    [Header("Cámaras")]
    // ¡ESTA LÍNEA FALTABA! Por eso te marcaba error abajo
    public CinemachineCamera camaraDardos; 
    public int prioridadActiva = 20;
    public int prioridadInactiva = 5;

    void Awake() { 
        instance = this; 
    }

    // --- MÉTODOS DE LA INTERFAZ DE TU AMIGO ---
    public void OnClick() {
        // Un click rápido lanza con fuerza mínima
        IniciarCarga();
        Lanzar();
    }

    public void OnHold() {
        if (!cargando) IniciarCarga();
    }

    public void OnCancel() {
        Lanzar();
    }

    // --- LÓGICA DEL JUEGO ---
    public void IniciarCarga() {
        cargando = true;
        tiempoPresionado = 0f;
        ActivarMinijuego(); // Cambia la cámara al empezar a cargar
    }

   public void Lanzar() {
    // ... tu código actual ...
    GameObject dardoGO = Instantiate(prefabDardo, spawnPoint.position, spawnPoint.rotation);
    // Dibuja una línea por 5 segundos para ver hacia dónde salió
    Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 20f, Color.red, 5f);

        if (!cargando) return;
        
        cargando = false;
        float porcentajeCarga = Mathf.Clamp01(tiempoPresionado / tiempoCargaMaximo);
        float fuerzaFinal = Mathf.Lerp(fuerzaMinima, fuerzaMaxima, porcentajeCarga);

        Rigidbody rbDardo = dardoGO.GetComponent<Rigidbody>();
        
        if(rbDardo != null) {
            rbDardo.isKinematic = false;
            rbDardo.AddForce(spawnPoint.forward * fuerzaFinal, ForceMode.Impulse);
        }
    }

    void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
        Debug.Log("Espacio presionado: Forzando lanzamiento");
        IniciarCarga();
        Lanzar();
    }
    
    if (cargando) {
        tiempoPresionado += Time.deltaTime;
    }
}

    public void CalcularPuntuacion(Vector3 puntoImpacto) {
        float distancia = Vector3.Distance(puntoImpacto, centroDiana.position);
        
        if (distancia < 0.1f) Debug.Log("¡BULLSEYE! 100 PUNTOS");
        else if (distancia < 0.3f) Debug.Log("Cerca: 50 PUNTOS");
        else Debug.Log("En la orilla: 10 PUNTOS");

        // Opcional: Desactivar cámara después de un rato o al ganar
        // Invoke("DesactivarMinijuego", 2f);
    }

    public void ActivarMinijuego() {
        if(camaraDardos != null) camaraDardos.Priority = prioridadActiva;
    }

    public void DesactivarMinijuego() {
        if(camaraDardos != null) camaraDardos.Priority = prioridadInactiva;
    }
}
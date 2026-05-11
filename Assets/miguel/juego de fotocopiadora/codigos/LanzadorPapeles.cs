using UnityEngine;

public class LanzadorPapeles : MonoBehaviour
{
    [Header("Configuración de Prefabs")]
    public GameObject papelVerdePrefab;
    public GameObject papelMaloPrefab;
    public GameObject papelDecorativoPrefab; 
    public Transform puntoSalida;

    [Header("Ajustes de Física")]
    public float fuerzaLanzamiento = 6f; // Prueba con 6, es más controlable
    public float intervaloLanzamiento = 1.2f;

    private GamePapeles gameManager;

    void Start()
    {
        gameManager = Object.FindAnyObjectByType<GamePapeles>();
        InvokeRepeating("LanzarPapelAleatorio", 1f, intervaloLanzamiento);
    }

    void LanzarPapelAleatorio()
    {
        if (gameManager != null && gameManager.juegoTerminado)
        {
            CancelInvoke("LanzarPapelAleatorio");
            return;
        }

        // --- LÓGICA DE PROBABILIDADES ---
        float suerte = Random.value;
        GameObject prefabAUsar;

        if (suerte < 0.15f)      prefabAUsar = papelVerdePrefab;       // 10% Verde
        else if (suerte < 0.40f) prefabAUsar = papelMaloPrefab;        // 10% Malo (Rojo)
        else                     prefabAUsar = papelDecorativoPrefab;  // 80% Decoración

        // --- INSTANCIACIÓN ---
        // Forzamos que nazca exactamente en la posición del puntoSalida
        GameObject papel = Instantiate(prefabAUsar, puntoSalida.position, puntoSalida.rotation);
        
        Rigidbody rb = papel.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            // 1. Limpiamos cualquier fuerza rara que traiga el prefab
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 2. DIRECCIÓN DE SALIDA (Hacia adelante y hacia arriba)
            // Usamos 'up' global para que siempre suban un poco antes de caer
            Vector3 direccionFinal = (puntoSalida.forward * 0.5f + Vector3.up).normalized;

            // 3. FUERZA Y TORQUE (Giro)
            rb.AddForce(direccionFinal * fuerzaLanzamiento, ForceMode.Impulse);
            
            // Un giro aleatorio pero suave
            Vector3 giro = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddTorque(giro * 5f, ForceMode.Impulse);
        }
        
        Destroy(papel, 0.8f); 
    }
}
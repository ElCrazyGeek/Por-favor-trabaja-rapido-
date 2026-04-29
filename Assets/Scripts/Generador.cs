using UnityEngine;
using System.Collections;

public class ObstaculoSpawner : MonoBehaviour
{
    public GameObject prefabObjetivo;

    [Header("Ritmo de Aparición")]
    public float tiempoMinInicial = 1.2f;
    public float tiempoMaxInicial = 2.0f;
    public float tiempoMinFinal = 0.5f;
    public float tiempoMaxFinal = 0.8f;

    [Header("Velocidad de Movimiento")]
    public float velInicial = 6f;
    public float velFinal = 12f; // El doble de rápido al final
    public static float velocidadGlobal; // Variable estática para que los cubos la lean

    [Header("Configuración")]
    public float limiteY = 3.5f;
    public float posicionXSpawn = 15f;
    public float duracionTotalJuego = 20f;

    private float tiempoTranscurrido = 0f;

    void Start()
    {
        velocidadGlobal = velInicial; // Resetear al empezar
        StartCoroutine(SpawnRutina());
    }

    IEnumerator SpawnRutina()
    {
        yield return new WaitForSeconds(1f);

        while (tiempoTranscurrido < duracionTotalJuego)
        {
            SpawnearCubo();

            // Calculamos el progreso (0 a 1)
            float progreso = Mathf.Clamp01(tiempoTranscurrido / duracionTotalJuego);

            // AUMENTO DE VELOCIDAD: Actualizamos la variable estática
            velocidadGlobal = Mathf.Lerp(velInicial, velFinal, progreso);

            // AUMENTO DE RITMO: Calculamos la espera para el siguiente
            float minActual = Mathf.Lerp(tiempoMinInicial, tiempoMinFinal, progreso);
            float maxActual = Mathf.Lerp(tiempoMaxInicial, tiempoMaxFinal, progreso);
            float esperaAleatoria = Random.Range(minActual, maxActual);
            
            tiempoTranscurrido += esperaAleatoria; 
            yield return new WaitForSeconds(esperaAleatoria);
        }
    }

    void SpawnearCubo()
    {
        float alturaAleatoria = Random.Range(-limiteY, limiteY);
        Vector3 posicion = new Vector3(posicionXSpawn, alturaAleatoria, transform.position.z);
        Instantiate(prefabObjetivo, posicion, Quaternion.identity);
    }
}
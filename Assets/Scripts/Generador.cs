using UnityEngine;

public class ObstaculoSpawner : MonoBehaviour
{
    public GameObject prefabObjetivo;
    public float tiempoEntreSpawn = 1.5f;
    public float rangoY = 0f;
    public float posicionXSpawn = 15f; 
    

    void Start()
    {
        InvokeRepeating("SpawnearCubo", 1f, tiempoEntreSpawn);
    }

    void SpawnearCubo()
    {
        rangoY = Random.Range(1.5f, 3f); 
        float alturaAleatoria = Random.Range(-rangoY, rangoY);
        Vector3 posicion = new Vector3(posicionXSpawn, alturaAleatoria, transform.position.z);
        Instantiate(prefabObjetivo, posicion, Quaternion.identity);
    }
}
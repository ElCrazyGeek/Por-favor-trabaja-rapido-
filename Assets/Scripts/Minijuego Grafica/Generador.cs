using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class ObstaculoSpawner : MonoBehaviour
{
    public RectTransform contenedor;
    public GameObject prefab;

    public float tiempoMinInicial = 1.2f;
    public float tiempoMaxInicial = 2f;
    public float tiempoMinFinal = 0.5f;
    public float tiempoMaxFinal = 0.8f;
    public float velocidadInicial = 400f;
    public float velocidadFinal = 800f;

    public float duracion = 20f;

    float tiempo;

    void Start()
    {
        StartCoroutine(Rutina());
    }

    IEnumerator Rutina()
    {
        yield return new WaitForSeconds(1f);

        while (tiempo < duracion)
        {
            float progreso = tiempo / duracion;

            float espera = Random.Range(
                Mathf.Lerp(tiempoMinInicial, tiempoMinFinal, progreso),
                Mathf.Lerp(tiempoMaxInicial, tiempoMaxFinal, progreso)
            );

            Spawn(progreso);

            tiempo += espera;
            yield return new WaitForSeconds(espera);
        }
    }

    void Spawn(float progreso)
    {
    GameObject obj = Instantiate(prefab, contenedor);
    RectTransform rt = obj.GetComponent<RectTransform>();

    float alto = contenedor.rect.height;

    float y = Random.Range(-120f, 220f);    
    rt.anchoredPosition = new Vector2(950f, y);

    float vel = Mathf.Lerp(velocidadInicial, velocidadFinal, progreso);

    if (obj.TryGetComponent(out ObstaculoVentas obstaculo))
    {
        obstaculo.velocidad = vel;
    }
}
}
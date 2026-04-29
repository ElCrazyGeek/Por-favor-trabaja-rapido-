using UnityEngine;

public class ObstaculoVentas : MonoBehaviour
{
    public float velocidad = 6f;
    public bool fueAtravesado = false;
    public float limiteFalloX = -5f; // Posición X detrás del jugador

    void Update()
    {
       transform.Translate(Vector3.left * ObstaculoSpawner.velocidadGlobal * Time.deltaTime);

        if (!fueAtravesado && transform.position.x < limiteFalloX)
        {
            fueAtravesado = true;
            VentasManager.Instance.ModificarVentas(-10f);
            if(GetComponent<Renderer>() != null)
                GetComponent<Renderer>().material.color = Color.red;
        }

        if (transform.position.x < -25f) Destroy(gameObject);
    }
}
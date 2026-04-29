using UnityEngine;

public class ObstaculoVentas : MonoBehaviour
{
    public float velocidad = 6f;
    public bool fueAtravesado = false;
    public float limiteFalloX = -5f; // Posición X detrás del jugador

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        // Si el jugador NO lo tocó y el cubo ya pasó de largo
        if (!fueAtravesado && transform.position.x < limiteFalloX)
        {
            fueAtravesado = true;
            VentasManager.Instance.ModificarVentas(-10f);
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (transform.position.x < -20f) Destroy(gameObject);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class rastro : MonoBehaviour
{
    public TrailRenderer trail;
    public RectTransform jugador;

    void Update()
    {
        // El rastro debe seguir la posición del jugador en el mundo para no desfasarse
        // Esto asegura que la línea se dibuje exactamente donde está el cuadrito verde
        transform.position = jugador.position;
    }

    // Función útil para tu Manager: Limpia la línea si el jugador pierde
    public void LimpiarGrafica()
    {
        trail.Clear();
    }
}
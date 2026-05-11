using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    
    public IEnumerator Shake(float duracion, float magnitud)
    {
        Vector3 posicionOriginal = transform.localPosition;
        float tiempoPasado = 0.0f;

        while (tiempoPasado < duracion)
        {
            float x = Random.Range(-1f, 1f) * magnitud;
            float y = Random.Range(-1f, 1f) * magnitud;

            transform.localPosition = new Vector3(x, y, posicionOriginal.z);
            tiempoPasado += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = posicionOriginal;
    }
}
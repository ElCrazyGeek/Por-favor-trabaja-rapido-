using UnityEngine;

public class TrayectoriaDardo : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int cantPuntos = 30;
    [SerializeField] private float tmpPuntos = 0.1f;

    [SerializeField] private float s_Width = 0.2f;
    [SerializeField] private float end_Width = 0.1f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = s_Width;
        lineRenderer.endWidth = end_Width;
    }

    public void DibujarTrayectoria(Vector3 fuerza, Transform puntoSalida)
    {
        lineRenderer.positionCount = cantPuntos;

        Vector3 posInicial = puntoSalida.position;

        for (int i = 0; i < cantPuntos; i++)
        {
            float tiempo = i * tmpPuntos;

            Vector3 pos = posInicial + (fuerza * tiempo) + (0.5f * Physics.gravity * tiempo * tiempo);

            lineRenderer.SetPosition(i,pos);
        }
    }

    public void LimpiarTrayectoria()
    {
        lineRenderer.positionCount = 0;
    }
}



using System.Collections.Generic;
using UnityEngine;

public class rastro : MonoBehaviour
{
    public LineRenderer line;
    public float distanciaMinima = 0.1f;

    private List<Vector3> puntos = new List<Vector3>();

    void Start()
    {
       line.positionCount = 1;
        line.SetPosition(0, transform.position);
    }

    void Update()
    {
        if (puntos.Count == 0)
        {
            AgregarPunto();
            return;
        }

        float distancia = Vector3.Distance(transform.position, puntos[puntos.Count - 1]);

        if (distancia > distanciaMinima)
        {
            AgregarPunto();
        }
    }

    void AgregarPunto()
    {
        puntos.Add(transform.position);
        line.positionCount = puntos.Count;
        line.SetPositions(puntos.ToArray());
    }
}
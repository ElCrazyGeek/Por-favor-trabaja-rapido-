using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Sellos_Manager : MonoBehaviour
{
    public GameObject prefabAprobado; // Prefab de texto que dice APROBADO
    public GameObject prefabRechazado; // Prefab de texto que dice RECHAZADO

    void Update()
    {
        // Clic Izquierdo = Aprobar
        if (Input.GetMouseButtonDown(0)) 
        {
            Sellar(true);
        }
        // Clic Derecho = Rechazar
        else if (Input.GetMouseButtonDown(1)) 
        {
            Sellar(false);
        }
    }

    void Sellar(bool intentandoAprobar)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultados);

        foreach (var hit in resultados)
        {
            if (hit.gameObject.CompareTag("Documento"))
            {
                var doc = hit.gameObject.GetComponent<Documentos>();
                
                // Elegimos qué sello poner
                GameObject prefabUsar = intentandoAprobar ? prefabAprobado : prefabRechazado;
                
                // Instanciamos el sello visual
                GameObject marca = Instantiate(prefabUsar, hit.gameObject.transform);
                marca.transform.position = Input.mousePosition;

                // Lógica de acierto/error (puedes añadir puntos aquí después)
                if (intentandoAprobar == doc.Valido)
                {
                    Debug.Log("¡Acción correcta!");
                }
                else
                {
                    Debug.Log("¡Error de juicio!");
                }

                // QUITAMOS EL DOCUMENTO
                // En lugar de un Destroy inmediato, podemos llamar a una función que lo anime o lo saque
                Destroy(hit.gameObject, 0.5f); // Se destruye tras medio segundo para que se vea el sello puesto
                
                break; 
            }
        }
    }
}
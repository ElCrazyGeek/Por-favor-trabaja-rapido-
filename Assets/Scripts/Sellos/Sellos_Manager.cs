using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Sellos_Manager : MonoBehaviour
{
    [Header("Prefabs de Sello Visual")]
    public GameObject prefabAprobado;
    public GameObject prefabRechazado;

    public GameManager_Sellos gameManager;
    



    void Update()
    {
        // Clic Izquierdo = Intentar Aprobar
        if (Input.GetMouseButtonDown(0)) 
        {
            Sellar(true);
        }
        // Clic Derecho = Intentar Rechazar
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
                Documentos doc = hit.gameObject.GetComponent<Documentos>();

                // 1. REGLA ESPECIAL: ¿Es un documento de Despido y lo aprobaste?
                if (doc.tipo == Documentos.TipoDocumento.Despido && intentandoAprobar)
                {
                    gameManager.FinDelJuego("DESPEDIDO");
                    return;
                }

                // 2. REPORTAR AL GAME MANAGER
                // Si intentas aprobar algo que es válido, o rechazar algo inválido, es acierto.
                bool fueCorrecto = (intentandoAprobar == doc.Valido);
                gameManager.RegistrarAcierto(fueCorrecto);

                // 3. EFECTO VISUAL (Estampado)
                GameObject prefabUsar = intentandoAprobar ? prefabAprobado : prefabRechazado;
                GameObject marca = Instantiate(prefabUsar, hit.gameObject.transform);
                marca.transform.position = Input.mousePosition;
                
                // Rotación aleatoria para que se vea más real
                marca.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));

                // 4. ELIMINAR DOCUMENTO
                doc.SerRecogido();
                
                break; // Solo sellamos el primer documento que toque el raycast
            }
        }
    }
}
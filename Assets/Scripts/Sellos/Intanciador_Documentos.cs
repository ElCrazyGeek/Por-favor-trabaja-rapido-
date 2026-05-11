using UnityEngine;

public class Documento_Spawner : MonoBehaviour
{
    public GameObject documentoPrefab; // Arrastra tu Prefab aquí
    public Transform contenedorUI;    // El objeto "Sellos" o el Panel donde deben vivir
    public int cantidadInicial = 5;

    void Start()
    {
        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarDocumento();
        }
    }

    public void GenerarDocumento()
    {
        // Instancia el prefab como hijo del contenedor de UI
        GameObject nuevoDoc = Instantiate(documentoPrefab, contenedorUI);
        
        // Les damos una posición aleatoria dentro de un rango para que no se encimen todos
        RectTransform rt = nuevoDoc.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(Random.Range(-300f, 300f), Random.Range(-200f, 200f));
    }
    public void GenerarNuevaOleada(int nuevaCantidad)
{
    // Limpieza opcional: si quieres que la mesa se vacíe antes de la nueva carga
    // (Opcional, si quieres pánico real, deja los que ya estaban)
    
    for (int i = 0; i < nuevaCantidad; i++)
    {
        GenerarDocumento();
    }
}
}
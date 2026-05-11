
using Unity.VisualScripting;
using UnityEngine;

public class lamparaScript : MonoBehaviour
{
    [SerializeField] private GameObject[] randomProps;
    [SerializeField] private Collider colliderSuperficie;

   
    [SerializeField] private GameObject documento;
    

    [SerializeField] private int cantidadProps;
    private bool eligioPosicion;
     GameObject prefabElegido;

    void Start()
    {
        cantidadProps=Random.Range(15,20);

        Bounds bounds = colliderSuperficie.bounds;
        int indiceDocumento = Random.Range(0,cantidadProps);

        Vector3 posicionDocumento = Vector3.zero;
        
        for(int i = 0; i < cantidadProps; i++)
        {

            float x= Random.Range(bounds.min.x,bounds.max.x);
            float z= Random.Range(bounds.min.z,bounds.max.z);
            float y= bounds.max.y +0.05f;

            Vector3 posicionAleatoria = new(x,y,z);

            prefabElegido = randomProps[Random.Range(0,randomProps.Length)];
            GameObject prop = Instantiate(prefabElegido,posicionAleatoria,prefabElegido.transform.rotation);
            prop.transform.parent = this.transform;

            Vector3 rotacion = prop.transform.eulerAngles;

            prop.transform.eulerAngles = new Vector3(rotacion.x, Random.Range(0,360),rotacion.z);

           if(i == indiceDocumento)
            {
                posicionDocumento = posicionAleatoria;
            }
        }

        if(posicionDocumento != Vector3.zero)
        {
            GameObject docInstanciado = Instantiate(documento, posicionDocumento, documento.transform.rotation);
            docInstanciado.transform.parent = this.transform;
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!managerGlobal.instance.puedeJugar)
        {
            LimpiarEscena();
        }
        
    }


    void LimpiarEscena()
    {
        foreach (Transform hijo in transform)
        {
            Destroy(hijo.gameObject);
        }
        this.enabled=false;
    }
}

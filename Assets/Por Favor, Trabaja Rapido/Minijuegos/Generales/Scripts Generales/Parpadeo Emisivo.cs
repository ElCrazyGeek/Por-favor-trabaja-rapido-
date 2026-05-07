using UnityEngine;

public class ParpadeoEmisivo : MonoBehaviour
{
    private Material materialInstancia;
    
    void Start()
    {
        materialInstancia = GetComponent<Renderer>().material;
    }

   
    void Update()
    {
        
    }
}

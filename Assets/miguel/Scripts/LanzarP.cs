using UnityEngine;

public class LanzarP : MonoBehaviour
{
   
   public GameObject prefabPelota;
   public Transform puntoLanzamiento;
   [Header("Configuración de Fuerza")]
   public float fuerzaMinima = 5f;
   public float fuerzaMaxima = 30f; 
   public float VelocidadCarga = 10f;
   [Range(0,90)] public float AnguloLanzamiento = 45f;

   private float FuerzaAct;
   private bool cargando = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            cargando = true;
            FuerzaAct = fuerzaMinima; 
        }
        else if (Input.GetMouseButton(0) && cargando) 
        {
            
            FuerzaAct += VelocidadCarga * Time.deltaTime;
            if (FuerzaAct > fuerzaMaxima)
                FuerzaAct = fuerzaMaxima;
        }
        else if (Input.GetMouseButtonUp(0) && cargando) 
        {
            Lanzar();
        }
    }

    void Lanzar()
    {
        GameObject pelota = Instantiate(prefabPelota, puntoLanzamiento.position, puntoLanzamiento.rotation);
        Rigidbody rb = pelota.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 direccion = puntoLanzamiento.forward;
            direccion = Quaternion.AngleAxis(-AnguloLanzamiento, puntoLanzamiento.right) * direccion; // Aplicar ángulo de lanzamiento
            rb.AddForce(direccion * FuerzaAct, ForceMode.Impulse);
        }
        Destroy(pelota, 10f); //Borrar pelotas después de 10 segundos
    }
   

   
}

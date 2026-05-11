using UnityEngine;

public class Dardo : MonoBehaviour
{
    public Rigidbody rb;
    private bool pegado = false;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Empieza quieto hasta que el Manager lo lance
    }

    void OnCollisionEnter(Collision collision) {
        if (pegado) return;

        // Verificamos el Tag que mencionaste
        if (collision.gameObject.CompareTag("Diana(tachuela)")) {
            pegado = true;
            rb.isKinematic = true; // Se detiene en seco
            rb.linearVelocity = Vector3.zero;
            
            // Lo emparentamos para que si la diana se mueve, el dardo se mueva con ella
            transform.SetParent(collision.transform);
            
            // Avisamos al Manager que pegamos en el blanco
            Dardos_Manager.instance.CalcularPuntuacion(transform.position);
        }
    }
}
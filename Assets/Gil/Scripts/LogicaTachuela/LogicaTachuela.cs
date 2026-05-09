using UnityEngine;

public class LogicaTachuela : MonoBehaviour
{
    public float minForce = 5f;
    public float maxForce = 15f;
    [SerializeField] private float velCarga = 2f;
    [SerializeField] private GameObject prefabTachuela;
    [SerializeField] private Transform spawnTachuela;

    public float actForce {get; private set;}
    private bool wasPressed;
    public void CargarTachuela(bool isPressed)
    {
        if (isPressed)
        {
            actForce += velCarga * Time.deltaTime;
            actForce = Mathf.Clamp(actForce, minForce, maxForce);
        }

        if (wasPressed && !isPressed)
        {
            LanzarTachuela();
            actForce = minForce;
        }
        wasPressed = isPressed;
    }


    private void LanzarTachuela()
    {
        GameObject tachuela = Instantiate(prefabTachuela, spawnTachuela.position, Quaternion.Euler(0,0,180));

        Rigidbody rb = tachuela.GetComponent<Rigidbody>();

        Logger.Instance.Log($"La bala hizo spawn en la pos: {tachuela.transform.position}",this);

        rb.AddForce(spawnTachuela.right * actForce, ForceMode.Impulse);
    }
}

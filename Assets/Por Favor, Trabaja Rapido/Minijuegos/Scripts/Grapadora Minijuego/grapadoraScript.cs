using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class grapadoraScript : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform papel;
    [SerializeField] private Transform grapadora;

    [SerializeField] private float velocidadHuida;
    [SerializeField] private float rangoHuida;
    [SerializeField] private float velocidadGrapadora;
    [SerializeField] private float distanciaClick;
    [SerializeField] private Collider mesa;

    [Header ("Audio")]
    [SerializeField] private AudioClip sfxGrapa;
    Bounds bounds;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!managerGlobal.instance.puedeJugar) return;

        bounds = mesa.bounds;

        MoverGrapadora();
        MoverPapel();

        
    }

    public void OnClick()
    {
        if (Vector3.Distance(papel.position, grapadora.position) < distanciaClick && managerGlobal.instance.puedeJugar)
        {
            audioManager.instance.reproducirSFX(sfxGrapa);
            managerGlobal.instance.ganoMinijuego();
        }
    }
    public void OnHold(){}
    public void OnCancel(){}

    void MoverGrapadora()
{
    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

    if (Physics.Raycast(ray, out RaycastHit hit))
    {
        Vector3 pos = hit.point;
        

        pos.x = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
        pos.z = Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
        grapadora.position = new Vector3(pos.x, grapadora.position.y, pos.z);
    }
}

void MoverPapel()
{
    float distancia = Vector3.Distance(papel.position, grapadora.position);

   
    if (distancia < rangoHuida)
    {
        Vector3 dir = (papel.position - grapadora.position).normalized;
        dir.y = 0f;

        papel.position += dir * velocidadHuida * Time.deltaTime;
    }

   
    Vector3 pos = papel.position;

    float offsetX = (bounds.max.x - bounds.min.x) * 0.1f;
    float offsetZ = (bounds.max.z - bounds.min.z) * 0.1f;

    pos.x = Mathf.Clamp(pos.x, bounds.min.x + offsetX, bounds.max.x - offsetX);
    pos.z = Mathf.Clamp(pos.z, bounds.min.z + offsetZ, bounds.max.z - offsetZ);

    papel.position = pos;
}

}

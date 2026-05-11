using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class ManagerMinijuegos : MonoBehaviour
{

    public static ManagerMinijuegos instance;
    [SerializeField] private AudioClip sfxInicio;
    [SerializeField] private GameObject primerMinijuego;
    [SerializeField] private Transform spawnPoints;

    public int totalMinijuegos;
 

    [SerializeField] private CinemachineCamera virtualCam;

    public int juegosCompletados=0;
    private List<GameObject> minijuegosPrefabs = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance=this;
        } else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        cargarMinijuegos();
        SiguienteMinijuego();
        totalMinijuegos = minijuegosPrefabs.Count + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerGlobal.instance.textoTotal != null)
        {
            managerGlobal.instance.textoTotal.text = "Trabajos de Hoy: "+juegosCompletados;
        }
    }

    void cargarMinijuegos()
    {
        GameObject[] cargados = Resources.LoadAll<GameObject>("Minijuegos");
        minijuegosPrefabs = cargados.ToList();

        if (minijuegosPrefabs.Contains(primerMinijuego))
        {
            minijuegosPrefabs.Remove(primerMinijuego);
        }
    }


    public void SiguienteMinijuego()
    {
        GameObject minijuegoAElegir;

        if (juegosCompletados == 0)
        {
            minijuegoAElegir=primerMinijuego;
        } else
        {
            int indiceAleatorio = Random.Range(0, minijuegosPrefabs.Count);
            minijuegoAElegir = minijuegosPrefabs[indiceAleatorio];
            minijuegosPrefabs.RemoveAt(indiceAleatorio);

        }

        Transform puntoSpawn = spawnPoints.Find(minijuegoAElegir.name);

        if(puntoSpawn == null)
        {
            Debug.LogError("No se encontro un punto de Spawn llamado:"  +minijuegoAElegir.name);
        }

        GameObject instanciaJuego = Instantiate(minijuegoAElegir, puntoSpawn.position, puntoSpawn.rotation);
        ConfigurarCamara(instanciaJuego);
        
    }

    void ConfigurarCamara(GameObject juegoActual)
    {
        Transform target = juegoActual.transform.Find("CamaraTarget");

        if(target != null)
        {
            virtualCam.Follow = target;
            virtualCam.LookAt = target;

            virtualCam.OnTargetObjectWarped(target, target.position - virtualCam.transform.position);
        } else
        {
            Debug.LogWarning("El minijuego no cuenta con un objeto llamado 'CamaraTarget'");
        }
    }
}

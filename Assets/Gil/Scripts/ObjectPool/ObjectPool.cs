using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int cantidad = 5;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < cantidad; i++)
        {
            CrearObjeto();
        }
    }


    private GameObject CrearObjeto()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);

        pool.Enqueue(obj);

        return obj;
    }

    public GameObject ObtenerObjeto()
    {
        if (pool.Count == 0)
        {
            CrearObjeto();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);

        return obj;
    }

    public void RegresarObjeto(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}

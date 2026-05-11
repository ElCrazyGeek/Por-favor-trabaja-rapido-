using System.Collections;
using UnityEngine;

public class cajonesScript : MonoBehaviour
{
    [SerializeField] private Transform[] cajones;


    [SerializeField] private float tiempoAperturaActual;
    [SerializeField] private float tiempoMaximo;

    [SerializeField] private float[] tiempoCajon;
    [SerializeField] private bool[] cajonAbierto;

    [SerializeField] private int probabilidadAbrir;

    [SerializeField] private GameObject[] RandomProps;
    [SerializeField] private GameObject documento;
    [SerializeField] private int probabilidadCorrecto;

    int propElegido;
    private bool aparecioDocumento;

    [SerializeField] private AudioClip sfxCajonAbre;
    [SerializeField] private AudioClip sfxCajonCierra;



    int cajonActual;

    private Vector3[] posicionesCerradas;
    private Vector3[] posicionesAbiertas;
    private GameObject[] propsInstanciados;

    void Start()
    {

        posicionesCerradas = new Vector3[cajones.Length];
        posicionesAbiertas = new Vector3[cajones.Length];

        propsInstanciados = new GameObject[cajones.Length];
        for(int i = 0; i < cajones.Length; i++)
        {
            posicionesCerradas[i] = cajones[i].localPosition;

            posicionesAbiertas[i] = posicionesCerradas[i]+ new Vector3(-0.008f,0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!managerGlobal.instance.puedeJugar) return;

        tiempoAperturaActual+=Time.deltaTime;

        if (tiempoAperturaActual >= tiempoMaximo)
        {
            int cajonRandom = Random.Range(0, cajones.Length);

            if (!cajonAbierto[cajonRandom])
            {
                int probabilidad = Random.Range(0,100);

                if(probabilidad < probabilidadAbrir)
                {
                    StartCoroutine(MoverCajon(cajones[cajonRandom], posicionesAbiertas[cajonRandom],0.1f));
                      audioManager.instance.reproducirSFX(sfxCajonAbre);
                    cajonAbierto[cajonRandom]=true;
                    tiempoCajon[cajonRandom]=Random.Range(0.3f,1.5f);

                    probabilidadAbrir= Mathf.Max(5,probabilidadAbrir-5);
                    SpawnProp(cajonRandom);
                        
                    
                }
            }
            tiempoAperturaActual=0f;

        }

         for(int i = 0; i < cajones.Length; i++)
            {
                if (cajonAbierto[i])
                {
                    tiempoCajon[i]-=Time.deltaTime;

                       if (tiempoCajon[i] <= 0)
            {
                StartCoroutine(MoverCajon(cajones[i],posicionesCerradas[i],0.1f));

            audioManager.instance.reproducirSFX(sfxCajonCierra);
            cajonAbierto[i]=false;
            probabilidadAbrir+=5;

            aparecioDocumento=false;

            if(propsInstanciados != null)
                    {
                        Destroy(propsInstanciados[i]);
                    }
            }
                }
            }


       

    }

    IEnumerator MoverCajon(Transform cajon, Vector3 destino, float duracion)
    {
        Vector3 inicio = cajon.localPosition;

        float tiempo=0f;
      

        while(tiempo < duracion)
        {
            tiempo+= Time.deltaTime;

            float t= tiempo/duracion;

            cajon.localPosition=Vector3.Lerp(inicio, destino, t);
            yield return null;
        }
        cajon.localPosition=destino;
    }


    void SpawnProp(int index)
    {
        int correcto = Random.Range(0,20);
        //Debug.Log(correcto);

        if(correcto < probabilidadCorrecto && !aparecioDocumento)
        {
            propsInstanciados[index] = Instantiate(documento,cajones[index].position,cajones[index].rotation);
            aparecioDocumento=true;
        } else
        {
            int propsRandom = Random.Range(0,RandomProps.Length);

            propsInstanciados[index]= Instantiate(RandomProps[propsRandom],cajones[index].position,cajones[index].rotation);
        }
    }

    

    

   
}



using UnityEngine;

public class IluminacionManager : MonoBehaviour
{
    [SerializeField] private GameObject luzDireccional;

    public static IluminacionManager instance;
    private float intensidadOriginal;
    private Color colorOriginal;

   public bool apagado;



    void Awake()
    {
        instance = this;
        intensidadOriginal = RenderSettings.ambientIntensity;
        colorOriginal = RenderSettings.ambientLight;
    }


    public void ActivarOscuridad()
    {
        apagado=true;
        luzDireccional.SetActive(false);
        RenderSettings.ambientIntensity = 0.2f;
        RenderSettings.ambientLight = Color.black;

    }

    public void RestaurarLuz()
    {
        apagado=false;
        luzDireccional.SetActive(true);
         RenderSettings.ambientIntensity = intensidadOriginal;
        RenderSettings.ambientLight = colorOriginal;
    }
}

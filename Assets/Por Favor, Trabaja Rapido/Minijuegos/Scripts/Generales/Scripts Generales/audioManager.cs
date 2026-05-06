using UnityEngine;

public class audioManager : MonoBehaviour
{
     public AudioSource sfxSource;
     public AudioSource loopSource;

     public AudioSource UISource;

     public static audioManager instance;

    public AudioClip clickSFX;

    [Range(0f,1f)]
    public float volumenSFX;
    
    [Range(0f,1f)]
    public float pitchSFX;

    private float tiempoUltimoSFX;
    public float cooldownSFX = 0.1f;


    void Awake()
    {
       if( instance == null)
       {
           instance = this;
           DontDestroyOnLoad(gameObject);
       } else
        {
              Destroy(gameObject);
        }
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public void reproducirSFX(AudioClip clip)
    {
    if (Time.time - tiempoUltimoSFX < cooldownSFX) return;

    sfxSource.pitch = pitchSFX;
    sfxSource.PlayOneShot(clip, volumenSFX);
    tiempoUltimoSFX = Time.time;
    }

    public void reproducirLoop(AudioClip clip)
    {
        if (loopSource.isPlaying && loopSource.clip == clip && loopSource.loop) 
                return;

            loopSource.clip = clip;
            loopSource.loop = true;
            loopSource.pitch = pitchSFX;
            loopSource.volume = volumenSFX;
            loopSource.Play();
    }

    public void detenerLoop(AudioClip clip)
    {
        if (loopSource.clip == clip && loopSource.isPlaying)
    {
        loopSource.Stop();
        loopSource.loop = false;
    }
    }

    public void reproducirUISFX()
    {
        UISource.PlayOneShot(clickSFX, volumenSFX);
    }
}

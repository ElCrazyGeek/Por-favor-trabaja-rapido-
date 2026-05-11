using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioManager.instance.reproducirMusica(2);
    }


    public void Salir(){
        Application.Quit();
    }

    public void jugar()
    {
        SceneManager.LoadScene("La oficina");
    }

    public void Creditos()
    {
        panelCreditos.SetActive(true);
    }


    public void regresar()
    {
        panelCreditos.SetActive(false);
    }
}

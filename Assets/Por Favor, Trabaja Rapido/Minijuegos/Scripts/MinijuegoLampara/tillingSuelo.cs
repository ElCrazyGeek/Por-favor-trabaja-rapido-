using UnityEngine;

public class tillingSuelo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer renderer= GetComponent<Renderer>();

        renderer.material.mainTextureScale= new Vector2(3f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

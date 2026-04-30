using UnityEngine;
using TMPro;

public class TextoAnimacion : MonoBehaviour
{

    [SerializeField] private TMP_Text textoComponente;
    
    void Start()
    {
        
    }


    void Update()
    {
        textoComponente.ForceMeshUpdate();
        var textInfo = textoComponente.textInfo;

        for(int i=0; i< textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

           for (int j = 0; j < 4; j++)
            {
                var orig = vertices[charInfo.vertexIndex + j];
                
                
               float ruido = Mathf.PerlinNoise(i, Time.time * 2f);

                float tiempo = Time.time;   
                float intensidad = 20f;
              

            Vector3 offset = new Vector3(
                (Mathf.Sin(tiempo * 10f + i) + ruido) * intensidad,
                (Mathf.Cos(tiempo * 10f + i) + ruido) * intensidad,
                0
            );

                vertices[charInfo.vertexIndex + j] = orig + offset;
            }
        }

        for(int i=0; i< textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textoComponente.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

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
                
                
                float intensidad = 4f; 
                
                Vector3 temblor = new Vector3(
                    Random.Range(-intensidad, intensidad), 
                    Random.Range(-intensidad, intensidad), 
                    0
                );

                vertices[charInfo.vertexIndex + j] = orig + temblor;
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

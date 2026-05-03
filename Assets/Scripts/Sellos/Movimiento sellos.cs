using UnityEngine;

public class Movimientosellos : MonoBehaviour
{
    RectTransform rectTransform;
    Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        // Opcional: Ocultar el cursor real para que solo se vea el sello
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 posicionLocal;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, 
            out posicionLocal
        );
        rectTransform.anchoredPosition = posicionLocal;
    }
}
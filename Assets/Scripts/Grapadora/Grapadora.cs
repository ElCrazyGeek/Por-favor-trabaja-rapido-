using UnityEngine;

public class Grapadora : MonoBehaviour
{
    public RectTransform rect;

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos
        );

        rect.anchoredPosition = pos;
    }
}
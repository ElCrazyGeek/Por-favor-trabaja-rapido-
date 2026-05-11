using UnityEngine;
using UnityEngine.InputSystem;

public class Grapadora : MonoBehaviour
{
    public RectTransform rect;

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect.parent as RectTransform,
           Mouse.current.position.ReadValue(),
            null,
            out pos
        );

        rect.anchoredPosition = pos;
    }
}
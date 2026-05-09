using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text score_txt;

    private int scoreActual = 0;
    private float valActual = 0;
    public void UICargarBarra(float fuerza, float minForce, float maxForce)
    {
        float porcentaje = Mathf.InverseLerp(minForce, maxForce, fuerza);
        image.fillAmount = porcentaje;
    }

    public void ScoreTxt(int score)
    {
        scoreActual += score;
        score_txt.text = scoreActual.ToString();
        Logger.Instance.Log($"Se agregaron: {score} puntos", this);
    }
}

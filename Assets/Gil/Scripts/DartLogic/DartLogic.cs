using UnityEngine;

public class DartLogic : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        DartboardLogic board = collision.gameObject.GetComponent<DartboardLogic>();
        if (board != null)
        {
            ContactPoint contatct = collision.contacts[0];
            UILogic ui = GameObject.Find("UILogic").GetComponent<UILogic>();
            int score = board.GetScore(contatct.point);
            Logger.Instance.Log($"Se obtuvieron: {score} puntos", this);
            ui.ScoreTxt(score);

        }
    }
}

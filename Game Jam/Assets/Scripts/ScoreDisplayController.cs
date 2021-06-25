using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayController : MonoBehaviour
{
    public Image Left;
    public Image Middle;
    public Image Right;

    public Sprite[] Numbers;

    private void Start()
    {
        PushScore();
    }

    public void PushScore()
    {
        int[] digits = UIController.Instance.GetScoreDigits();

        Left.sprite = Numbers[ digits[ 2 ] ];
        Middle.sprite = Numbers[ digits[ 1 ] ];
        Right.sprite = Numbers[ digits[ 0 ] ];

        Color tint = new Color(1, 1, 1, 1);
        Left.color = tint;
        Middle.color = tint;
        Right.color = tint;
    }
}

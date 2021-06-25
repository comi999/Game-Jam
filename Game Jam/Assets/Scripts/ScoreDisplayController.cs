using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayController : MonoBehaviour
{
    public Text ScoreText;
    public Sprite Background;

    private void Start()
    {
        ScoreText.text = "SCORE: " + UIController.Instance.Score.ToString();
    }
}

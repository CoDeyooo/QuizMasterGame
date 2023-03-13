using System;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    private ScoreKeeper scoreKeeper;
    private string finalMessage = $"Congratulations!{Environment.NewLine}Your Score: ";

    private void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        this.finalScoreText.text = $"{this.finalMessage + this.scoreKeeper.ScorePercentage}%";
    }
}

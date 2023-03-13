using System;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int questionsCount = 0;
	private int correctAnswersCount = 0;

	public void IncrementCorrectAnswerCount()
    {
        this.correctAnswersCount++;
    }

    public void UpdateScore()
    {
        this.scoreText.text = $"Score: {String.Format("{0:0.##}", this.ScorePercentage)}%";
    }

    public int QuestionsCount
    {
        get => this.questionsCount;
        set => this.questionsCount = value;
    }
    
    public float ScorePercentage
    {
        get => ((float)this.correctAnswersCount / this.questionsCount) * 100f;
    }
}

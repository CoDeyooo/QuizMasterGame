using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    private Timer timer;
    private ProgressManager progressManager;
    private QuestionSO currentQuestion;
    private ScoreKeeper scoreKeeper;
    private int correctAnswerIndex;
    private bool isAnswerDisplayed;

    public bool isComplete = false;

    public void OnAnswerSelected(int index)
    {
        this.DisplayAnswer(index);
        this.isAnswerDisplayed = true;
        this.timer.ImageFillAmount = 0;
        this.SetButtonsInteractability(false);
    }

    private void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
        this.timer = FindObjectOfType<Timer>();
        this.progressManager = FindObjectOfType<ProgressManager>();

        this.scoreKeeper.QuestionsCount = this.questions.Count;
        this.progressBar.maxValue = this.questions.Count;
    }

    private void Start()
    {
        this.LoadNextQuestion();
    }

    private void Update()
    {
        if (this.timer.ImageFillAmount <= 0)
        {
            if (!this.timer.isPlayerAnswering)
            {
                this.LoadNextQuestion();
            }
            else
            {
                this.DisplayAnswer(-1);
                this.isAnswerDisplayed = true;
            }
        }
        else
        {
            this.timerImage.fillAmount = this.timer.ImageFillAmount;
        }

        if (this.progressManager.shouldProgress)
        {
            this.progressBar.value = this.progressManager.progress;
        }
    }

    private void LoadNextQuestion()
    {
        this.isAnswerDisplayed = false;

        if (this.questions.Count == 0)
        {
            this.isComplete = true;
            return;
        }

        this.currentQuestion = this.GetRandomQuestion();
        this.SetButtonsInteractability(true);
        this.ResetButtonSprite();
        this.DisplayQuestion();
        this.SetButtonVisibility();
        this.progressManager.IncrementProgress();
        this.correctAnswerIndex = this.currentQuestion.GetCorrectAnswerIndex();
    }

    private QuestionSO GetRandomQuestion()
    {
        var question = this.questions[UnityEngine.Random.Range(0, this.questions.Count)];
        this.questions.Remove(question);
        return question;
    }

    private void SetButtonsInteractability(bool isInteractable)
    {
        foreach (var button in this.answerButtons)
        {
            var buttonComponent = button.GetComponent<Button>();
            buttonComponent.interactable = isInteractable;
        }
    }

    private void SetButtonVisibility()
    {
        var limit = this.currentQuestion.GetAnswersCount();
        for (int i = 0; i < this.answerButtons.Length; i++)
        {
            var isButtonActive = true;

            if (i >= limit)
            {
                isButtonActive = false;
            }

            var buttonComponent = this.answerButtons[i].GetComponent<Button>();
            buttonComponent.gameObject.SetActive(isButtonActive);
        }
    }

    private void DisplayQuestion()
    {
        this.questionText.text = currentQuestion.GetQuestion();
    
        for (int i = 0; i < this.currentQuestion.GetAnswersCount(); i++)
        {
            var buttonText = this.answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = this.currentQuestion.GetAnswer(i);
        }
    }

    private void DisplayAnswer(int index)
    {
        if (this.isAnswerDisplayed) { return; }

        if (index != this.correctAnswerIndex)
        {
            var answerText = currentQuestion.GetAnswer(this.correctAnswerIndex);
            this.questionText.text = $"Incorrect. The correct answer was:{Environment.NewLine}" + answerText;
            
            if (index < 0)
            {
                this.questionText.text = this.questionText.text.Replace("Incorrect.", "Time's up!");
            }
        }
        else
        {
            this.questionText.text = "Correct!";
            this.scoreKeeper.IncrementCorrectAnswerCount();
        }

        this.scoreKeeper.UpdateScore();

        var buttonImage = this.answerButtons[this.correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = this.correctAnswerSprite;
    }

    private void ResetButtonSprite()
    {
        var correctAnswerImage = this.answerButtons[this.correctAnswerIndex].GetComponent<Image>();
        correctAnswerImage.sprite = this.defaultAnswerSprite;
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter a new question here";
    [SerializeField] List<string> answers = new List<string>();
    [SerializeField] int correctAnswerIndex = 0;
    public string GetQuestion()
    {
        return this.question;
    }

    public int GetCorrectAnswerIndex()
    {
        return this.correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return this.answers[index];
    }

    public int GetAnswersCount()
    {
        return this.answers.Count;
    }
}

using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float secondsToAnswer = 20f;
    [SerializeField] float secondsToReview = 10f;

    public bool isPlayerAnswering;
    private float imageFillAmount;

    void Start()
    {
        this.ImageFillAmount = 1;
        this.isPlayerAnswering = true;
    }

    void Update()
    {
        this.UpdateTimer();
    }

    private void UpdateTimer()
    {
        var seconds = this.secondsToAnswer;

        if (!this.isPlayerAnswering)
        {
            seconds = this.secondsToReview;
        }

        if (this.imageFillAmount <= 0)
        {
            this.isPlayerAnswering ^= true;
            this.imageFillAmount = 1;
        }

        this.imageFillAmount -= Time.deltaTime / seconds;
    }

    public float ImageFillAmount
    {
        get => this.imageFillAmount;
        set => this.imageFillAmount = value;
    }
}

using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public float progress = 0;
    public bool shouldProgress = false;
    
    private float progressLimit = 0;

    public void IncrementProgress()
    {
        this.progressLimit++;
        this.shouldProgress = true;
    }

    private void Update()
    {
        if (this.shouldProgress)
        {
            this.progress += Time.deltaTime;
        }

        if (this.progress >= this.progressLimit)
        {
            this.progress = this.progressLimit;
            this.shouldProgress = false;
        }
    }
}

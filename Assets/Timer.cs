using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    

    [SerializeField] private TMP_Text timerText;

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;

    private void Start()
    {
       StartTimer();
    }

    private void Update()
    {
        if (!isTimerRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public string GetFinalTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatsDisplayWidget : MonoBehaviour
{
    [SerializeField] private UIStatDisplayWidget _attemptsWidget;
    [SerializeField] private UIStatDisplayWidget _timerWidget;

    private void OnEnable()
    {
        GameManager.Instance.OnAttemptsChanged += HandleAttemptsChanged;
        GameManager.Instance.OnTimerChanged += HandleTimerChanged;

        Initialize();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAttemptsChanged -= HandleAttemptsChanged;
        GameManager.Instance.OnTimerChanged -= HandleTimerChanged;
    }

    private void Initialize()
    {
        _attemptsWidget.Set("Attempts", "0");
        _timerWidget.Set("Time", "0:00");
    }

    private void HandleAttemptsChanged(int attempts)
    {
        _attemptsWidget.SetValue(attempts.ToString());
    }

    private void HandleTimerChanged(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerWidget.SetValue(formattedTime);
    }
}

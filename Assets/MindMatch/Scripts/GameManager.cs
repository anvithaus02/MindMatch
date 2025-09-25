using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<int> OnAttemptsChanged;
    public event Action<float> OnTimerChanged;
    public event Action OnLevelCompleted;

    private int _attempts;
    private float _timer;
    private bool _isLevelActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_isLevelActive)
        {
            _timer += Time.deltaTime;
            OnTimerChanged?.Invoke(_timer);
        }
    }

    public void StartLevel()
    {
        _attempts = 0;
        _timer = 0f;
        _isLevelActive = true;

        OnAttemptsChanged?.Invoke(_attempts);
        OnTimerChanged?.Invoke(_timer);
    }

    public void RegisterAttempt()
    {
        _attempts++;
        OnAttemptsChanged?.Invoke(_attempts);
    }

    public void CompleteLevel()
    {
        _isLevelActive = false;
        OnLevelCompleted?.Invoke();
    }
}

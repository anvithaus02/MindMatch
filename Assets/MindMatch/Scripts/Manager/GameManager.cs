using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<int> OnAttemptsChanged;
    public event Action<float> OnTimerChanged;

    public event Action<LevelData> OnLevelStarted;

    public event Action OnLevelCompleted;

    private int _attempts;
    private float _timer;
    private bool _isLevelActive;
    private LevelData _currentLevel;


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

    public void StartLevel(LevelData levelData)
    {
        _currentLevel = levelData;
        _attempts = 0;
        _timer = 0f;
        _isLevelActive = true;

        OnLevelStarted?.Invoke(levelData);

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

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (_currentLevel.LevelNumber >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", _currentLevel.LevelNumber + 1);
            PlayerPrefs.Save();
        }

        OnLevelCompleted?.Invoke();
    }

    public LevelData GetCurrentLevel() => _currentLevel;

}

using System;
using System.Collections;
using MindMatch.Gameplay;
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
    private LevelData _currentLevelData;

    private MindCard _firstSelected;
    private MindCard _secondSelected;

    private int _totalCards;
    private int _matchedCards;


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
        _currentLevelData = levelData;
        _attempts = 0;
        _timer = 0f;
        _isLevelActive = true;
        _matchedCards = 0;

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

        if (_currentLevelData.LevelNumber >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", _currentLevelData.LevelNumber + 1);
            PlayerPrefs.Save();
        }

        OnLevelCompleted?.Invoke();
    }

    public void OnCardSelected(MindCard card)
    {
        if (_firstSelected == null)
        {
            _firstSelected = card;
        }
        else if (_secondSelected == null)
        {
            _secondSelected = card;
            GameManager.Instance.RegisterAttempt();
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (_firstSelected == null || _secondSelected == null)
        {
            ResetSelection();
            yield break;
        }

        bool isMatch = _firstSelected.CardIcon == _secondSelected.CardIcon;

        if (isMatch)
            HandleMatch();
        else
            HandleMismatch();

        ResetSelection();
    }

    private void HandleMatch()
    {
        _firstSelected.SetMatched();
        _secondSelected.SetMatched();

        _matchedCards += 2;
        int totalCards = _currentLevelData.Rows * _currentLevelData.Columns;

        if (_matchedCards >= totalCards)
            GameManager.Instance.CompleteLevel();
    }

    private void HandleMismatch()
    {
        _firstSelected.ResetCard();
        _secondSelected.ResetCard();
    }

    private void ResetSelection()
    {
        _firstSelected = null;
        _secondSelected = null;
    }


    public LevelData GetCurrentLevel() => _currentLevelData;

}

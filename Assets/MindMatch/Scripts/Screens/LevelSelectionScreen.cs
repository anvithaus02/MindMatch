using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScreen : MonoBehaviour
{
    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private LevelSelectorWidget _levelSelectorPrefab;
    [SerializeField] private Transform _parentContainer;
    [SerializeField] private BackButton _backButton;


    private void OnEnable()
    {
        _backButton.Initialize(OnBackButtonClick);
        PopulateLevels();
    }

    private void PopulateLevels()
    {
        foreach (Transform child in _parentContainer)
        {
            Destroy(child.gameObject);
        }
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // default to level 1

        foreach (var level in _levelsConfig.GetAllLevels())
        {
            LevelSelectorWidget widget = Instantiate(_levelSelectorPrefab, _parentContainer);
            widget.Setup(level, OnLevelClicked, unlockedLevel);
        }
    }

    private void OnLevelClicked(LevelData level)
    {
        ScreenManager.Instance.ShowScreen(Screen.GamePlayScreen);
        GameManager.Instance.StartLevel(level);
    }

    private void OnBackButtonClick()
    {
        ScreenManager.Instance.Back();
    }
}

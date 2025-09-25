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
        // Clear old widgets
        foreach (Transform child in _parentContainer)
        {
            Destroy(child.gameObject);
        }

        // Create widget for each level
        for (int i = 0; i < _levelsConfig.GetAllLevels().Count; i++)
        {
            LevelData level = _levelsConfig.GetAllLevels()[i];
            LevelSelectorWidget widget = Instantiate(_levelSelectorPrefab, _parentContainer);

            widget.Setup(level, OnLevelClicked);

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

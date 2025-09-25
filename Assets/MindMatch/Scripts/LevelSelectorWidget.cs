using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LevelSelectorWidget : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _lockIcon;

    private LevelData _levelData;
    private Action<LevelData> _onClick;

    /// <summary>
    /// Current unlocked level index from PlayerPrefs (or any persistent system)
    /// </summary>
    private int _unlockedLevel;

    /// <summary>
    /// Setup the widget with level data and click callback
    /// </summary>
    public void Setup(LevelData levelData, Action<LevelData> onClick, int unlockedLevel)
    {
        _levelData = levelData;
        _onClick = onClick;
        _unlockedLevel = unlockedLevel;

        _levelText.text = levelData.LevelNumber.ToString();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnButtonClicked);

        SetLevelState();
    }

    private void OnButtonClicked()
    {
        // Only allow click if level is unlocked
        if (_levelData.LevelNumber <= _unlockedLevel)
        {
            _onClick?.Invoke(_levelData);
        }
        else
        {
            // Optional: play "locked" feedback
            Debug.Log($"Level {_levelData.LevelNumber} is locked!");
        }
    }

    private void SetLevelState()
    {
        bool isUnlocked = _levelData.LevelNumber <= _unlockedLevel;

        _lockIcon.gameObject.SetActive(!isUnlocked);
        _levelText.gameObject.SetActive(isUnlocked);

        _button.interactable = isUnlocked;
    }
}

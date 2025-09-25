using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LevelSelectorWidget : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _levelText;

    private LevelData _levelData;
    private Action<LevelData> _onClick;

    public void Setup(LevelData levelData, Action<LevelData> onClick)
    {
        _levelData = levelData;
        _onClick = onClick;

        _levelText.text = levelData.LevelNumber.ToString();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _onClick?.Invoke(_levelData));
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
namespace MindMatch.Gameplay.UI
{
    public class LevelSelectorWidget : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _lockIcon;

        private LevelData _levelData;
        private Action<LevelData> _onClick;

        private int _unlockedLevel;

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
            AudioManager.Instance.PlayAudio(AudioType.ButtonClick);
            if (_levelData.LevelNumber <= _unlockedLevel)
            {
                _onClick?.Invoke(_levelData);
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
}
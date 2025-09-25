using MindMatch.UI;
using UnityEngine;

namespace MindMatch.Gameplay.UI
{
    public class GamePlayScreen : MonoBehaviour
    {
        [Header("Prefabs & References")]
        [SerializeField] private MindCard _mindCardPrefab;
        [SerializeField] private RectTransform _gamePlayArea;
        [SerializeField] private CategoryData _categoryData;
        [SerializeField] private BackButton _backButton;

        [Header("Win Screen")]
        [SerializeField] private GameObject _winInfoHolder;
        [SerializeField] private ActionButton _okButton;


        private void OnEnable()
        {
            SetWinInfoVisible(false);

            GameManager.Instance.OnLevelStarted += InitializeLevel;
            GameManager.Instance.OnLevelCompleted += () => SetWinInfoVisible(true);

            _backButton.Initialize(HandleBackOrOkButton);
            _okButton.Initialize("OK", HandleBackOrOkButton);
        }

        private void OnDisable()
        {
            GameManager.Instance.OnLevelStarted -= InitializeLevel;
            GameManager.Instance.OnLevelCompleted -= () => SetWinInfoVisible(true);

            SetWinInfoVisible(false);
            ClearPlayArea();

        }
        private void InitializeLevel(LevelData level)
        {
            ClearPlayArea();
            CardSpawner.SpawnCards(_mindCardPrefab, _categoryData, _gamePlayArea, level);
        }

        private void SetWinInfoVisible(bool isVisible) => _winInfoHolder.SetActive(isVisible);

        private void HandleBackOrOkButton() => ScreenManager.Instance.Back();

        private void ClearPlayArea()
        {
            foreach (Transform child in _gamePlayArea)
                Destroy(child.gameObject);
        }
    }
}
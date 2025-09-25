using System.Collections;
using MindMatch.UI;
using UnityEngine;
using UnityEngine.UI;
namespace MindMatch.Gameplay.UI
{
    public class GamePlayScreen : MonoBehaviour
    {
        [SerializeField] private MindCard _mindCardPrefab;
        [SerializeField] private RectTransform _gamePlayArea;
        [SerializeField] private CategoryData _categoryData;
        [SerializeField] private BackButton _backButton;

        [Header("Win info")]
        [SerializeField] private GameObject _winInfoHolder;
        [SerializeField] private ActionButton _okButton;
        private CardSpawner _spawner;
        private CardImageProvider _imageProvider;

        private MindCard _firstSelected;
        private MindCard _secondSelected;

        private int _totalCards;
        private int _matchedCards;

        private void Awake()
        {
            _spawner = new CardSpawner(_mindCardPrefab);
            _imageProvider = new CardImageProvider(_categoryData);
        }

        private void OnEnable()
        {
            _winInfoHolder.SetActive(false);

            GameManager.Instance.OnLevelStarted += InitializeLevel;
            GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
            _backButton.Initialize(OnBackButtonClick);
            _okButton.Initialize("OK", OnOkButtonClick);
        }

        private void OnDisable()
        {
            GameManager.Instance.OnLevelStarted -= InitializeLevel;
            GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
            _winInfoHolder.SetActive(false);

            foreach (Transform child in _gamePlayArea)
                Destroy(child.gameObject);

        }
        private void InitializeLevel(LevelData level)
        {
            int rows = level.Rows;
            int columns = level.Columns;
            int padding = 5;
            _totalCards = rows * columns;
            _matchedCards = 0;

            var cardImages = _imageProvider.GetShuffledPairs(level.Category, _totalCards);
            _spawner.SpawnCards(_gamePlayArea, cardImages, rows, columns, padding);
        }

        private void OnLevelCompleted()
        {
            _winInfoHolder.SetActive(true);
        }

        private void OnBackButtonClick()
        {
            ScreenManager.Instance.Back();
        }

        private void OnOkButtonClick()
        {
            ScreenManager.Instance.Back();
        }
    }
}
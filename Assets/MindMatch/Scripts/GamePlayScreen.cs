using System.Collections;
using UnityEngine;

public class GamePlayScreen : MonoBehaviour
{
    [SerializeField] private MindCard _mindCardPrefab;
    [SerializeField] private DynamicGridGenerator _dynamicGridGenerator;
    [SerializeField] private CategoryData _categoryData;
    [SerializeField] private Category category = Category.Animals;

    private CardSpawner _spawner;
    private CardImageProvider _imageProvider;

    private MindCard _firstSelected;
    private MindCard _secondSelected;

    private int _totalCards;
    private int _matchedCards;

    private void Awake()
    {
        _spawner = new CardSpawner(_dynamicGridGenerator, _mindCardPrefab);
        _imageProvider = new CardImageProvider(_categoryData);
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        int rows = 5, columns = 6, padding = 5;
        _totalCards = rows * columns;
        _matchedCards = 0;

        var cardImages = _imageProvider.GetShuffledPairs(category, _totalCards);
        _spawner.SpawnCards(_dynamicGridGenerator.transform, cardImages, rows, columns, padding);

        // Subscribe to card events
        foreach (Transform child in _dynamicGridGenerator.transform)
        {
            MindCard card = child.GetComponent<MindCard>();
            if (card != null)
                card.OnCardSelected += OnCardSelected;
        }

        // Start tracking game progress
        GameManager.Instance.StartLevel();
    }

    private void OnCardSelected(MindCard card)
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
            yield break;

        if (_firstSelected.CardIcon == _secondSelected.CardIcon)
        {
            _firstSelected.SetMatched();
            _secondSelected.SetMatched();

            _matchedCards += 2;
            if (_matchedCards >= _totalCards)
                GameManager.Instance.CompleteLevel();
        }
        else
        {
            _firstSelected.ResetCard();
            _secondSelected.ResetCard();
        }

        _firstSelected = null;
        _secondSelected = null;
    }
}

using System.Collections;
using System.Collections.Generic;
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

        var cardImages = _imageProvider.GetShuffledPairs(category, rows * columns);
        _spawner.SpawnCards(_dynamicGridGenerator.transform, cardImages, rows, columns, padding);

        // Subscribe to card events
        foreach (Transform child in _dynamicGridGenerator.transform)
        {
            MindCard card = child.GetComponent<MindCard>();
            if (card != null)
                card.OnCardSelected += OnCardSelected;
        }
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
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f); // brief delay to show second card

        if (_firstSelected == null || _secondSelected == null)
            yield break;

        if (_firstSelected.CardIcon == _secondSelected.CardIcon)
        {
            _firstSelected.SetMatched();
            _secondSelected.SetMatched();
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

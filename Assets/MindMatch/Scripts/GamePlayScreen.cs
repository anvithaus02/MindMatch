using UnityEngine;

public class GamePlayScreen : MonoBehaviour
{
    [SerializeField] private MindCard _mindCardPrefab;
    [SerializeField] private DynamicGridGenerator _dynamicGridGenerator;
    [SerializeField] private CategoryData _categoryData;
    [SerializeField] private Category category = Category.Animals;

    private CardSpawner _spawner;
    private CardImageProvider _imageProvider;

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
        int rows = 2, columns = 5, padding = 5;

        var cardImages = _imageProvider.GetShuffledPairs(category, rows * columns);
        _spawner.SpawnCards(_dynamicGridGenerator.transform, cardImages, rows, columns, padding);
    }
}

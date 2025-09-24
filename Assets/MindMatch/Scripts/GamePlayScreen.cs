using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    public float cellSize;
    public Vector2 startPos;
    public float padding;
}

public class GamePlayScreen : MonoBehaviour
{
    [Header("Card Setup")]
    [SerializeField] private MindCard _mindCardPrefab;
    [SerializeField] private DynamicGridGenerator _dynamicGridGenerator;
    [SerializeField] private CategoryData _categoryData;
    [SerializeField] private Category category = Category.Animals;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        SpawnCards(2, 5, 5);
    }

    private void SpawnCards(int rows, int columns, float padding)
    {
        int totalCards = rows * columns;

        List<Sprite> availableImages = _categoryData.GetAllImages(category);

        if (availableImages.Count < totalCards / 2)
        {
            Debug.LogError($"Not enough images in category {category}. Needed: {totalCards / 2}, Found: {availableImages.Count}");
            return;
        }

        List<Sprite> chosenImages = new List<Sprite>();
        List<Sprite> shuffledAvailable = new List<Sprite>(availableImages);
        Shuffle(shuffledAvailable); // randomize available set

        for (int i = 0; i < totalCards / 2; i++)
        {
            chosenImages.Add(shuffledAvailable[i]);
        }

        List<Sprite> cardImages = new List<Sprite>();
        foreach (var img in chosenImages)
        {
            cardImages.Add(img);
            cardImages.Add(img);
        }

        Shuffle(cardImages);

        SpawnData spawnData = _dynamicGridGenerator.GetCardSpawnPositions(rows, columns, padding);

        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                MindCard card = Instantiate(_mindCardPrefab, _dynamicGridGenerator.transform);
                RectTransform rt = card.GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2(spawnData.cellSize, spawnData.cellSize);

                float posX = spawnData.startPos.x + c * (spawnData.cellSize + spawnData.padding);
                float posY = spawnData.startPos.y - r * (spawnData.cellSize + spawnData.padding);

                rt.localPosition = new Vector3(posX, posY, 0);


                card.SetImage(cardImages[index]);
                index++;
            }
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}

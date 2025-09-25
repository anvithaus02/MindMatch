using System.Collections.Generic;
using UnityEngine;


public class SpawnData
{
    public float cellSize;
    public Vector2 startPos;
    public float padding;

    public SpawnData(float cellSize, Vector2 startPos, float padding)
    {
        this.cellSize = cellSize;
        this.startPos = startPos;
        this.padding = padding;
    }
}
public class CardSpawner
{
    private DynamicGridGenerator _gridGenerator;
    private MindCard _cardPrefab;

    public CardSpawner(DynamicGridGenerator gridGenerator, MindCard cardPrefab)
    {
        _gridGenerator = gridGenerator;
        _cardPrefab = cardPrefab;
    }

    public void SpawnCards(Transform parent, List<Sprite> cardImages, int rows, int columns, float padding)
    {
        if (cardImages.Count != (rows * columns))
        {
            Debug.LogError("Card images count does not match total cards!" +cardImages.Count+"   "+(rows * columns));
            return;
        }

        SpawnData spawnData = _gridGenerator.GetCardSpawnPositions(rows, columns, padding);

        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                MindCard card = Object.Instantiate(_cardPrefab, parent);
                RectTransform rt = card.GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2(spawnData.cellSize, spawnData.cellSize);
                rt.localPosition = new Vector3(
                    spawnData.startPos.x + c * (spawnData.cellSize + spawnData.padding),
                    spawnData.startPos.y - r * (spawnData.cellSize + spawnData.padding),
                    0);

                card.SetImage(cardImages[index]);
                index++;
            }
        }
    }
}

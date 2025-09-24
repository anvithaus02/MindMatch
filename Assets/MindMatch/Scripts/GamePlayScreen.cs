using System.Collections;
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
    [SerializeField] private MindCard _mindCard;
    [SerializeField] private DynamicGridGenerator _dynamicGridGenerator;
    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        SpawnCards(6, 6);
    }

    private void SpawnCards(int rows, int columns)
    {
       SpawnData spawnData = _dynamicGridGenerator.GetCardSpawnPositions(rows, columns);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                MindCard card = Instantiate(_mindCard, transform);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(spawnData.cellSize, spawnData.cellSize);

                float posX = spawnData.startPos.x + c * (spawnData.cellSize + spawnData.padding);
                float posY = spawnData.startPos.y - r * (spawnData.cellSize + spawnData.padding);
                rt.localPosition = new Vector3(posX, posY, 0);
            }
        }
    }
}

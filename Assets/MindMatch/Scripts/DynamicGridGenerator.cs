using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 3;
    [SerializeField] private float padding = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GenerateGrid();
        }
    }

    public void GenerateGrid()
    {
        RectTransform container = GetComponent<RectTransform>();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float containerWidth = container.rect.width;
        float containerHeight = container.rect.height;

        float cellWidth = (containerWidth - (columns - 1) * padding) / columns;
        float cellHeight = (containerHeight - (rows - 1) * padding) / rows;

        float totalGridWidth = columns * cellWidth + (columns - 1) * padding;
        float totalGridHeight = rows * cellHeight + (rows - 1) * padding;

        Vector2 pivotOffset = new Vector2(container.pivot.x * containerWidth, container.pivot.y * containerHeight);

        Vector2 startPos = new Vector2(-totalGridWidth / 2 + cellWidth / 2, totalGridHeight / 2 - cellHeight / 2);
        startPos = startPos - pivotOffset + new Vector2(containerWidth / 2, containerHeight / 2);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject card = Instantiate(cardPrefab, transform);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellWidth, cellHeight);

                float posX = startPos.x + c * (cellWidth + padding);
                float posY = startPos.y - r * (cellHeight + padding);
                rt.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}
